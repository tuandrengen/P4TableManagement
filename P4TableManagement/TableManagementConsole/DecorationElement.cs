using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class DecorationElement : MapElement
    {
        private int _sizeOfZone;

		public int sizeOfZone
		{
			get { return _sizeOfZone;}
			set { _sizeOfZone = value;}
		}
        private string _name;

		public string name
		{
			get { return _name;}
			set { _name = value;}
		}



    }
}
