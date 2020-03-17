using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Table
    {
        private int _seats;
		private int _tableNumber;
		private string _state;
		private int _bookingID;
		public List<string> parameter = new List<string>();

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



    }
}
