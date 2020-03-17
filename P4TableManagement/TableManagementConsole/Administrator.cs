using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Administrator
    {
        private int _clearanceCode;

		public int clearanceCode
		{
			get { return _clearanceCode;}
			set { _clearanceCode = value;}
		}

		private int _name;

		public int name
		{
			get { return _name;}
			set { _name = value;}
		}

		public Log()
        {
            throw NotImplementedException();
        }

    }
}
