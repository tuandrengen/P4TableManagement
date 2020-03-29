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
        public void GetAllMapElements_AddAllMapElementsToList_3MapElements2Tables1DecorationElement()
        {
            List<Table> tableList = new List<Table>();
            List<DecorationElement> deList = new List<DecorationElement>();

            tableList.Add(new SmallTable());
            tableList.Add(new LargeTable());

            deList.Add(new DecorationElement("Bar", 2, 2, 2, 2, 2, 2));

            MapElement.GetAllMapElements(tableList, deList);

            var expected = 3;
            var actual = MapElement.mapElementList.Count;

            Assert.AreEqual(expected, actual);
        }
    }
}