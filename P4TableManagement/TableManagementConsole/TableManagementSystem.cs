using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public class TableManagementSystem : ITableManagementSystem
    {
		// Most logic goes here
		// 1 - All writelines should be removed, as we do not write out to a console application.
		// 1a - Eventually make all the functions return a string so the UI can show it to the user.

		public List<Table> TableList { get { return Table.tableList; } } 

		public void AddTableToList(Table table)
		{
			TableList.Add(table);
		}

		public void AssignTable(Table table, Booking booking)
		{
			if (table.state == "Occupied")
			{
				throw new TableAlreadyAssignedException("Error: Cannot assign an occupied table!");
			}

			table.bookingID = booking.id;
			table.state = "Occupied";
			Console.WriteLine($"Table #{ table.tableNumber } has been assigned! Booking ID: { table.bookingID }");
		}

		public void UnassignTable(Table table)
		{
			table.bookingID = default;
			table.state = "Available";
			Console.WriteLine($"Table #{ table.tableNumber } has been unassigned!");
		}

		public void PayTable(Table table)
		{
			table.state = "Paid";
			Console.WriteLine($"Table #{ table.tableNumber } has been paid! Booking ID: { table.bookingID }");
		}

		public void ReserveTable(Table table, Booking booking)
		{
			table.bookingID = booking.id;
			table.state = "Reserved";
			Console.WriteLine($"Table #{ table.tableNumber } has been reserved! Booking ID: { table.bookingID }");
		}

		public List<Table> GetTableList(Predicate<Table> searchCriteria)
		{
			List<Table> newList = TableList.FindAll(searchCriteria);
			return newList;
		}

		// Combines this object of a table with another table by deleting the two
		// tables from the tableList and then returning the new CombinedTable.
		public Table CombineTables(Table one, Table two)
		{
			foreach (Table table in TableList)
			{
				if (table.tableNumber == one.tableNumber || table.tableNumber == two.tableNumber)
				{
					TableList.Remove(table);
				}
			}

			return new CombinedTable<Table>(one, two);
		}

		// Separates table by adding the two tables that were combined at first,
		// and adding them back to tableList.
		public void SeparateTables(CombinedTable<Table> combinedTable)
		{
			foreach (Table table in combinedTable.combinedTables)
			{
				TableList.Add(table);
			}

			TableList.Remove(combinedTable);
		}

		public void DeleteTableFromList(Table table)
		{
			TableList.Remove(table);
			//// .ToList and Linq library
			//// https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
			//foreach (Table table in tableList.ToList())
			//{
			//	if (table.ID == id)
			//	{
			//		tableList.Remove(item);
			//	}
			//}
		}
	}
}
