using System;

namespace TableManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel test = new Excel(@"C:\Users\123\Desktop\test\test.xlsx", 1);
            string res = test.ReadCell(0 , 0);
            Console.WriteLine(res);
        }
    }
}
