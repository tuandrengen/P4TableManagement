﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace P4TableManagement
{
	public abstract class Table : MapElement, IComparable<Table>
    {
		public static int _tableID = 0;
		public int seats { get; set; }
		// tableNumber has been changed from private set; to protected set; 
		// as sub classes should be able to set this value as well
		public int tableNumber { get; protected set; }
		public int bookingID { get; set; }
		public string state { get; set; }

		public int ID { get; set; }

		public List<string> parameters = new List<string>();

		public Table(double width, double height, double placementX, double placementY) : base(width, height, placementX, placementY)
		{
			// Read this link for more information about auto-incrementing a value.
			// https://stackoverflow.com/questions/8813435/incrementing-a-unique-id-number-in-the-constructor
			tableNumber = System.Threading.Interlocked.Increment(ref _tableID);
			state = "Available";
		}

		// YO this AllowNull wat this how to implement
		public int CompareTo(/*[AllowNull]*/ Table other)
		{
			return ID.CompareTo(other.ID);
		}
	}
}
