using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace readfilefromexcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlwb = xlApp.Workbooks.Open(@"C:\Users\123\Desktop\test\test2.xlsx", 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel.Worksheet xlws = (Excel.Worksheet)xlwb.Sheets[1];
            Excel.Range xlRange = xlws.UsedRange;


            int numberofrow = xlRange.Rows.Count;
            int numberofcol = xlRange.Columns.Count;

            for (int i = 1; i <= numberofrow; i++)
            {
                for (int j = 1; j <= numberofcol; j++)
                {
                    Excel.Range range = (xlws.Cells[i, j] as Excel.Range);
                    string cellValue = range.Value.ToString();
                    Console.WriteLine(cellValue);
                }
            }
        }
    }
}
