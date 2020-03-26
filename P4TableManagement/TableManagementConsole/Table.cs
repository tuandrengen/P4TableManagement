using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public abstract class Table : IComparable<Table>
    {
		public int width { get; set; }
		public int height { get; set; }
		public int seats { get; set; }
		// tableNumber has been changed from private set; to protected set; 
		// as sub classes should be able to set this value as well
		public int tableNumber { get; set; }
		public int bookingID { get; set; }
		public string state { get; set; }

		private static int _tableID = 0;

		public int ID { get; set; }

		List<string> parameters = new List<string>();

		// public static List<Table> availableTables = new List<Table>();
		public static List<Table> tableList = new List<Table>();

		public Table()
		{
			// Read this link for more information about auto-incrementing a value.
			// https://stackoverflow.com/questions/8813435/incrementing-a-unique-id-number-in-the-constructor
			ID = System.Threading.Interlocked.Increment(ref _tableID);
			state = "Available";
		}

		public static void DeleteTableFromList(int id)
		{
			// .ToList and Linq library
			// https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
			foreach (Table item in tableList.ToList())
			{
				if (item.ID == id)
				{
					tableList.Remove(item);
				}
			}
		}

		// Assign booking to table
		// Need Booking.
		public void AssignTable(Booking booking)
		{
			if(this.state == "Occupied")
			{
				// Error message on screen.
				// throw new Exception("Error: Cannot assign an occupied table!");
				return;
			}
			bookingID = booking.id;
			state = "Occupied";
			Console.WriteLine($"Table #{ tableNumber } has been assigned! Booking ID: { bookingID }");
		}

		public void UnassignTable()
		{
			bookingID = default;
			state = "Available";
			Console.WriteLine($"Table #{ tableNumber } has been unassigned!");
		}

		public void PayTable()
		{
			state = "Paid";
			Console.WriteLine($"Table #{ tableNumber } has been paid! Booking ID: { bookingID }");
		}

		public void ReserveTable(Booking booking)
		{
			bookingID = booking.id;
			state = "Reserved";
			Console.WriteLine($"Table #{ tableNumber } has been reserved! Booking ID: { bookingID }");
		}

		public static List<Table> GetTableList(Predicate<Table> searchCriteria, List<Table> tableList)
		{
			List<Table> newList = tableList.FindAll(searchCriteria);
			return newList;
		}

		// Combines this object of a table with another table by deleting the two
		// tables from the tableList and then returning the new CombinedTable.
		public Table CombineTables(Table other, List<Table> tableList)
		{
			foreach (Table table in tableList.ToList())
			{
				if (table.tableNumber == this.tableNumber || table.tableNumber == other.tableNumber)
				{
					tableList.Remove(table);
				}
			}
			return new CombinedTable<Table>(this, other);
		}

		// Separates table by adding the two tables that were combined at first,
		// and adding them back to tableList.
		public static void SeparateTables(CombinedTable<Table> inputTable, List<Table> tableList)
		{
			foreach (Table table in inputTable.combinedTables)
			{
				tableList.Add(table);
			}
			tableList.Remove(inputTable);
		}

		public int CompareTo([AllowNull] Table other)
		{
			return ID.CompareTo(other.ID);
		}
	}
}
