using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class DecorationElement : MapElement
    {
        private int _zoneWidth;
        private int _zoneHeight;
        private string _name;

        //properties
		public int zoneWidth
		{
			get { return _zoneWidth; }
			set { _zoneWidth = value; }
		}

        public int zoneHeight
        {
            get { return _zoneHeight; }
            set { _zoneHeight = value; }
        }

        public string name
		{
			get { return _name; }
			set { _name = value; }
		}

        //constructor
		public DecorationElement(string name, int zoneWidth, int zoneHeight, int width, int height, int placementX, int placementY) : base(width, height, placementX, placementY)
		{
            this.name = name;
            this.zoneWidth = zoneWidth;
            this.zoneHeight = zoneHeight;
		}
    }
}
