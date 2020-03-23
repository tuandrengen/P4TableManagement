using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;



namespace TableManagementConsole
{

    class Excel
    {
        string path = " ";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;


        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path, 0, true, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ws = (Worksheet)wb.Worksheets[sheet];
        }

        public List<string> ReadCell()
        {
            List<string> result = new List<string>();
            _Excel.Range range = ws.UsedRange;
            string res = "";

            int numberOfRow = range.Rows.Count;
            int numberOfCol = range.Columns.Count;


            for (int row = 1; row <= numberOfRow; row++)
            {
                _Excel.Range line = ws.Rows[row];

                for (int col = 1; col <= numberOfCol; col++)
                {
                    _Excel.Range aCell = (ws.Cells[row, col] as _Excel.Range);
                    res += aCell.Value2.ToString() + " ";
                }

                result.Add(res);
                res = "";
            }

            return result;
        }
    }

}
