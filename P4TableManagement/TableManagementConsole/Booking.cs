using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Booking
    {

        private int _id;
		private int _numberOfGuests;
		private DateTime _timeStart;
		private DateTime _timeEnd;
		private bool _isGap;

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







    }
}
