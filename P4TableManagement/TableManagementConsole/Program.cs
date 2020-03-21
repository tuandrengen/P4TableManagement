using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table1 = new SmallTable();
            Table table2 = new LargeTable();
            Table table3 = new SmallTable();
            Table table4 = new SmallTable();

            Table.tableList.Add(table1);
            Table.tableList.Add(table2);
            Table.tableList.Add(table3);
            Table.tableList.Add(table4);

            PrintList(Table.tableList);

            Table.DeleteTableFromList(3);
            PrintList(Table.tableList);

        }

        public static void PrintList(List<Table> l)
        {
            foreach (Table item in l)
            {
                Console.WriteLine($"Number of Seats: {item.seats}, ID: {item.ID}");            
            }
            Console.WriteLine();
        }
	}
}
