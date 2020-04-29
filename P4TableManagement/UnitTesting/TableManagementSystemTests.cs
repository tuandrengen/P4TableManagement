using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableManagementConsole;


namespace UnitTesting
{
    [TestClass]
    public class TableManagementSystemTests
    {
        ITableManagementSystem tablemanagement;

        Table table1;
        Table table2;
        Table table3;
        Table table4;
        DecorationElement bar1;
        DecorationElement bar2;
        DecorationElement aquarium;

        Booking booking1;
        Booking booking2;
        Booking booking3;

        [TestInitialize]
        public void TestInitialize()
        {
            tablemanagement = new TableManagementSystem();

            table1 = new SmallTable();
            table2 = new LargeTable();
            table3 = new LargeTable();
            table4 = new SmallTable();

            aquarium = new DecorationElement("Aquarium", 2, 2, 2, 2, 2, 2);
            bar1 = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);
            bar2 = new DecorationElement("Bar", 2, 2, 2, 2, 2, 2);

            booking1 = new Booking(4, DateTime.Parse("18:00"), false);
            booking2 = new Booking(4, DateTime.Parse("16:00"), false);
            booking3 = new Booking(4, DateTime.Parse("19:30"), false);
        }


        [TestMethod]
        public void CreateDecorationElement_CreateDecorationElement_1DEinList()
        {   
            tablemanagement.CreateDecorationElement(bar1);

            var expected = 1;
            var actual = tablemanagement.DeList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDecorationElement_CreateDEofSameName_1DEinListAsDuplicateWasNotCreated()
        {
            tablemanagement.CreateDecorationElement(bar1);
            tablemanagement.CreateDecorationElement(bar2);

            var expected = 1;
            var actual = tablemanagement.DeList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDecorationElement_Create2DifferentDE_2DEinList()
        {
            tablemanagement.CreateDecorationElement(bar1);
            tablemanagement.CreateDecorationElement(aquarium);

            var expected = 2;
            var actual = tablemanagement.DeList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteDecorationElement_DeleteDecorationElement_Create2DEDelete1DE1DEinList()
        {
            tablemanagement.CreateDecorationElement(bar1);
            tablemanagement.CreateDecorationElement(aquarium);
            tablemanagement.DeleteDecorationElement(aquarium);

            var expected = 1;
            var actual = tablemanagement.DeList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAllMapElements_AddAllMapElementsToList_3MapElements2Tables1DecorationElement()
        {
            tablemanagement.TableList.Add(table1);
            tablemanagement.TableList.Add(table2);

            tablemanagement.DeList.Add(bar1);

            tablemanagement.GetAllMapElements();

            var expected = 3;
            var actual = tablemanagement.MeList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Add_AddTablesToList_3Tables3InList()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);

            var expected = 3;
            var actual = tablemanagement.TableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remove_RemoveTablesFromList_3Tables2InList1Removed()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.DeleteTableFromList(table2);

            var expected = 2;
            var actual = tablemanagement.TableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CombineTables_CombineTables_3Tables1Combined2Normal()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            tablemanagement.TableList.Add(tablemanagement.CombineTables(table1, table2));

            var expected = 3;
            var actual = tablemanagement.TableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SeparateTables_SeparateTables_4Tables0Combined4Normal()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            Table table5 = tablemanagement.CombineTables(table1, table2);
            tablemanagement.AddTableToList(table5);
            tablemanagement.SeparateTables((CombinedTable<Table>)table5);

            var expected = 4;
            var actual = tablemanagement.TableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AssignTable_AssignTable_1Table1Assigned()
        {
            tablemanagement.AssignTable(table2, booking1);

            var expected = "Occupied";
            var actual = table2.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PaidTable_GuestPayForTable_1Table1Paid()
        {
            tablemanagement.PayTable(table1);

            var expected = "Paid";
            var actual = table1.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReservedTable_ReserveTable_1Table1Reserved()
        {
            tablemanagement.ReserveTable(table2, booking1);

            var expected = "Reserved";
            var actual = table2.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnassignTable_UnassignTable_1Table1Available()
        {
            tablemanagement.AssignTable(table1, booking1);
            tablemanagement.UnassignTable(table1);

            var expected = "Available";
            var actual = table1.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchAvailableTables_4Tables3AvailableTables()
        {
            List<Table> availableList = new List<Table>();
            Predicate<Table> availableFinder = (Table t) => t.state.Equals("Available");

            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            tablemanagement.AssignTable(table1, booking1);
            availableList = tablemanagement.GetTableList(availableFinder);

            var expected = 3;
            var actual = availableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchOccupiedTables_4Tables3OccupiedTables()
        {
            List<Table> occupiedList = new List<Table>();
            Predicate<Table> occupiedFinder = (Table t) => t.state.Equals("Occupied");

            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            tablemanagement.AssignTable(table1, booking1);
            tablemanagement.AssignTable(table2, booking2);
            tablemanagement.AssignTable(table3, booking3);
            occupiedList = tablemanagement.GetTableList(occupiedFinder);

            var expected = 3;
            var actual = occupiedList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchPaidTables_4Tables3PaidTables()
        {
            Predicate<Table> paidFinder = (Table t) => t.state.Equals("Paid");

            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            tablemanagement.PayTable(table1);
            tablemanagement.PayTable(table2);
            tablemanagement.PayTable(table3);
            List<Table> paidList = tablemanagement.GetTableList(paidFinder);

            var expected = 3;
            var actual = paidList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchReservedTables_4Tables3ReservedTables()
        {
            Predicate<Table> reservedFinder = (Table t) => t.state.Equals("Reserved");

            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            tablemanagement.ReserveTable(table1, booking1);
            tablemanagement.ReserveTable(table2, booking2);
            tablemanagement.ReserveTable(table3, booking3);
            List<Table> reservedList = tablemanagement.GetTableList(reservedFinder);

            var expected = 3;
            var actual = reservedList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_OrderByTableNumberASC_TablesOrderedInAscendingOrderByTableNumber()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.TableList.Sort((t1, t2) => t1.tableNumber.CompareTo(t2.tableNumber)); // Ascending Order

            List<Table> expectedList = new List<Table>() { table1, table2, table3 };
            List<Table> actualList = tablemanagement.TableList;

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Sort_OrderByTableNumberDESC_TablesOrderedInDescendingOrderByTableNumber()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.TableList.Sort((t1, t2) => t2.tableNumber.CompareTo(t1.tableNumber)); // Descending Order

            List<Table> expected = new List<Table>() { table3, table2, table1 };
            List<Table> actual = tablemanagement.TableList;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_OrderByTableStateASCThenTableNumberASC_TablesOrderedByTableStateASCTthenTableNumberASC()
        {
            tablemanagement.AddTableToList(table1);
            tablemanagement.AddTableToList(table2);
            tablemanagement.AddTableToList(table3);
            tablemanagement.AddTableToList(table4);
            tablemanagement.AssignTable(table2, booking1);
            tablemanagement.AssignTable(table1, booking2);

            tablemanagement.TableList.Sort((t1, t2) =>
            {
                if (t1.state == t2.state)
                {
                    return t1.tableNumber.CompareTo(t2.tableNumber);
                }
                return t1.state.CompareTo(t2.state);
            });

            List<Table> expected = new List<Table>() { table3, table4, table1, table2};
            List<Table> actual = tablemanagement.TableList;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_OrderByTableStateASCThenTableNumberDESC_TablesOrderedByTableStateASCTthenTableNumberDESC()
        {
            tablemanagement.TableList.Add(table1);
            tablemanagement.TableList.Add(table2);
            tablemanagement.TableList.Add(table3);
            tablemanagement.TableList.Add(table4);
            tablemanagement.AssignTable(table2, booking1);
            tablemanagement.AssignTable(table3, booking2);

            tablemanagement.TableList.Sort((t1, t2) =>
            {
                if (t1.state == t2.state)
                {
                    return t2.tableNumber.CompareTo(t1.tableNumber);
                }
                return t1.state.CompareTo(t2.state);
            });

            List<Table> expected = new List<Table>() { table4, table1, table3, table2 };
            List<Table> actual = tablemanagement.TableList;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AssignTable_AssignAssignedTable_Booking2DoesNotOverwriteBooking1OnTable1()
        {
            tablemanagement.AssignTable(table1, booking1);
            tablemanagement.AssignTable(table1, booking2);

            Assert.AreEqual(table1.bookingID, 1);
        }
    }
}
