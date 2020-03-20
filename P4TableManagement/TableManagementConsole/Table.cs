using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public class Table
    {
		public int width { get; set; }
		public int height { get; set; }
		public int seats { get; set; }
		public int tableNumber { get; private set; }

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
			this.ID = System.Threading.Interlocked.Increment(ref _tableID); // https://stackoverflow.com/questions/8813435/incrementing-a-unique-id-number-in-the-constructor
		}


		// Vent på UI, bliver kaldt med en knap (static)
		public void CreateSmallTable()
		{
			tableList.Add(new SmallTable());
		}

		// same as above
		public void CreateLargeTable()
		{
			tableList.Add(new LargeTable());
		}

		public static void DeleteTableFromList(int Id)
		{
			foreach (Table item in tableList.ToList()) // https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
			{
				if (item.ID == Id)
				{
					tableList.Remove(item);
				}
			}
		}

		public void AssignTable()
		{
			throw new NotImplementedException();
		}

		public void UnassignTable()
		{
			throw new NotImplementedException();
		}

		public void TableStateOccupied()
		{
			throw new NotImplementedException();
		}

		public void TableStateAvailable()
		{
			throw new NotImplementedException();
		}
		public void TableStatePaid()
		{
			throw new NotImplementedException();
		}

		public void TableStateReserved()
		{
			throw new NotImplementedException();
		}

		public void CombineTables()
		{
			throw new NotImplementedException();
		}

		public void SeparateTables()
		{
			throw new NotImplementedException();
		}
	}
}
