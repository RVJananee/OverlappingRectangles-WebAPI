using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioReference.WebAPI.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BioReference.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]    
    public class RectangleController : ControllerBase
    {
        // GET api/rectangle
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "rectangle1", "rectangle2" };
        }

        [HttpPost(Name = nameof(Calculate))]
        public ActionResult<Rectangle> Calculate([FromBody] List<Coordinates> coordinatesArray)
        {
            if (coordinatesArray == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Rectangle> result = new List<Rectangle>();

            for (int i = 0; i < coordinatesArray.Count; i++)
            {
                Coordinates rectangle = new Coordinates(
                    coordinatesArray[i].Top,
                    coordinatesArray[i].Left,
                    coordinatesArray[i].Bottom,
                    coordinatesArray[i].Right
                );
                Rectangle item = new Rectangle()
                {
                    SequenceNumber = i + 1,
                    Area = rectangle.GetRectangleArea(),
                    OverlappingCount = rectangle.GetOverLappingCount(coordinatesArray)
                };
                result.Add(item);
            }
            return Ok(result);
        }
    }
}