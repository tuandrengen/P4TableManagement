using System;
using System.Collections.Generic;
using System.Linq;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table1 = new SmallTable() { tableNumber = 1 };
            Table table2 = new LargeTable() { tableNumber = 2 };
            Table table3 = new SmallTable() { tableNumber = 3 };
            Table table4 = new SmallTable() { tableNumber = 4 };

            Table table5 = new CombinedTable<Table>(table1, table3);

            Table.tableList.Add(table1);
            Table.tableList.Add(table2);
            Table.tableList.Add(table3);
            Table.tableList.Add(table4);
            Table.tableList.Add(table5);

            PrintList(Table.tableList);

            Table.DeleteTableFromList(3);
            PrintList(Table.tableList);

        }

        public static void PrintList(List<Table> l)
        {
            foreach (Table item in l)
            {
                Console.WriteLine($"Number of Seats: {item.seats}, ID: {item.ID}, Number: {item.tableNumber}");            
            }
            Console.WriteLine();
        }


        /*
         * TODO:
         * 1)   Table.cs - And all other tables.
         * 1.a) Add method that removes the combined tables from the list
         * 2.b) Add a method that replaces the two tables in the list with 
         *      the new combined table.
         *      e.g. table 1 and 3 are combined, remove them from the list,
         *      and add a new table with the lowest tableNumber of the two
         *      to the list of tables.
         * 
         * 2)   DecorationElement.cs and MapElement.cs
         * 2.a) Find out what kinds of decoration elements there are
         *      e.g. Aquarium, playroom, etc.
         * 2.b) Does DecorationElement.cs and MapElement.cs work?
         *      If not, make them.
         *      
         * 3)   MapSection.cs communication
         * 3.1) Add map elements to MapSection.cs
         * 
         * 4)   TableMap.cs
         * 4.1) Add MapSections to TableMap.
         * 
         * 5)   Look through code and ensure good naming of variables,
         *      methods, and classes.
         */
    }
}
