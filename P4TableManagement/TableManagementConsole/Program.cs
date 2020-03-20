using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
			Table table = new Table();

            table.CreateLargeTable();
            table.CreateSmallTable();

            PrintList(table.tableList);
        }

        public static void PrintList(List<Table> l)
        {
            foreach (Table item in l)
            {
                Console.WriteLine(item.ID);
                Console.WriteLine(item.ID);
            }
        }

	}
}
