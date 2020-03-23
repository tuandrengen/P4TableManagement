using System;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel test = new Excel("A:\a.xlsx", 1);
            int res = test.ReadCell(0 , 0);
            Console.WriteLine(res);
        }
    }
}
