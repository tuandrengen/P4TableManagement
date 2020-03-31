using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    public class MapSection
    {
		public int size { get; set; }
		public int sectionID { get; set; }
		public string visibility { get; set; }

		private static int _mapSectionID = 0;

		public static List<MapSection> mapSections = new List<MapSection>();

		public MapSection()
		{
			sectionID = System.Threading.Interlocked.Increment(ref _mapSectionID);
			this.visibility = "Inactive";
			AddMapSectionToList();
		}

		public void AddMapSectionToList()
		{
			// Directly adds to mapSections list.
			mapSections.Add(this);
		}

		//public void EditMapSection()
		//{
		//	throw new NotImplementedException();
		//}

		public void ChangeVisibility()
		{
			if (visibility == "Active")
			{
				visibility = "Inactive";
			}
			else
			{
				visibility = "Active";
			}
		}

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
