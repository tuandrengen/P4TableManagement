using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace P4TableManagement
{
    public class ReservationList
        // mangler metoder som skaber ny sheet for hver dag
        // og en metode som skaber en ny workbook for hver måned
    {
        public List<Reservation> listReservation = new List<Reservation>();
        
        //manuelt indtastning af data ind i database
        //skriver ikke parameter og kommentar ind. 
        public void CreateReservation(string path, int sheet)
        {
            Excel excel = new Excel(path, sheet);

            Console.WriteLine("Enter string: ");
            string s = Console.ReadLine();
            string[] seperator = { "p, ", ", ", "p " };

            string[] list = s.Split(seperator, 4, StringSplitOptions.None);

            excel.WriteToRow(list);

            excel.Quit();
        }

        //læser fra en anden database(online bestilling) og inputter ind i vorse database(excel)
        public void AutomaticCreateReservation(string sourcepath, int sourcesheet, string targetpath, int targetsheet)
        {
            //MISSING lav en metode som tjekker om en reservation fra source allerede er i target
            Excel source = new Excel(sourcepath, sourcesheet);
            Excel target = new Excel(targetpath, targetsheet);

            //Read from another database (excel)
            List<Reservation> sourcelist = source.ReadCell();
            source.Quit();

            //write to reservation's database (excel)
            target.ImportReservationList(sourcelist);

            target.Quit();
        }


        //indtager data fra databasen, laver daten om til objekter(reservations) og putter dem i en liste
        public List<Reservation> PopulateReservationList(string path, int sheet)
        {
            Excel excel = new Excel(path, sheet);
            List<Reservation> list = excel.ReadCell();

            list = SortReservations(list);

            //printer listen skal fjernes senere
            foreach (var reservation in list)
            {
                Console.WriteLine($"{reservation.numberOfGuests}p, {reservation.name},  {reservation.phoneNumber}, {reservation.timeStart.ToString("HH:mm")} , {string.Join(", ", reservation.parameter)} , {reservation.comment}");
            }

            excel.Quit();

            return list;
        }

        //sletter reservationer
        public void DeleteReservations(string path, int sheet, int row)
        {
            Excel excel = new Excel(path, sheet);
            excel.Delete(row);
            excel.Quit();
        }

        //ændre data hos en allerede eksisterende
        public void EditReservations(string path, int sheet, int row, int column)
        {
            //i GUI kan man vaelge en reservation dvs. en bestemt row i excel, 
            //ind i edit s[ vaelge man 
            Excel excel = new Excel(path, sheet);
            string input = Console.ReadLine();
            excel.WriteToCell(row, column, input);
            excel.Quit();
        }

        // sortere reservationer
        public List<Reservation> SortReservations(List<Reservation> list)
        {
            List<Reservation> sortedlist = list.OrderBy(res => res.timeStart).ThenBy(res => res.numberOfGuests).ToList();
            return sortedlist;
        }


        // Filtrer reservationer
        public List<Reservation> FilterBySpecificNumberOfGuests(List<Reservation> list, int i)
        {
            List<Reservation> filteredList = list.Where(x => x.numberOfGuests == i).ToList();
            return filteredList; 
        }

        public List<Reservation> FilterByRangeNumberOfGuests(List<Reservation> list, int max, int min)
        {
            List<Reservation> filteredList = list.Where(x => x.numberOfGuests <= max).Where(x => x.numberOfGuests >= min).ToList();
            return filteredList;
        }

        public List<Reservation> FilterBySpecificTimeStart(List<Reservation> list, string timestart)
        {
            //30/12/1899 is the default date and since the excel only has the time, it automatically used the default date. So in order to compare we also change it here
            DateTime specificTime = DateTime.Parse("30/12/1899 " + timestart);
            List<Reservation> filteredList = list.Where(x => x.timeStart.ToString("HH:mm") == timestart).ToList();
            return filteredList;
        }

        public List<Reservation> FilterByRangeTimeStart(List<Reservation> list, string max, string min)
        {
            //30/12/1899 is the default date and since the excel only has the time, it automatically used the default date. So in order to compare we also change it here
            DateTime datemax = DateTime.Parse("30/12/1899 " + max);
            DateTime datemin = DateTime.Parse("30/12/1899 " + min);

            List<Reservation> filteredList = list.Where(x => x.timeStart <= datemax).Where(x => x.timeStart >= datemin).ToList();
            return filteredList;
        }

        public List<Reservation> FilterBySpecificParameter(List<Reservation> list, string parameter)
        {
            List<Reservation> filteredList = list.Where(x => x.parameter.Contains(parameter)).ToList();
            return filteredList;
        }

        public List<Reservation> FilterByMoreParameters(List<Reservation> list, List<string> parameters)
        {
            List<Reservation> filteredList = new List<Reservation>();
            //List<Reservation> filteredlist = list.Where(x => parameters.Any(p => p.Equals(x.parameter))).ToList();
            foreach (string item in parameters)
            {
                filteredList = list.Where(x => x.parameter.Contains(item)).ToList();
            }

            return filteredList;
        }

        public List<Reservation> FilterByisgap(List<Reservation> list, bool isgap)
        {
            List<Reservation> filteredList = list.Where(x => x.isGap == isgap).ToList();
            return filteredList;
        }
    }
}
