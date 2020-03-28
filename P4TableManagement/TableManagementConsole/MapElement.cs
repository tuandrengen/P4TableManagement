using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    public abstract class MapElement
    {
        //properties
        public int width { get; set; }
        public int height { get; set; }
        public int placementX { get; set; }
        public int placementY { get; set; }

        static List<MapElement> mapElementList = new List<MapElement>();

        //constructor
        public MapElement(int width, int height, int placementX, int placementY)
        {
            this.width = width;
            this.height = height;
            this.placementX = placementX;
            this.placementY = placementY;
        }

        public void GetAllMapElements(List<Table> tableList, List<DecorationElement> deList)
        {
            mapElementList.Clear();
            mapElementList.TrimExcess();
            foreach (Table table in tableList)
            {
                mapElementList.Add(table);
            }
            foreach (DecorationElement de in deList)
            {
                mapElementList.Add(de);
            }
        }

        public void AddMapElementToList(List<MapElement> meList)
        {
            meList.Add(this);
        }

        public void DeleteMapElementFromList(List<MapElement> meList)
        {
            meList.Remove(this);
        }
    }
}
