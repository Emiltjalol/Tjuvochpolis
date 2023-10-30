using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuv_Polis_MinUtveckling26Okt
{
    class Person
    {
        public string Namn { get; set; }
        public int X_coord { get; set; }
        public int Y_coord { get; set; }
        public List<string> Inventory { get; set; }
        public char Symbol { get; set; }
        public bool PrisonInmate { get; set; }
        public bool PoorHouseInmate { get; set; }
        public int Direction { get; set; }

        public Person(string namn, char symbol, int direction)
        {
            Namn = namn;
            Inventory = new List<string>();
            Symbol = symbol;
            Direction = direction;
        }
    }

    class Police : Person
    {
        public Police(string namn, int x_Coord, int y_Coord, char symbol, int direction) : base(namn, symbol, direction)
        {
            X_coord = x_Coord;
            Y_coord = y_Coord;
            Direction = direction;
        }
        public void CatchThief(Thief thief)
        {
            if (thief.Inventory.Count > 0)
            {
                //Console.WriteLine($"{Namn} har fångat tjuven {thief.Namn}!");

                foreach (string stolenItem in thief.Inventory)
                {
                    Inventory.Add(stolenItem);
                }
                //Thread.Sleep(1500);
                thief.Inventory.Clear();
                thief.PrisonInmate = true;
            }
            else
            {
                //Temp Console.Writeline
                //Console.WriteLine("Har inget inte gjort något ännu");
            }
        }
    }

    class Citizen : Person
    {
        public Citizen(string namn, int x_Coord, int y_Coord, char symbol, int direction, bool poorHouseInmate) : base(namn, symbol, direction)
        {
            X_coord = x_Coord;
            Y_coord = y_Coord;
            Direction = direction;
            PoorHouseInmate = poorHouseInmate;
        }
    }

    class Thief : Person
    {
        public Thief(string namn, int x_Coord, int y_Coord, char symbol, bool prisonInmate, int direction) : base(namn, symbol, direction)
        {
            X_coord = x_Coord;
            Y_coord = y_Coord;
            PrisonInmate = prisonInmate;
            Direction = direction;
        }


        public void Steal(Citizen citizen)
        {
            if (citizen.Inventory.Count > 0)
            {
                int Item = new Random().Next(citizen.Inventory.Count);
                string stolenItem = citizen.Inventory[Item];
                Inventory.Add(stolenItem);
                citizen.Inventory.RemoveAt(Item);
                //Thread.Sleep(200);
            }
        }
    }
}
