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

		public MapSection()
		{
			sectionID = System.Threading.Interlocked.Increment(ref _mapSectionID);
			this.visibility = "Inactive";
			AddMapSectionToTableMap();
		}

		public void AddMapSectionToTableMap()
		{
			// Directly adds to TableMap.tableMap list.
			TableMap.tableMap.Add(this); 
		}

		public void DeleteMapSection()
		{
			// Directly removes from TableMap.tableMap list.
			TableMap.tableMap.Remove(this); 
		}

		public void EditMapSection()
		{
			throw new NotImplementedException();
		}

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

		public void UpdateMapSection() // ???
		{
			throw new NotImplementedException();
		}

		public void ResetMapSection()
		{
			throw new NotImplementedException();
		}
	}
}
