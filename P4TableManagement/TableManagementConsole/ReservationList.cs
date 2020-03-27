using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace TableManagementConsole
{
    partial class ReservationList
        // mangler metoder som skaber ny sheet for hver dag
        // og en metode som skaber en ny workbook for hver måned
    {
        public List<Reservation> listReservation = new List<Reservation>();
        
        //manuelt indtastning af data ind i database
        public void CreateReservation(string path, int sheet)
        {
            Excel excel = new Excel(path, sheet);

            Console.WriteLine("Enter string: ");
            string s = Console.ReadLine();
            string[] seperator = { "p, ", ", ", "p " };
            //Int32 count = 4;

            string[] list = s.Split(seperator, 4, StringSplitOptions.None);

            excel.WriteToCell(list);

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
        public void PopulateReservationList(string path, int sheet)
        {
            Excel test = new Excel(path, sheet);
            List<Reservation> list = test.ReadCell();

            //printer listen skal fjernes senere
            foreach (var reservation in list)
            {
                Console.WriteLine($"{reservation.numberOfGuests}p, {reservation.name},  {reservation.phoneNumber}, {reservation.timeStart.ToString("HH:mm")} , {string.Join(", ", reservation.parameter)} , {reservation.comment}");
            }

            test.Quit();
        }

        //sletter reservationer
        public void DeleteReservations()
        {
             throw new NotImplementedException();
        }

        //ændre data hos en allerede eksisterende
        public void EditReservations()
        {
             throw new NotImplementedException();
        }

        // sortere reservationer
        public void SortReservations()
        {
            throw new NotImplementedException();
        }

        // Filtrer reservationer
        public void FilterReservations()
        {
             throw new NotImplementedException();
        }

    }
}
