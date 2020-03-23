using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel test = new Excel(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            List<string> res = test.ReadCell();
            res.ForEach(Console.WriteLine);
        }
    }
}
