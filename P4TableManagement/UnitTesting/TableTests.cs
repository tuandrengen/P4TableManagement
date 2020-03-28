using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableManagementConsole;

namespace UnitTesting
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void Add_AddTablesToList_3Tables3InList()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 1 };
            Table table2 = new LargeTable() { tableNumber = 2 }; 
            Table table3 = new LargeTable() { tableNumber = 3 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);

            var expected = 3;
            var actual = tableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remove_RemoveTablesFromList_3Tables2InList1Removed()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 4 };
            Table table2 = new LargeTable() { tableNumber = 5 };
            Table table3 = new LargeTable() { tableNumber = 6 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table2.DeleteTableFromList(tableList);

            var expected = 2;
            var actual = tableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CombineTables_CombineTables_3Tables1Combined2Normal()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 7 };
            Table table2 = new SmallTable() { tableNumber = 8 };
            Table table3 = new SmallTable() { tableNumber = 9 };
            Table table4 = new SmallTable() { tableNumber = 10 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            tableList.Add(table1.CombineTables(table2, tableList));

            var expected = 3;
            var actual = tableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SeparateTables_SeparateTables_4Tables0Combined4Normal()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 11 };
            Table table2 = new SmallTable() { tableNumber = 12 };
            Table table3 = new SmallTable() { tableNumber = 13 };
            Table table4 = new SmallTable() { tableNumber = 14 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            Table table5 = table1.CombineTables(table2, tableList);
            table5.AddTableToList(tableList);
            Table.SeparateTables((CombinedTable<Table>)table5, tableList);

            var expected = 4;
            var actual = tableList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AssignTable_AssignTable_1Table1Assigned()
        {
            Table table1 = new LargeTable() { tableNumber = 15 };
            Booking booking1 = new Booking(4, DateTime.Parse("18:00"), false) { id = 1 };

            table1.AssignTable(booking1);

            var expected = "Occupied";
            var actual = table1.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PaidTable_GuestPayForTable_1Table1Paid()
        {
            Table table1 = new SmallTable() { tableNumber = 16 };

            table1.PayTable();

            var expected = "Paid";
            var actual = table1.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReservedTable_ReserveTable_1Table1Reserved()
        {
            Table table1 = new LargeTable() { tableNumber = 17 };
            Booking booking1 = new Booking(4, DateTime.Parse("20:30"), false) { id = 2 };

            table1.ReserveTable(booking1);

            var expected = "Reserved";
            var actual = table1.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnassignTable_UnassignTable_1Table1Available()
        {
            Table table1 = new SmallTable() { tableNumber = 18 };
            Booking booking1 = new Booking(2, DateTime.Parse("14:00"), false) { id = 3 };

            table1.AssignTable(booking1);
            table1.UnassignTable();

            var expected = "Available";
            var actual = table1.state;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchAvailableTables_4Tables3AvailableTables()
        {
            List<Table> tableList = new List<Table>();
            List<Table> availableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 19 };
            Table table2 = new SmallTable() { tableNumber = 20 };
            Table table3 = new SmallTable() { tableNumber = 21 };
            Table table4 = new LargeTable() { tableNumber = 22 };
            Booking booking1 = new Booking(2, DateTime.Parse("16:00"), false) { id = 4 };
            Predicate<Table> availableFinder = (Table t) => t.state.Equals("Available");

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            table1.AssignTable(booking1);
            availableList = Table.GetTableList(availableFinder, tableList);

            var expected = 3;
            var actual = availableList.Count;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetTableList_SearchOccupiedTables_4Tables3OccupiedTables()
        {
            List<Table> tableList = new List<Table>();
            List<Table> occupiedList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 19 };
            Table table2 = new SmallTable() { tableNumber = 20 };
            Table table3 = new SmallTable() { tableNumber = 21 };
            Table table4 = new LargeTable() { tableNumber = 22 };
            Booking booking1 = new Booking(2, DateTime.Parse("16:00"), false) { id = 5 };
            Booking booking2 = new Booking(2, DateTime.Parse("16:00"), false) { id = 6 };
            Booking booking3 = new Booking(2, DateTime.Parse("16:00"), false) { id = 7 };
            Predicate<Table> occupiedFinder = (Table t) => t.state.Equals("Occupied");

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            table1.AssignTable(booking1);
            table2.AssignTable(booking2);
            table3.AssignTable(booking3);
            occupiedList = Table.GetTableList(occupiedFinder, tableList);

            var expected = 3;
            var actual = occupiedList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchPaidTables_4Tables3PaidTables()
        {
            List<Table> tableList = new List<Table>();
            List<Table> paidList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 23 };
            Table table2 = new SmallTable() { tableNumber = 24 };
            Table table3 = new SmallTable() { tableNumber = 25 };
            Table table4 = new LargeTable() { tableNumber = 26 };
            Predicate<Table> paidFinder = (Table t) => t.state.Equals("Paid");

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            table1.PayTable();
            table2.PayTable();
            table3.PayTable();
            paidList = Table.GetTableList(paidFinder, tableList);

            var expected = 3;
            var actual = paidList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTableList_SearchReservedTables_4Tables3ReservedTables()
        {
            List<Table> tableList = new List<Table>();
            List<Table> reservedList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 19 };
            Table table2 = new SmallTable() { tableNumber = 20 };
            Table table3 = new SmallTable() { tableNumber = 21 };
            Table table4 = new LargeTable() { tableNumber = 22 };
            Booking booking1 = new Booking(2, DateTime.Parse("16:00"), false) { id = 5 };
            Booking booking2 = new Booking(2, DateTime.Parse("16:00"), false) { id = 6 };
            Booking booking3 = new Booking(2, DateTime.Parse("16:00"), false) { id = 7 };
            Predicate<Table> reservedFinder = (Table t) => t.state.Equals("Reserved");

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            table1.ReserveTable(booking1);
            table2.ReserveTable(booking2);
            table3.ReserveTable(booking3);
            reservedList = Table.GetTableList(reservedFinder, tableList);

            var expected = 3;
            var actual = reservedList.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_OrderByTableNumberASC_TablesOrderedInAscendingOrderByTableNumber()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 23 };
            Table table2 = new LargeTable() { tableNumber = 24 };
            Table table3 = new LargeTable() { tableNumber = 25 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            tableList.Sort((t1, t2) => t1.tableNumber.CompareTo(t2.tableNumber)); // Ascending Order

            List<Table> expectedList = new List<Table>() { table1, table2, table3 };
            List<Table> actualList = tableList;

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Sort_OrderByTableNumberDESC_TablesOrderedInDescendingOrderByTableNumber()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 26 };
            Table table2 = new LargeTable() { tableNumber = 27 };
            Table table3 = new LargeTable() { tableNumber = 28 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            tableList.Sort((t1,t2) => t2.tableNumber.CompareTo(t1.tableNumber)); // Descending Order

            List<Table> expected = new List<Table>() { table3, table2, table1 };
            List<Table> actual = tableList;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_OrderByTableStateASCThenTableNumberASC_TablesOrderedByTableStateASCTthenTableNumberASC()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 29 };
            Table table2 = new LargeTable() { tableNumber = 30 };
            Table table3 = new SmallTable() { tableNumber = 31 };
            Table table4 = new LargeTable() { tableNumber = 32 };
            Booking booking1 = new Booking(4, DateTime.Parse("12:00"), false) { id = 8 };
            Booking booking2 = new Booking(2, DateTime.Parse("13:00"), false) { id = 9 };

            table1.AddTableToList(tableList);
            table2.AddTableToList(tableList);
            table3.AddTableToList(tableList);
            table4.AddTableToList(tableList);
            table2.AssignTable(booking1);
            table3.AssignTable(booking2);
            
            tableList.Sort((t1, t2) =>
            {
                if (t1.state == t2.state)
                {
                    return t1.tableNumber.CompareTo(t2.tableNumber);
                }
                return t1.state.CompareTo(t2.state);
            });

            List<Table> expected= new List<Table>() { table1, table4, table2, table3 };
            List<Table> actual = tableList;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_OrderByTableStateASCThenTableNumberDESC_TablesOrderedByTableStateASCTthenTableNumberDESC()
        {
            List<Table> tableList = new List<Table>();
            Table table1 = new SmallTable() { tableNumber = 33 };
            Table table2 = new LargeTable() { tableNumber = 34 };
            Table table3 = new SmallTable() { tableNumber = 35 };
            Table table4 = new LargeTable() { tableNumber = 36 };
            Booking booking1 = new Booking(4, DateTime.Parse("12:00"), false) { id = 10 };
            Booking booking2 = new Booking(2, DateTime.Parse("13:00"), false) { id = 11 };

            tableList.Add(table1);
            tableList.Add(table2);
            tableList.Add(table3);
            tableList.Add(table4);
            table2.AssignTable(booking1);
            table3.AssignTable(booking2);

            tableList.Sort((t1, t2) =>
            {
                if (t1.state == t2.state)
                {
                    return t2.tableNumber.CompareTo(t1.tableNumber);
                }
                return t1.state.CompareTo(t2.state);
            });

            List<Table> expected = new List<Table>() { table4, table1, table3, table2 };
            List<Table> actual = tableList;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AssignTable_AssignAssignedTable_Booking2DoesNotOverwriteBooking1OnTable1()
        {
            Table table1 = new SmallTable() { tableNumber = 37 };
            Booking booking1 = new Booking(2, DateTime.Parse("12:00"), false) { id = 12 };
            Booking booking2 = new Booking(2, DateTime.Parse("12:00"), false) { id = 13 };

            table1.AssignTable(booking1);
            table1.AssignTable(booking2);

            Assert.AreEqual(table1.bookingID, 12);
        }
    }
}