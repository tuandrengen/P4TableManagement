using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class MapSection
    {
		private int _size;
		private int _sectionID;

		//properties 
		public int size
		{
			get { return _size; }
			set { _size = value; }
		}
		
		public int sectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
		}
    }
}
