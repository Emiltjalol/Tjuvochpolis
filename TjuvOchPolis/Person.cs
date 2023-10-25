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
    }
}
