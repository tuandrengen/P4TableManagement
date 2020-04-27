using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    public class SmallTable : Table
    {
        public SmallTable() : base(1, 1, 0, 0)
        {
            seats = 2;
        }
    }
}
