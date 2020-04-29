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
		public List<string> parameter = new List<string>();

		public string name { get; set; }
		public int phoneNumber { get; set; }
		public string comment { get; set; }
		public string stringTime{ get; set; }

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

    }
}