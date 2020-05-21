using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public class LargeTable : Table
    {

        public LargeTable(double width, double height, double placementX, double placementY) : base(width, height, placementX, placementY)
        {
            seats = 4;
        }
    }
}
