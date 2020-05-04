using P4TableManagement;
using System;
using System.Collections.Generic;

namespace P4TableManagement
{
    public interface ITableManagementSystem
    {
        List<Table> TableList { get; }
        List<DecorationElement> DeList { get; }
        List<MapElement> MeList { get; }
        List<MapSection> MsList { get; }
        void AddMapElementToList(MapElement mapElement);
        void AddMapSectionToList(MapSection mapSection);
        void AddTableToList(Table table);
        void AssignTable(Table table, Booking booking);
        Table CombineTables(Table one, Table two);
        void CreateDecorationElement(DecorationElement decorationElement);
        void DeleteDecorationElement(DecorationElement decorationElement);
        void DeleteMapElementFromList(MapElement mapElement);
        void DeleteTableFromList(Table table);
        void GetAllMapElements();
        List<Table> GetTableList(Predicate<Table> searchCriteria);
        void PayTable(Table table);
        void RemoveMapSection(int id);
        void ReserveTable(Table table, Booking booking);
        void SeparateTables(CombinedTable<Table> combinedTable);
        void ToggleMapSectionVisibility(MapSection mapSection);
        void UnassignTable(Table table);
    }
}