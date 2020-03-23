using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public class CombinedTable <T1> : Table, IComparer<Table>
                           where T1 : Table 
    {
        public List<Table> combinedTables = new List<Table>();
        public T1 tableOne { get; set; }
        public T1 tableTwo { get; set; }

        public CombinedTable(T1 tableOne, T1 tableTwo) : base()
        {
            combinedTables.Add(tableTwo);
            combinedTables.Add(tableOne);

            width = tableOne.width + tableTwo.width;
            height = tableOne.height + tableTwo.height;
            seats = tableOne.seats + tableTwo.seats;
            // Lambda expression that returns the lowest tablenumber of the combinedTables list.
            tableNumber = combinedTables.Min(x => x.tableNumber);
        }

        public int Compare(Table firstTable, Table secondTable)
        {
            return firstTable.tableNumber.CompareTo(secondTable.tableNumber);
        }
    }
}
