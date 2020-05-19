using System;
using System.Collections.Generic;
using System.Text;

namespace P4TableManagement
{
    class WalkIn : Booking
    {
        public WalkIn(int numberOfGuest, bool isGap) : base(numberOfGuest, DateTime.Now, isGap) { }
    }
}