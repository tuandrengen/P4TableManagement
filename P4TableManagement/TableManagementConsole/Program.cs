using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel test = new Excel(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            List<Reservation> list = test.ReadCell();
            foreach (var reservation in list)
            {
                Console.WriteLine($"{reservation.name}, {reservation.numberOfGuests}, {reservation.phoneNumber}, {reservation.timeStart.ToString("HH:mm")}, {reservation.comment}, {string.Join(", ", reservation.parameter)}");
            }
            //List<string> res = test.ReadCell();
            //res.ForEach(Console.Write);
        }
    }
}
