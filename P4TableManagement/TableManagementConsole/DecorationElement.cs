using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public class DecorationElement : MapElement
    {

        public int zoneWidth { get; set; }
        public int zoneHeight { get; set; }
        public string name { get; set; }

        // Eventually remove the X and Y coordinates, as it is not relevant here.
		public DecorationElement(string name, int zoneWidth, int zoneHeight, int width, int height, int placementX, int placementY) : base(width, height, placementX, placementY)
		{
            this.name = name;
            this.zoneWidth = zoneWidth;
            this.zoneHeight = zoneHeight;
		}
    }
}
