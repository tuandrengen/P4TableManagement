using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableManagementConsole;

namespace UnitTesting
{
    [TestClass]
    public class ReservationListTests
    {
        /*
         * Remember to check excel files when testing.
         * When you test DeleteReservation it deletes a row from the
         * excel file permanently.
         * That's why 'delete testing' are tested until it works, 
         * and then commented out.
         */
        [TestMethod]
        public void PopulateReservationList_PopulateReservationList_9Entries()
        {
            string path = @"C:\Users\tuant\OneDrive\Skrivebord\Database\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();
            List<Reservation> reservationList = new List<Reservation>();

            reservationList = list.PopulateReservationList(path, sheet);

            var expected = 8;
            var actual = reservationList.Count;

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void DeleteReservation_DeleteReservation_From9EntriesTo8()
        //{
        //    string path = @"C:\Users\tuant\OneDrive\Skrivebord\Database\test1.xlsx";
        //    int sheet = 1;
        //    ReservationList list = new ReservationList();
        //    List<Reservation> reservationList = new List<Reservation>();

        //    list.DeleteReservations(path, sheet, 2);
        //    reservationList = list.PopulateReservationList(path, sheet);

        //    var expected = 8;
        //    var actual = reservationList.Count;

        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        public void SortReservations_SortReservations_ReservationsSorted()
        {
            string path = @"C:\Users\tuant\OneDrive\Skrivebord\Database\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();
            
            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);
            reservationList = list.SortReservations(reservationList);


        }
    }
}
