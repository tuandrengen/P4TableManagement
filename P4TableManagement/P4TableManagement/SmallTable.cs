using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public class SmallTable : Table
    {
        public SmallTable(double width, double height, double placementX, double placementY) : base(width, height, placementX, placementY)
        {
            this.width = width;
            this.height = height;
            this.placementX = placementX;
            this.placementY = placementY;
            seats = 2;
        }
    }
}
