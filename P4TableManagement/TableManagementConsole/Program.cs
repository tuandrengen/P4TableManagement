using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReservationList list = new ReservationList();
            string path = @"C:\Users\T-Phamz\Desktop\test\test.xlsx";

            List<Reservation> reservationList = list.PopulateReservationList(path, 1);
            Console.WriteLine("test ");
            var test = list.FilterBySpecificParameter(reservationList, "1 højstole");
            //list.DeleteReservations(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1, 8);
            //list.EditReservations(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1, 1, 2);

        }
    }


}
