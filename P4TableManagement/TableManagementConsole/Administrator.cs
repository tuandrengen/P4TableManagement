using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Administrator
    {
        private int _clearanceCode;
		private string _name;

        //properties
		public int clearanceCode
		{
			get { return _clearanceCode;}
			set { _clearanceCode = value;}
		}

		public string name
		{
			get { return _name;}
			set { _name = value;}
		}

        //constructor
		public Administrator(string name, int clearanceCode)
		{
            this.name = name;
            this.clearanceCode = clearanceCode;
		}

        //metodes
		public void Log()
        {
            throw new NotImplementedException();
        }

    }
}
