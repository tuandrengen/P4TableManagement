using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    public abstract class MapElement
    {
        public int width { get; set; }
        public int height { get; set; }
        public int placementX { get; set; }
        public int placementY { get; set; }

        public MapElement(int width, int height, int placementX, int placementY)
        {
            this.width = width;
            this.height = height;
            this.placementX = placementX;
            this.placementY = placementY;
        }
    }
}
