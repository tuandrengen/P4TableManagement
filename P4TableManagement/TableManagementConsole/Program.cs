using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Table table = new Table();

            table.CreateSmallTable();
            table.CreateLargeTable();
            table.CreateSmallTable();
            table.CreateLargeTable();

            PrintList(Table.tableList);
            Table.DeleteTableFromList(3);
            PrintList(Table.tableList);

        }

        public static void PrintList(List<Table> l)
        {
            foreach (Table item in l)
            {
                Console.WriteLine("Højde: {0}, ID: {1}",item.seats, item.ID);            
            }
        }

	}
}
