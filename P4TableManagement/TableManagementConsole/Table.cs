using System;
using System.Collections.Generic;
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

		public int ID
		{
			get { return _tableID; }
			set { _tableID = value; }
		}
        
		List<string> parameters = new List<string>();


		public List<Table> tableList = new List<Table>();

		//constructor
		//public Table(int width, int height, int placementX, int placementY, int seats, int tableNumber, List<string> parameter) : base(width, height, placementX, placementY)
		//{
		//          this.seats = seats;
		//          this.tableNumber = tableNumber;
		//          this.parameter.AddRange(parameter);
		//      } Kommet i kommentar grundet: Mange af parameterne skal ikke sættes her, f.eks. tableNumber burde bare få tildelt et nummer automatisk og ikke manuelt.
		
		public Table()
		{
            ID++;
            _tableID = ID;
        }
		

		public void CreateSmallTable()
		{
			tableList.Add(new SmallTable());
		}

		public void CreateLargeTable()
		{
			tableList.Add(new LargeTable());
		}

		public void DeleteTableFromList(Table table)
		{
			foreach (Table item in this.tableList)
			{
				if (item.tableNumber == table.tableNumber)
				{
					this.tableList.Remove(item);
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
