using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class MapElement
    {
        private int _size;
        private int _placementX;
        private int _placementY;

        public int size 
        { 
            get {return _size;} 
            set {_size = value;} 
        }

        public int placementX 
        {
            get {return _placementX;} 
            set{ _placementX = value;} 
        }
        public int placementY 
        { 
            get {return _placementY;} 
            set{_placementY = value;} 
        }

    }
}
