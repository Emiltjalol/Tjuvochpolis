using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Draw
    {
        public static void DrawCity(int width, int height)
        {
            for (int i = 0; i <= width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
                Console.SetCursorPosition(i, height + 1);
                Console.Write("-");
            }
            for (int i = 0; i <= height + 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(width + 1, i);
                Console.Write("|");
            }

        }
        public static void DrawPrison(int prisonX, int prisonY, int prisonWidth, int prisonHeight)
        {
            Console.WriteLine("");
            Console.SetCursorPosition(prisonX + 3, 0);
            Console.Write("FÄNGELSET");
            for (int i = 0; i <= prisonWidth; i++)
            {
                Console.SetCursorPosition(prisonX + i, prisonY);
                Console.Write("-");
                Console.SetCursorPosition(prisonX + i, prisonY + prisonHeight);
                Console.Write("-");
            }
            for (int i = 0; i <= prisonHeight; i++)
            {
                Console.SetCursorPosition(prisonX, prisonY + i);
                Console.Write("|");
                Console.SetCursorPosition(prisonX + prisonWidth, prisonY + i);
                Console.Write("|");
            }
        }
        public static void DrawPoorHouse(int poorHouseX, int poorHouseY, int poorHouseWidth, int poorHouseHeight)
        {

            Console.WriteLine("");
            Console.SetCursorPosition(poorHouseX + 3, poorHouseY - 1);
            Console.Write("FATTIGHUSET");
            for (int i = 0; i <= poorHouseWidth; i++)
            {
                Console.SetCursorPosition(poorHouseX + i, poorHouseY);
                Console.Write("-");
                Console.SetCursorPosition(poorHouseX + i, poorHouseY + poorHouseHeight);
                Console.Write("-");
            }
            for (int i = 0; i <= poorHouseHeight; i++)
            {
                Console.SetCursorPosition(poorHouseX, poorHouseY + i);
                Console.Write("|");
                Console.SetCursorPosition(poorHouseX + poorHouseWidth, poorHouseY + i);
                Console.Write("|");
            }
        }
        public static void DrawStatistics(int prisonWidth, int numOfRobberies, int thivesInPrison, int citizensInPoorHouse, int citizenNum, int thiefNum, int policeNum)
        {
            Console.SetCursorPosition(103 + (prisonWidth + 5), 1);
            Console.WriteLine($"Rån: {numOfRobberies}");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 2);
            Console.WriteLine($"Fängelset: {thivesInPrison}");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 3);
            Console.WriteLine($"Fattighuset: {citizensInPoorHouse}");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 4);
            Console.WriteLine($"Medborgare: {citizenNum}   [C]++");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 5);
            Console.WriteLine($"Tjuvar: {thiefNum}       [T]++");
            Console.SetCursorPosition(103 + (prisonWidth + 5), 6);
            Console.WriteLine($"Poliser: {policeNum}      [P]++");
        }
                //public class PoliceFactory
                //{
                //    private static readonly string[] PoliceNames = {
                //"Polisen Svensson", "Detektiv Johnsson", "Sergeant Davidsson",
                //"Inspektör Willhelmsson", "Kapten Andersson", "Löjtnant Martinsson",
                //"Officer Börjesson", "Detektiv Göransson",
                //"Sergant Carlberg", "Inspektör Nilsson"
                //};

                //    public static List<Police> CreatePolice(int count, int width, int height, Random random)
                //    {
                //        var policeList = new List<Police>();

                //        for (int j = 0; j < count; j++)
                //        {
                //            int randomX = random.Next(1, width - 1);
                //            int randomY = random.Next(1, height - 1);
                //            var police = new Police(PoliceNames[j % PoliceNames.Length], randomX, randomY, 'P');
                //            policeList.Add(police);
                //        }

                //        return policeList;
                //    }
                //}



                //public class CitizenFactory
                //{
                //    private static readonly string[] CitizenNames = {
                //"John Simonsson", "Jane Karlsson", "Michael Jonsson", "Emily Davidsson", "David Williamsson", "Lisa Andersson",
                //"Sarah Martinsson", "Robert Börjesson", "Maria Klarksson", "William Andersson", "Jennifer Jenssen", "Christoffer Grenborg",
                //"Klara Klausson", "Richard Waldemarsson", "Patricia Tunberg", "Josef Mauritz", "Linda Hallberg", "Thomas Larsson", "Cynthia Garcia",
                //"Charles Rodriguez", "Nancy Scottsson", "Daniel Ljungberg", "Susan Klingberg", "Mattias Wright", "Helene Adamsson",
                //"Kevin Klausson", "Sandra Grenberg", "Andreas Redström", "Maria Cartelberg", "James Hallström", "Daniel Jakobsson"
                //};

                //    public static List<Citizen> CreateCitizens(int count, int width, int height, Random random)
                //    {
                //        var citizenList = new List<Citizen>();

                //        for (int i = 0; i < count; i++)
                //        {
                //            int randomX = random.Next(1, width - 1);
                //            int randomY = random.Next(1, height - 1);
                //            var citizen = new Citizen(CitizenNames[i % CitizenNames.Length], randomX, randomY, 'C');
                //            citizen.Inventory.Add("Nycklar");
                //            citizen.Inventory.Add("Mobiltelefon");
                //            citizen.Inventory.Add("Plånbok");
                //            citizen.Inventory.Add("Klocka");
                //            citizenList.Add(citizen);
                //        }

                //        return citizenList;
                //    }
                //}



                //public class ThiefFactory
                //{
                //    private static readonly string[] ThiefNames = {
                //"Tommy", "Susie", "Bobby", "Steve", "Vicky",
                //"Danny", "Rita", "Eddie", "Maggie",
                //"Frankie", "Lenny", "Connie", "Ronny", "Lucy",
                //"Harry", "Penny", "Vinny", "Mia", "Johnny", "Gina", "Larry"
                //};

                //    public static List<Thief> CreateThieves(int count, int width, int height, Random random)
                //    {
                //        var thiefList = new List<Thief>();

                //        for (int i = 0; i < count; i++)
                //        {
                //            int randomX = random.Next(1, width - 1);
                //            int randomY = random.Next(1, height - 1);
                //            var thief = new Thief(ThiefNames[i % ThiefNames.Length], randomX, randomY, 'T');
                //            thiefList.Add(thief);
                //        }

                //        return thiefList;
                //    }
                //}
    }
}

