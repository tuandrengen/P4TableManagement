using System;
using System.Collections.Generic;
using System.Linq;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dummy data...
            Table table1 = new SmallTable() { tableNumber = 1 };
            Table table2 = new LargeTable() { tableNumber = 2 };
            Table table3 = new SmallTable() { tableNumber = 3 };
            Table table4 = new SmallTable() { tableNumber = 4 };
            Table table5 = new SmallTable() { tableNumber = 5 };
            Table table6 = new SmallTable() { tableNumber = 6 };
            Table table7 = new SmallTable() { tableNumber = 7 };

            Booking booking = new Booking(2, DateTime.Parse("18:00"), false);

            // Adds tables to tableList.
            Table.tableList.Add(table1);
            Table.tableList.Add(table2);
            Table.tableList.Add(table3);
            Table.tableList.Add(table4);
            Table.tableList.Add(table5);
            Table.tableList.Add(table6);
            Table.tableList.Add(table7);

            PrintList(Table.tableList);

            // Combines table 1 and 3 to a new table, table 5.
            // The CombineTables() method also removes the two tables from the tableList,
            // and then adds the new table to the table list.
            Table table10 = table1.CombineTables(table3);
            Table.tableList.Add(table10);
            PrintList(Table.tableList);

            // Sort tables by table's ID in an ascending order (low to high).
            //Table.tableList.Sort();
            //Console.WriteLine("Sorted by: ID");
            //PrintList(Table.tableList);

            // Sort tableList by Table number in an ascending order (low to high).
            Table.tableList.Sort(new SortTableByTableNumber());
            Console.WriteLine("Sorted by: Table number");
            PrintList(Table.tableList);

            // Set table 2 and 4 to 'Occupied' state via the delegate State 
            // and the event statehandler.
            // stateHandler(table2, "Occupied");
            // stateHandler(table4, "Occupied");

            table2.AssignTable(booking);
            table5.AssignTable(booking);
            table6.PaidTable();
            table4.ReservedTable(booking);
            Console.WriteLine();

            // Uses predicate to search for all tables that are available.
            Console.WriteLine($"Available Tables: ({ Table.GetTableList(availableFinder).Count })");
            PrintList(Table.GetTableList(availableFinder));

            // Uses predicate to search for all tables that are unvailable.
            Console.WriteLine($"Occupied Tables: ({ Table.GetTableList(occupiedFinder).Count })");
            PrintList(Table.GetTableList(occupiedFinder));

            Console.WriteLine($"Paid Tables: ({ Table.tableList.Count })");
            PrintList(Table.GetTableList(paidFinder));

            Console.WriteLine($"Reserved Tables: ({ Table.GetTableList(reservedFinder).Count })");
            PrintList(Table.GetTableList(reservedFinder));

            // Separates table 5 back to table 1 and 3.
            // See CombineTables() in Table.cs for more.
            Table.SeparateTables((CombinedTable<Table>)table10);

            // Prints all tables.
            Table.tableList.Sort();
            Console.WriteLine($"All tables: ({ Table.tableList.Count })");
            PrintList(Table.tableList);

            Console.WriteLine($"Available Tables: ({ Table.GetTableList(availableFinder).Count })");
            // Uses predicate to search for all tables that are available.
            PrintList(Table.GetTableList(availableFinder));

            // Uses predicate to search for all tables that are unvailable.
            Console.WriteLine($"Occupied Tables: ({ Table.GetTableList(occupiedFinder).Count })");
            PrintList(Table.GetTableList(occupiedFinder));

            Console.WriteLine($"Paid Tables: ({ Table.GetTableList(paidFinder).Count })");
            PrintList(Table.GetTableList(paidFinder));

            Console.WriteLine($"Reserved Tables: ({ Table.GetTableList(reservedFinder).Count })");
            PrintList(Table.GetTableList(reservedFinder));
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

        // Predicates that acts as search criteria for the GetTableList() method.
        // Move to Table.cs later.
        static Predicate<Table> availableFinder = (Table t) => t.state.Equals("Available");
        static Predicate<Table> occupiedFinder = (Table t) => t.state.Equals("Occupied");
        static Predicate<Table> reservedFinder = (Table t) => t.state.Equals("Reserved");
        static Predicate<Table> paidFinder = (Table t) => t.state.Equals("Paid");

        // Delegate to set state of table.
        // Move to Table.cs later.
        // public delegate void State(Table table, string state);
        // static State stateHandler = SetState;

        // Method used by statHandler event.
        // Move to Table.cs later.
        // public static void SetState(Table table, string state)
        // {
        //     table.state = state;
        // }

        /*
         * TODO:
         * 1)   Table.cs - And all other tables.
         * 1.c) Table.cs -> MapElement.cs  
         *      Make them communicate.
         * 
         * 2)   DecorationElement.cs and MapElement.cs
         * 2.a) Find out what kinds of decoration elements there are
         *      e.g. Aquarium, playroom, Bar, Vinduer? etc.
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
