using System;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel test = new Excel(@"C:\Users\T-Phamz\Desktop\test\test.xlsx", 1);
            string res = test.ReadCell(0 , 0);
            Console.WriteLine(res);
        }
    }
}
