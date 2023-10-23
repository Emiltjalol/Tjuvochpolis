using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Symbol= symbol;

        }


        public virtual char GetSymbol()
        {
            // Return the symbol for the person (customize this logic as needed)
            return 'X';
        }
    }



    class Police : Person
    {
        public Police(string namn, int xcoordinate, int ycoordinate, char symbol) : base(namn, symbol)
        {
            
            XCoordinate = xcoordinate;
            YCoordinate = ycoordinate;
        }

        public override char GetSymbol()
        {
            return 'P';
        }
    }

    class Citizen : Person
    {
        public Citizen(string namn, int xcoordinate, int ycoordinate,char symbol) : base(namn,symbol)
        {
            XCoordinate = xcoordinate;
            YCoordinate = ycoordinate;
        }

        public override char GetSymbol()
        {
            return 'C';
        }
    }

    class Thief : Person
    {
        public Thief(string namn, int xcoordinate, int ycoordinate,char symbol) : base(namn,symbol)
        {
            XCoordinate = xcoordinate;
            YCoordinate = ycoordinate;
        }

        public override char GetSymbol()
        {
            return 'T';
        }
    }
}