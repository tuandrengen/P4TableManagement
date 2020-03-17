using System;
using System.Collections.Generic;
using System.Text;

namespace TableManagementConsole
{
    class Administrator : Employee
    {
        private int _clearanceCode;

	public int clearanceCode
	{
		get { return _clearanceCode;}
		set { _clearanceCode = value;}
	}

    }
}
