using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableManagementConsole
{
    public class DecorationElement : MapElement
    {

        public int zoneWidth { get; set; }
        public int zoneHeight { get; set; }
        public string name { get; set; }

        public static List<DecorationElement> decorationElementList = new List<DecorationElement>();

        //constructor
        // Fjern evt. X og Y koordinat, ikke relevant her...
		public DecorationElement(string name, int zoneWidth, int zoneHeight, int width, int height, int placementX, int placementY) : base(width, height, placementX, placementY)
		{
            this.name = name;
            this.zoneWidth = zoneWidth;
            this.zoneHeight = zoneHeight;
		}

        // We create a new DecorationElement and adds it to the overall list (redundant parameters?)
        public void CreateDecorationElement(List<DecorationElement> deList)
        {
            if(deList.Count != 0)
            {
                // Kontrolstruktur der tjekker om elementet allerede eksisterer i listen
                foreach (DecorationElement de in deList.ToList())
                {
                    // Console.WriteLine($"Vi tjekker dette item {de.name}");
                    if (name == de.name)
                    {
                        // De to elementer er ens og derfor kan den ikke sættes ind i listen.
                        // Man kunne prompte brugeren til at ændre navnet på newDE
                        // Console.WriteLine($"ERROR - Der eksisterer allerede et {de.name}");
                        // throw new Exception("Cannot create a Decoration Element with the same name of another!");
                        break;
                    }
                    else
                    {
                        deList.Add(this);
                    }
                }
            }
            else
            {
                deList.Add(this);
            }
        }

        public void DeleteDecorationElement(List<DecorationElement> deList)
        {
            deList.Remove(this);
        }
    }
}
