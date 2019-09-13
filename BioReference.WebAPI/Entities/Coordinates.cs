using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioReference.WebAPI.Entities
{
    public class Coordinates
    {
        public readonly int Left;
        public readonly int Right;
        public readonly int Top;
        public readonly int Bottom;
        public Point TopLeft; // Denotes the top-left point of the rectangle
        public Point BottomRight; // Denotes the bottom-right point of the rectangle
        public Coordinates(int top, int left, int bottom, int right)
        {
            this.Top = top;
            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;

            TopLeft.x = top;
            TopLeft.y = left;
            BottomRight.x = bottom;
            BottomRight.y = right;
        }

        public int GetRectangleArea()
        {
            //logic for area calculation
            int length, breadth;

            length = Math.Abs(this.Bottom - this.Top);
            breadth = Math.Abs(this.Right - this.Left);
            return length * breadth;
        }

        public int GetOverLappingCount(List<Coordinates> coordinatesArray)
        {
            var filteredCoordinatesArray = coordinatesArray.ToList();
            var itemToRemove = filteredCoordinatesArray.Single(r => r.Top == this.Top && r.Right == this.Right && r.Left == this.Left && r.Bottom == this.Bottom);
            filteredCoordinatesArray.Remove(itemToRemove);

            int count = 0;

            for (int i = 0; i < filteredCoordinatesArray.Count; i++)
            {
                if (this.IsOverLapping(filteredCoordinatesArray[i]))
                {
                    count = count + 1;
                }
            }
            return count;
        }

        public bool IsOverLapping(Coordinates otherRectangle)
        {
            // If one rectangle is on left side of other  
            if (this.TopLeft.x > otherRectangle.BottomRight.x || otherRectangle.TopLeft.x > this.BottomRight.x)
            {
                return false;
            }

            // If one rectangle is above other 
            if (this.TopLeft.y < otherRectangle.BottomRight.y || otherRectangle.TopLeft.y < this.BottomRight.y)
            {
                return false;
            }
            return true;
        }
    }
}
