using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    public interface ITableManagementSystem
    {
        List<Table> TableList { get; }
        void AddTableToList(Table table);
        void AssignTable(Table table, Booking booking);
        Table CombineTables(Table one, Table two);
        void DeleteTableFromList(Table table);
        List<Table> GetTableList(Predicate<Table> searchCriteria);
        void PayTable(Table table);
        void ReserveTable(Table table, Booking booking);
        void SeparateTables(CombinedTable<Table> combinedTable);
        void UnassignTable(Table table);
    }
}