using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public abstract class MapElement
    {
        public double width { get; set; }
        public double height { get; set; }
        public double placementX { get; set; }
        public double placementY { get; set; }

        public MapElement(double width, double height, double placementX, double placementY)
        {
            this.width = width;
            this.height = height;
            this.placementX = placementX;
            this.placementY = placementY;
        }
    }
}
