using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public abstract class Table
    {
		public int width { get; set; }
		public int height { get; set; }
		public int seats { get; set; }
		public int tableNumber { get; private set; }
		public int bookingID { get; set; }
		public string state { get; set; }

		private static int _tableID = 0;

		public int ID { get; set; }

		List<string> parameters = new List<string>();


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

		public static void DeleteTableFromList(int Id)
		{
			// .ToList and Linq library
			// https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
			foreach (Table item in tableList.ToList())
			{
				if (item.ID == Id)
				{
					tableList.Remove(item);
				}
			}
		}

		public void AssignTable(Booking booking)
		{
			// Need booking
			// BookingID = booking.ID;
			throw new NotImplementedException();
		}

		public void UnassignTable()
		{
			bookingID = default;
			throw new NotImplementedException();
		}

		public void TableStateOccupied()
		{
			state = "Occupied";
			throw new NotImplementedException();
		}

		public void TableStateAvailable()
		{
			state = "Available";
			throw new NotImplementedException();
		}
		public void TableStatePaid()
		{
			state = "Paid";
			throw new NotImplementedException();
		}

		public void TableStateReserved()
		{
			state = "reserved";
			throw new NotImplementedException();
		}

		public static void CombineTables(Table one, Table two)
		{
			throw new NotImplementedException();
		}

		public static void SeparateTables(Table one, Table two)
		{
			throw new NotImplementedException();
		}
	}
}
