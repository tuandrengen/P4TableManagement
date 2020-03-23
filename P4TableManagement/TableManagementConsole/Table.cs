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

		//constructor
		//public Table(int width, int height, int placementX, int placementY, int seats, int tableNumber, List<string> parameter) : base(width, height, placementX, placementY)
		//{
		//          this.seats = seats;
		//          this.tableNumber = tableNumber;
		//          this.parameter.AddRange(parameter);
		//      } Kommet i kommentar grundet: Mange af parameterne skal ikke sættes her, f.eks. tableNumber burde bare få tildelt et nummer automatisk og ikke manuelt.
		
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
			bookingID = booking.id;
		}

		public void UnassignTable()
		{
			bookingID = default;
		}

		public static List<Table> GetTableList(Predicate<Table> searchCriteria)
		{
			List<Table> newList = tableList.FindAll(searchCriteria);

			return newList;
		}

		public static Table CombineTables(Table one, Table two)
		{
			foreach (Table table in tableList.ToList())
			{
				if (table.tableNumber == one.tableNumber || table.tableNumber == two.tableNumber)
				{
					tableList.Remove(table);
				}
			}
			return new CombinedTable<Table>(one, two);
		}

		public static void SeparateTables(Table one)
		{
			throw new NotImplementedException();
		}

		public int CompareTo([AllowNull] Table other)
		{
			return ID.CompareTo(other.ID);
		}
	}
}
