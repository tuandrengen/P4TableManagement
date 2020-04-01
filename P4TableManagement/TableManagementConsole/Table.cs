using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Table : MapElement
    {
        private int _seats;
		private int _tableNumber;
		private string _state;
		private int _bookingID;
		public List<string> parameter = new List<string>();

        //properties
		public int seats
		{
			get { return _seats;}
			set { _seats = value;}
		}
		
		public int tableNumber
		{
			get { return _tableNumber;}
			set { _tableNumber = value;}
		}

		public string state
		{
			get { return _state;}
			set { _state = value;}
		}

		public int bookingID
		{
			get { return _bookingID;}
			set { _bookingID = value;}
		}

        //constructor
		public Table(int width, int height, int placementX, int placementY, int seats, int tableNumber, List<string> parameter) : base(width, height, placementX, placementY)
		{
            this.seats = seats;
            this.tableNumber = tableNumber;
            this.parameter.AddRange(parameter);
        }

		public Table AddTable()
		{
			throw new NotImplementedException();
		}

		public void DeleteTable()
		{
			throw new NotImplementedException();
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
