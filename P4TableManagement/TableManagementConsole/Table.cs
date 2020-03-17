using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Table
    {
        private int _seats;

		public int seats
		{
			get { return _seats;}
			set { _seats = value;}
		}
		private int _tableNumber;

		public int tableNumber
		{
			get { return _tableNumber;}
			set { _tableNumber = value;}
		}
		List<string> parameter = new List<string>();

    }
}
