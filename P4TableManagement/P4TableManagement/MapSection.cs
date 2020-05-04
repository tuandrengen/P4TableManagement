using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public class MapSection
    {
		private static int _mapSectionID = 0;

		public int size { get; set; }
		public int sectionID { get; set; }
		public bool visibility { get; set; }

		public MapSection()
		{
			sectionID = System.Threading.Interlocked.Increment(ref _mapSectionID);
			visibility = true;
		}

		//public void EditMapSection()
		//{
		//	throw new NotImplementedException();
		//}

		//public void UpdateMapSection() // ???
		//{
		//	throw new NotImplementedException();
		//}

		//public void ResetMapSection()
		//{
		//	throw new NotImplementedException();
		//}
	}
}
