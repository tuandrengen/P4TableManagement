using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TableManagementConsole
{
	public abstract class Table : MapElement, IComparable<Table>
    {
		private static int _tableID = 1;
		public int seats { get; set; }
		// tableNumber has been changed from private set; to protected set; 
		// as sub classes should be able to set this value as well
		public int tableNumber { get; protected set; }
		public int bookingID { get; set; }
		public string state { get; set; }

		public int ID { get; set; }

		public List<string> parameters = new List<string>();

		public Table(int width, int height, int placementX, int placementY) : base(width, height, placementX, placementY)
		{
			// Read this link for more information about auto-incrementing a value.
			// https://stackoverflow.com/questions/8813435/incrementing-a-unique-id-number-in-the-constructor
			tableNumber = System.Threading.Interlocked.Increment(ref _tableID);
			state = "Available";
		}

		public int CompareTo([AllowNull] Table other)
		{
			return ID.CompareTo(other.ID);
		}
	}
}
