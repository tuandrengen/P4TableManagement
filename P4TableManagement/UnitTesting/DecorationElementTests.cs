using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableManagementConsole;


namespace P4TableManagement
{
    [TestClass]
    public class DecorationElementTests
    {
        [TestMethod]
        public void CreateDecorationElement_CreateDecorationElement_1DEinList()
        {
            List<DecorationElement> deList = new List<DecorationElement>();
            DecorationElement bar = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);
            
            bar.CreateDecorationElement(deList);

            var expected = 1;
            var actual = deList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDecorationElement_CreateDEofSameName_1DEinListAsDuplicateWasNotCreated()
        {
            List<DecorationElement> deList = new List<DecorationElement>();
            DecorationElement bar1 = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);
            DecorationElement bar2 = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);

            bar1.CreateDecorationElement(deList);
            bar2.CreateDecorationElement(deList);

            var expected = 1;
            var actual = deList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDecorationElement_Create2DifferentDE_2DEinList()
        {
            List<DecorationElement> deList = new List<DecorationElement>();
            DecorationElement bar = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);
            DecorationElement aquarium = new DecorationElement("Aquarium", 2, 2, 2, 2, 2, 2);

            bar.CreateDecorationElement(deList);
            aquarium.CreateDecorationElement(deList);

            var expected = 2;
            var actual = deList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteDecorationElement_DeleteDecorationElement_Create2DEDelete1DE1DEinList()
        {
            List<DecorationElement> deList = new List<DecorationElement>();
            DecorationElement bar = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);
            DecorationElement aquarium = new DecorationElement("Aquarium", 2, 2, 2, 2, 2, 2);

            bar.CreateDecorationElement(deList);
            aquarium.CreateDecorationElement(deList);
            aquarium.DeleteDecorationElement(deList);

            var expected = 1;
            var actual = deList.Count;

            Assert.AreEqual(expected, actual);
        }
    }
}
