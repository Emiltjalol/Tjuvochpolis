using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Person
    {
        public string Namn { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public List<string> Inventory { get; set; }
        public char Symbol { get; set; }

        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(Symbol);
        }

        public Person(string namn, char symbol)
        {
            Namn = namn;
            Inventory = new List<string>();
            Symbol = symbol;
        }
    }

    class Police : Person
    {
        public Police(string namn, int xcoordinate, int ycoordinate, char symbol) : base(namn, symbol)
        {
            XCoordinate = xcoordinate;
            YCoordinate = ycoordinate;
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

                Thread.Sleep(1500);
                thief.Inventory.Clear();
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
        public Citizen(string namn, int xcoordinate, int ycoordinate, char symbol) : base(namn, symbol)
        {
            XCoordinate = xcoordinate;
            YCoordinate = ycoordinate;
        }
    }

    class Thief : Person
    {
        public Thief(string namn, int xcoordinate, int ycoordinate, char symbol) : base(namn, symbol)
        {
            XCoordinate = xcoordinate;
            YCoordinate = ycoordinate;
        }

        //behövs fixa /Eric K

        public void Steal(Citizen citizen)
        {
            if (citizen.Inventory.Count > 0)
            {
                int Item = new Random().Next(citizen.Inventory.Count);
                string stolenItem = citizen.Inventory[Item];
                Inventory.Add(stolenItem);
                citizen.Inventory.RemoveAt(Item);
                //Console.WriteLine($"Tjuven {Namn} har rånat medborgaren {citizen.Namn} på {stolenItem}");
                Thread.Sleep(200);
            }
        }
    }
}
