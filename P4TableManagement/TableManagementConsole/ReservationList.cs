using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace TableManagementConsole
{
    class ReservationList
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

        string path = " ";
        //indtager data fra databasen, laver daten om til objekter(reservations) og putter dem i en liste
        public void PopulateReservationList()
        {
             
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
