using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public class SmallTable : Table
    {
        public SmallTable(double width, double height, double placementX, double placementY) : base(width, height, placementX, placementY)
        {
            seats = 2;
        }
    }
}
