using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReservationList list = new ReservationList();
            Excel excel = new Excel(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            list.CreateReservation(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            list.PopulateReservationList(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);

            //list.AutomaticCreateReservation(@"C:\Users\T-Phamz\Desktop\test\sourcetest.xlsx", 1, @"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            //list.PopulateReservationList(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);


        }
    }
}
