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
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test3.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();
            List<Reservation> reservationList = new List<Reservation>();

            reservationList = list.PopulateReservationList(path, sheet);

            var expected = 7;
            var actual = reservationList.Count;

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void DeleteReservation_DeleteReservation_From9EntriesTo8()
        //{
        //    string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
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
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test1.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();
            
            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            List<Reservation> expectedList = new List<Reservation>
            {
                reservationList[1],
                reservationList[0],
                reservationList[2]
            };

            reservationList = list.SortReservations(reservationList);
            List<Reservation> actualList = reservationList;

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void EditReservation_()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test2.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();
            list.EditReservations(path, sheet, 1, 1);

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 8;
            var actual = reservationList[0].numberOfGuests;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FilterBySpecificNumberOfGuests_Filter_ReservationsFilteredBySpecificNumberOfGuests()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 2;
            var actual = list.FilterBySpecificNumberOfGuests(reservationList, 2).Count;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FilterByRangeNumberOfGuests_Filter_FilteredByRangeNumberOfGuests()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 5;
            var actual = list.FilterByRangeNumberOfGuests(reservationList, 4, 2).Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FilterBySpecificTimeStart_filter_FilteredBySpecificTimeStart()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 2;
            var actual = list.FilterBySpecificTimeStart(reservationList, "17:30").Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FilterByRangeTimeStart_filter_FilteredByRangeTimeStart()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 6;
            var actual = list.FilterByRangeTimeStart(reservationList, "18:30", "16:30").Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FilterBySpecificParameter_Filter_FilteredBySpecificParameter()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 6;
            var actual = list.FilterBySpecificParameter(reservationList, "1 højstole").Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FilterByMoreParameters_Filter_FilteredByMoreParameters()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            List<String> parameters = new List<string>() {"1 højstole", "ved casino" };

            var expected = 3;
            var actual = list.FilterByMoreParameters(reservationList, parameters).Count;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void FilterByisgap_Filter_FilteredByisgap()
        {
            string path = @"C:\Users\tuant\source\repos\P4TableManagement\P4TableManagement\UnitTesting\Test\test.xlsx";
            int sheet = 1;
            ReservationList list = new ReservationList();

            List<Reservation> reservationList = list.PopulateReservationList(path, sheet);

            var expected = 4;
            var actual = list.FilterByisgap(reservationList, true).Count;

            Assert.AreEqual(expected, actual);
        }

    }
}
