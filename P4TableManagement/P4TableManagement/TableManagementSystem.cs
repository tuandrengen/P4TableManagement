using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace P4TableManagement
{
    public class TableManagementSystem : ITableManagementSystem
    {
		// Most logic goes here
		// 1 - All writelines should be removed, as we do not write out to a console application.
		// 1a - Eventually make all the functions return a string so the UI can show it to the user.
		
		/*
		 * TODO:
		 * - All exceptions has to be caught somewhere - may be in the controller.
		 *		The exceptions has to tell the user what went wrong.
		 */

		public List<Reservation> ReservationList { get; set; }
		public List<Reservation> AssignedReservationList { get; set; }
		public List<Table> TableList { get; } // Table
		public List<DecorationElement> DeList { get; } // Decoration Element
		public List<MapElement> MeList { get; } // Map Element
		public List<MapSection> MsList { get; } // Map Section
		public List<MapSection> TmList { get { return MsList.FindAll(x => x.visibility == true); } }

		public TableManagementSystem()
		{
			TableList = new List<Table>();
			DeList = new List<DecorationElement>();
			MeList = new List<MapElement>();
			MsList = new List<MapSection>();
		}

		public void AddTableToList(Table table)
		{
			TableList.Add(table);
		}

		public void AssignTable(Table table, Booking booking)
		{
			// Now we check this statement before the method is cast in MainWindow.xaml.cs!
			//if (table.state == "Occupied")
			//{
			//	//throw new TableAlreadyAssignedException("Error: Cannot assign an occupied table!");
			//	MessageBox.Show("The table is already occupied and could not be assigned");
			//	return;
			//}

			table.bookingID = booking.id;
			table.state = "Assigned";
			//AssignedReservationList.Add(booking);

			MessageBox.Show($"Table #{ table.tableNumber } has been assigned! Booking ID: { table.bookingID }");
		}

		public void UnassignTable(Table table)
		{
			table.bookingID = default;
			table.state = "Available";
			MessageBox.Show($"Table #{ table.tableNumber } has been unassigned!");
		}

		public void PayTable(Table table)
		{
			table.state = "Paid";
			MessageBox.Show($"Table #{ table.tableNumber } has been paid! Booking ID: { table.bookingID }");
		}

		public void ReserveTable(Table table, Booking booking)
		{
			table.bookingID = booking.id;
			table.state = "Reserved";
			MessageBox.Show($"Table #{ table.tableNumber } has been reserved! Booking ID: { table.bookingID }");
		}

		public List<Table> GetTableList(Predicate<Table> searchCriteria)
		{
			List<Table> newList = TableList.FindAll(searchCriteria);
			return newList;
		}

		// Combines this object of a table with another table by deleting the two
		// tables from the tableList and then returning the new CombinedTable.
		public CombinedTable<Table> CombineTables(Table one, Table two)
		{
			foreach (Table table in TableList.ToList())
			{
				if (table.tableNumber == one.tableNumber || table.tableNumber == two.tableNumber)
				{
					TableList.Remove(table);
				}
			}

			return new CombinedTable<Table>(one, two);
		}

		// Separates table by adding the two tables that were combined at first,
		// and adding them back to tableList.
		public void SeparateTables(CombinedTable<Table> combinedTable)
		{
			foreach (Table table in combinedTable.combinedTables)
			{
				TableList.Add(table);
			}

			TableList.Remove(combinedTable);
		}

		public void DeleteTableFromList(Table table)
		{
			TableList.Remove(table);
			//// .ToList and Linq library
			//// https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
			//foreach (Table table in tableList.ToList())
			//{
			//	if (table.ID == id)
			//	{
			//		tableList.Remove(item);
			//	}
			//}
		}

		// Creates a new decoration element if it does not exist in the decoration element list.
		// If there are no elements in the decoration list, add the decoration element to the list.
		public void CreateDecorationElement(DecorationElement decorationElement)
		{
			if (DeList.Count != 0)
			{
				foreach (DecorationElement element in DeList.ToList())
				{
					if (decorationElement.name == element.name)
					{
						// The two elements are called the same, therfore, it can not be added to the list.
						// Eventually ask the user to change the name of the element.
						//throw new DecorationElementAlreadyExistsException("Cannot create a Decoration Element with the same name of another!");
						break;
					}
					else
					{
						DeList.Add(decorationElement);
					}
				}
			}
			else
			{
				DeList.Add(decorationElement);
			}
		}

		public void DeleteDecorationElement(DecorationElement decorationElement)
		{
			DeList.Remove(decorationElement);
		}

		public void GetAllMapElements()
		{
			MeList.Clear();
			MeList.TrimExcess();

			foreach (Table table in TableList)
			{
				MeList.Add(table);
			}
			
			foreach (DecorationElement decorationElement in DeList)
			{
				MeList.Add(decorationElement);
			}
		}

		public void AddMapElementToList(MapElement mapElement)
		{
			MeList.Add(mapElement);
		}

		public void DeleteMapElementFromList(MapElement mapElement)
		{
			MeList.Remove(mapElement);
		}

		public void AddMapSectionToList(MapSection mapSection)
		{
			MsList.Add(mapSection);
		}

		public void ToggleMapSectionVisibility(MapSection mapSection)
		{
			if (mapSection.visibility)
			{
				mapSection.visibility = false;
			}
			else
			{
				mapSection.visibility = true;
			}
		}

		public void RemoveMapSection(int id)
		{
			foreach (MapSection mapSection in MsList)
			{
				if (mapSection.sectionID == id)
				{
					MsList.Remove(mapSection);
				}
				else
				{
					throw new MapSectionNotFoundException();
				}
			}
		}
	}
}
