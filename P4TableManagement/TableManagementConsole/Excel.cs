using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;


namespace TableManagementConsole
{

    class Excel
    {
        //initialize and declare
        string path = " ";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        //constructor
        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path, 0, true, 5, "", "", true, _Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ws = (Worksheet)wb.Worksheets[sheet];
        }

        //method
        //creates a list of reservation based on excel sheet
        public List<Reservation> ReadCell()
        {

            List<Reservation> result = new List<Reservation>();
            _Excel.Range range = ws.UsedRange;

            int numberOfRow = range.Rows.Count;

            // reads each row and assigns that row's column to a field using Reservation ctor.
            for (int row = 1; row <= numberOfRow; row++)
            {
                _Excel.Range numberOfGuest = (ws.Cells[row, 1] as _Excel.Range);
                _Excel.Range timeStart = (ws.Cells[row, 3] as _Excel.Range);
                _Excel.Range name = (ws.Cells[row, 2] as _Excel.Range);
                _Excel.Range phoneNumber = (ws.Cells[row, 4] as _Excel.Range);
                _Excel.Range parameter = (ws.Cells[row, 5] as _Excel.Range);
                _Excel.Range comment = (ws.Cells[row, 6] as _Excel.Range);
                _Excel.Range isGap = (ws.Cells[row, 7] as _Excel.Range);

                //convert excel value to datetime type
                double date = timeStart.Value2;
                DateTime datetime = DateTime.FromOADate(date);

                //converterer lang string fra excel til list<string> som parameter.....
                string parameterString = parameter.Value2.ToString();
                List<string> parameters = parameterString.Split(',').ToList();


                //adds the data from the excel to a list of reservations
                result.Add(new Reservation(name.Value2.ToString(), datetime, bool.Parse(isGap.Value2.ToString()), (int)numberOfGuest.Value2, (int)phoneNumber.Value2, parameters, comment.Value2.ToString()));
            }

            return result;
        }
    }

}