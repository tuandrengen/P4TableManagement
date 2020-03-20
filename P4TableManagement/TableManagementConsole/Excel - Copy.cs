using System;
using System.Threading.Tasks;
using _Excel = Microsoft.Office.Interop.Excel; 


namespace TableManagementConsole
{

    class ReservationExcel
    {
        public void GetData()
        {
            _Excel.Application xlApp = new _Excel.Application();
            _Excel.Workbook xlwb = xlApp.Workbooks.Open(@"");
            _Excel.Worksheet xlws = (_Excel.Worksheet)xlwb.Sheets[1];
            _Excel.Range xlRange = xlws.UsedRange;

            int numberofrow = xlRange.Rows.Count;
            int numberofcol = xlRange.Columns.Count;

            for (int i = 1; i <= numberofrow; i++)
            {
                for (int j = 1; j <= numberofcol; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                }
            }
        }
    }

}
