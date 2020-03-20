using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Reservation : Booking
    {
        private string _name;
		private int _phoneNumber;
		private string _comment;
		public List<string> parameter = new List<string>();

        // her er properties
		public string name 
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

        //her er alle constructors
		public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber) : base(numberOfGuest, timeStart, isGap)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
		}

		public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber, List<string> parameter) : this(name, timeStart, isGap, numberOfGuest, phoneNumber)
		{
			this.parameter.AddRange(parameter);
		}

        public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber, string comment) : this(name, timeStart, isGap, numberOfGuest, phoneNumber)
        {
            this.comment = comment;
        }

        public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber, List<string> parameter, string comment) : this(name, timeStart, isGap, numberOfGuest, phoneNumber)
        {
            this.parameter.AddRange(parameter);
            this.comment = comment;
        }

        //her starter metoder ~~(* o * ~）
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