using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    public class TableMap
    {
        public static List<MapSection> tableMap = new List<MapSection>();

        // Called when refreshing list
        private void RefreshListOfMapSections()
        {
            ClearList();
            // Takes every element in MapSection.mapSections list
            // and adds it to tableMap list.
            foreach (MapSection mapSection in MapSection.mapSections)
            {
                tableMap.Add(mapSection);
            }
        }

        private void RemoveSection(int id)
        {
            foreach (MapSection mapSection in MapSection.mapSections)
            {
                if (mapSection.sectionID == id)
                {
                    MapSection.mapSections.Remove(mapSection);
                }
            }
            RefreshListOfMapSections();
        }

        public void ClearList()
        {
            // Clears all the content
            tableMap.Clear();
            // Releases all the memory allocated to the list.
            tableMap.TrimExcess();
        }
        
        //public bool Active()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
