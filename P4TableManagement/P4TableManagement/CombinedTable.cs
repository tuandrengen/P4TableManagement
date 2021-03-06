﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace P4TableManagement
{
    public class CombinedTable <T1> : Table, IComparer<Table>
                           where T1 : Table 
    {
        public List<Table> combinedTables = new List<Table>();
        public T1 tableOne { get; set; }
        public T1 tableTwo { get; set; }

        public CombinedTable(T1 tableOne, T1 tableTwo) : base(tableOne.width + tableTwo.width, tableOne.height + tableTwo.height, tableOne.placementX, tableOne.placementY)
        {
            combinedTables.Add(tableTwo);
            combinedTables.Add(tableOne);
            seats = tableOne.seats + tableTwo.seats;
            tableNumber = combinedTables.Min(x => x.tableNumber);
        }

        public int Compare(Table firstTable, Table secondTable)
        {
            return firstTable.tableNumber.CompareTo(secondTable.tableNumber);
        }
    }
}
