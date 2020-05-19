using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public class Reservation : Booking
    {
        private string _name;
		private int _phoneNumber;
		private string _comment;
		public string parameter;

		public string name { get; set; }
		public int phoneNumber { get; set; }
		public string comment { get; set; }
		public string stringTime{ get; set; }

		public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber) : base(numberOfGuest, timeStart, isGap)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
		}

		//public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber, List<string> parameter) : this(name, timeStart, isGap, numberOfGuest, phoneNumber)
		//{
		//	this.parameter = parameter;
		//}

  //      public Reservation(string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber, string comment) : this(name, timeStart, isGap, numberOfGuest, phoneNumber)
  //      {
  //          this.comment = comment;
  //      }

        public Reservation(int id, string name, DateTime timeStart, bool isGap, int numberOfGuest, int phoneNumber, string parameter, string comment) : this(name, timeStart, isGap, numberOfGuest, phoneNumber)
        {
			this.parameter = parameter;
            this.comment = comment;
			this.id = id;
        }

    }
}