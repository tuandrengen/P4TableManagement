﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace TableManagementConsole
{
    partial class ReservationList
    {
        public List<Reservation> listReservation = new List<Reservation>();
        
        //manuelt indtastning af data ind i database
        public void CreateReservation()
        {
            throw new NotImplementedException();
        }

        //læser fra en anden database(online bestilling) og inputter ind i vorse database(excel)
        public void AutomaticCreateReservation()
        {
            throw new NotImplementedException();
        }



        //indtager data fra databasen, laver daten om til objekter(reservations) og putter dem i en liste
        public void PopulateReservationList(string path, int sheet)
        {
            Excel test = new Excel(path, sheet);
            List<Reservation> list = test.ReadCell();
            foreach (var reservation in list)
            {
                Console.WriteLine($"{reservation.numberOfGuests}p, {reservation.name},  {reservation.phoneNumber}, {reservation.timeStart.ToString("HH:mm")} , {string.Join(", ", reservation.parameter)} , {reservation.comment}");
            }

            test.Close();
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
