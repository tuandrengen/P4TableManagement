using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class MapElement
    {
        private int _width;
        private int _height;
        private int _placementX;
        private int _placementY;

        //properties
        public int width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int height 
        { 
            get { return _height; } 
            set { _height = value; } 
        }

        public int placementX 
        {
            get { return _placementX; } 
            set { _placementX = value; } 
        }
        public int placementY 
        { 
            get { return _placementY; } 
            set { _placementY = value; } 
        }

        //constructor
        public MapElement(int width, int height, int placementX, int placementY)
        {
            this.width = width;
            this.height = height;
            this.placementX = placementX;
            this.placementY = placementY;
        } 
    }
}
