using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    public class LargeTable : Table
    {
        public LargeTable() : base(2, 1, 0, 0)
        {
            seats = 4;
        }
    }
}
