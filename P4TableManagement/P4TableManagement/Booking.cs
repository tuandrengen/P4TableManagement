using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    public class Booking
    {
        //private static int _id = 0;

		public int id { get; set; }
		public int numberOfGuests { get; set; }
		public DateTime timeStart { get; set; }
		public DateTime timeEnd { get; set; }
		public bool isGap { get; set; }

		public bool isAssigned { get; set; }

		public Booking(int numberOfGuests, DateTime timeStart, bool is_gap)
        {
			this.numberOfGuests = numberOfGuests;
			this.timeStart = timeStart;
			timeEnd = timeStart.AddHours(2);
			isGap = is_gap;
			//id = System.Threading.Interlocked.Increment(ref _id);
        }
    }
}