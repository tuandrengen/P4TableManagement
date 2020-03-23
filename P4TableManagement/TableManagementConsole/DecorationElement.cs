using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public class DecorationElement : MapElement
    {
        private int _zoneWidth;
        private int _zoneHeight;
        private string _name;

        //properties
		public int zoneWidth
		{
			get { return _zoneWidth; }
			set { _zoneWidth = value; }
		}

        public int zoneHeight
        {
            get { return _zoneHeight; }
            set { _zoneHeight = value; }
        }

        public string name
		{
			get { return _name; }
			set { _name = value; }
		}

        public static List<DecorationElement> decorationElementList = new List<DecorationElement>();

        //constructor
		public DecorationElement(string name, int zoneWidth, int zoneHeight, int width, int height, int placementX, int placementY) : base(width, height, placementX, placementY)
		{
            this.name = name;
            this.zoneWidth = zoneWidth;
            this.zoneHeight = zoneHeight;
		}

        // We create a new DecorationElement and adds it to the overall list (redundant parameters?)
        public void CreateDecorationElement(DecorationElement newDE)
        {
            bool listValid = false;

            // Kontrolstruktur der tjekker om elementet allerede eksisterer i listen
            foreach (var item in decorationElementList)
            {
                Console.WriteLine($"Vi tjekker dette item {item.name}");
                if (item.name == newDE.name)
                {
                    // De to elementer er ens og derfor kan den ikke sættes ind i listen.
                    // Man kunne prompte brugeren til at ændre navnet på newDE
                    Console.WriteLine($"ERROR - Der eksisterer allerede et {item.name}");
                    Console.WriteLine();
                    listValid = false;
                    break;
                }
                else
                {
                    listValid = true;
                }
                Console.WriteLine();
            }
            if (listValid)
            {
                Console.WriteLine("Der eksisterer ikke en lignende, så vi tilføjer");
                decorationElementList.Add(newDE);
            }
        }

        public void DeleteDecorationElement(DecorationElement DE)
        {
            decorationElementList.Remove(DE);
        }
    }
}
