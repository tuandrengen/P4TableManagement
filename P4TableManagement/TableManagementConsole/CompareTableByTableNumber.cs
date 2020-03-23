using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TableManagementConsole
{
	public class SortTableByTableNumber : IComparer<Table>
	{
		public int Compare([AllowNull] Table x, [AllowNull] Table y)
		{
			return x.tableNumber.CompareTo(y.tableNumber);
		}
	}
}
