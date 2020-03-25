using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReservationList list = new ReservationList();
            //Excel excel = new Excel(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            //excel.WriteToCell(4, 2, "Kim");
            //excel.Save();
            //excel.Close();
            //list.PopulateReservationList(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            //excel.Quit();
            list.CreateReservation(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            

        }
    }
}
