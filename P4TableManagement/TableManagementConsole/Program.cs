using System;
using System.Collections.Generic;
using System.Linq;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        // Method that prints all tables in a list.
        public static void PrintList(List<Table> tableList)
        {
            foreach (Table table in tableList)
            {
                Console.WriteLine($"Type: { table.GetType().Name }, Seats: { table.seats }, " +
                                  $"ID: { table.ID }, Table #{ table.tableNumber }, " +
                                  $"Status: { table.state }");            
            }
            Console.WriteLine();
        }
    }
}
