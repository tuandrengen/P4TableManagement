using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    abstract class Booking
    {
        private int _id;
		private int _numberOfGuests;
		private DateTime _timeStart;
		private DateTime _timeEnd;
		private bool _isGap;

        //properties
	    public int id
	    {
		    get { return _id;}
		    set { _id = value;}
	    }
    
	    public int numberOfGuests
	    {
		    get { return _numberOfGuests;}
		    set { _numberOfGuests = value;}
	    }

		public DateTime timeStart
		{
			get { return _timeStart;}
			set { _timeStart = value;}
		}

		public DateTime timeEnd
		{
			get { return _timeEnd;}
			set { _timeEnd = value;}
		}

		public bool isGap
		{
			get { return _isGap;}
			set { _isGap = value;}
		}

		// Constructor
        public Booking(int numberOfGuests, DateTime timeStart, bool is_gap)
        {
			this.numberOfGuests = numberOfGuests;
			this.timeStart = timeStart;
			isGap = is_gap;
        }

        // Metoder

		public abstract T AddBooking<T>();
        public abstract T EditBooking<T>();
        public abstract void DeleteBooking();
        public abstract void AssignBooking();
    }
}
