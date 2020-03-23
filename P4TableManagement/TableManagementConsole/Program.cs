using System;
using System.Collections.Generic;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel test = new Excel(@"C:\Users\123\Desktop\test\test.xlsx", 1);
            List<string> res = test.ReadCell();
            foreach (string item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}
