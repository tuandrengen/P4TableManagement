using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Table : MapElement
    {
        public int seats { get; set; }
        private int _tableNumber { get; }
        List<string> parameter = new List<string>();
        public string tableState { get; set; }
    }
}
