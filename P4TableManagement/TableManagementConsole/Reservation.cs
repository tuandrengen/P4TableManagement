using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Reservation : Booking
    {
        private int _name;
		private int _phoneNumber;
		private string _comment;
		public List<string> parameter = new List<string>();

		public int name 
		{
			get { return _name;}
			set { _name = value;}
		}

		public int phoneNumber
		{
			get { return _phoneNumber;}
			set { _phoneNumber = value;}
		}

		public string comment
		{
			get { return _comment;}
			set { _comment = value;}
		}

		public void ManualReservation()
		{
			throw new NotImplementedException();
		}

		public void AutomaticReservation()
		{
			throw new NotImplementedException();
		}




    }
}
