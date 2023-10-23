using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Person
    {
        public char Symbol { get; set; }

        public Person(char symbol)
        {
            Symbol = symbol;
        }

        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(Symbol);
        }
    }

}
