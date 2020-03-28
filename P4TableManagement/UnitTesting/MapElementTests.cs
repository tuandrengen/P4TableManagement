using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TableManagementConsole;

namespace UnitTesting
{
    [TestClass]
    public class MapElementTests
    {
        [TestMethod]
        public void GetAllMapElements_AddAllMapElementsToList_1()
        {
            List<Table> tableList = new List<Table>();
            List<DecorationElement> deList = new List<DecorationElement>();
            //List<MapElement> meList = new List<MapElement>();
            Table table1 = new SmallTable();
            Table table2 = new LargeTable();

            DecorationElement de1 = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);

            smallTable.AddMapElementToList(meList);

            var expected = 1;
            var actual = meList.Count;

            Assert.AreEqual(expected, actual);
        }
    }
}