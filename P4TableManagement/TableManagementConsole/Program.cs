using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReservationList list = new ReservationList();
            list.DeleteReservations(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1, 8);
        }
    }
}
