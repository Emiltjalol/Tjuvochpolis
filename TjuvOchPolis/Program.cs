using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace Tjuv_Polis_MinUtveckling26Okt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 100; // Bredden på fyrkanten
            int height = 25; // Höjden på fyrkanten
            int policeNum = 5; // Nummer av police
            int citizenNum = 20; // Nummer av Citizen
            int thiefNum = 20; // Nummer av Thief 
            int prisonX = width + 3; // X-koordinat för fängelsets övre vänstra hörn
            int prisonY = 1; // Y-koordinat för fängelsets övre vänstra hörn
            int prisonWidth = 15; // Bredden på fängelset
            int prisonHeight = 10; // Höjden på fängelset
            int poorHouseX = width + 3; // X-koordinat för fängelsets övre vänstra hörn
            int poorHouseY = 15; // Y-koordinat för fängelsets övre vänstra hörn
            int poorHouseWidth = 15; // Bredden på fängelset
            int poorHouseHeight = 10; // Höjden på fängelset
            int totalPeople = policeNum + citizenNum + thiefNum;
            int num_Of_Prisoners = 0;
            int num_Of_robberies = 0;
            List<Person> personList = new List<Person>();
            Random random = new Random();

            Console.CursorVisible = false; // Dölj pekaren

            string[] policeName = new string[]
            {
                "Polisen Svensson", "Detektiv Johnsson", "Sergeant Davidsson", "Inspektör Willhelmsson", "Kapten Andersson", "Löjtnant Martinsson",
                "Officer Börjesson", "Detektiv Göransson", "Sergant Carlberg", "Inspektör Nilsson"
            };
            for (int j = 0; j < policeNum; j++)
            {
                var police = new Police(policeName[j], random.Next(1, width - 1), random.Next(1, height - 1), 'P', random.Next(8));
                police.Inventory.Add("Handbojor");
                police.Inventory.Add("Bricka");
                police.Inventory.Add("Pistol");
                personList.Add(police);
            }
            string[] citizenName = new string[]
            {
                "John Simonsson", "Jane Karlsson", "Michael Jonsson", "Emily Davidsson", "David Williamsson", "Lisa Andersson",
                "Sarah Martinsson", "Robert Börjesson", "Maria Klarksson", "William Andersson", "Jennifer Jenssen", "Christoffer Grenborg",
                "Klara Klausson", "Richard Waldemarsson", "Patricia Tunberg", "Josef Mauritz", "Linda Hallberg", "Thomas Larsson", "Cynthia Garcia",
                "Charles Rodriguez", "Nancy Scottsson", "Daniel Ljungberg", "Susan Klingberg", "Mattias Wright", "Helene Adamsson",
                "Kevin Klausson", "Sandra Grenberg", "Andreas Redström", "Maria Cartelberg", "James Hallström", "Daniel Jakobsson"
            };
            for (int i = 0; i < citizenNum; i++)
            {
                var citizen = new Citizen(citizenName[i], random.Next(1, width - 1), random.Next(1, height - 1), 'C', random.Next(8), true);
                citizen.Inventory.Add("Nycklar");
                citizen.Inventory.Add("Mobiltelefon");
                citizen.Inventory.Add("Plånbok");
                citizen.Inventory.Add("Klocka");
                personList.Add(citizen);
            }
            string[] thiefName = new string[]
            {
                "Tommy", "Susie", "Bobby", "Steve", "Vicky", "Danny", "Rita", "Eddie", "Maggie",
                "Frankie", "Lenny", "Connie", "Ronny", "Lucy", "Harry", "Penny", "Vinny", "Mia", "Johnny", "Gina", "Larry"
            };
            for (int i = 0; i < thiefNum; i++)
            {
                var thief = new Thief(thiefName[i], random.Next(1, width - 1), random.Next(1, height - 1), 'T', false, random.Next(8));
                personList.Add(thief);
            }

            //  lista för de senaste händelserna.
            List<string> latestEvents = new List<string>();

            // Placera gubbarnas positioner i 2 arrayer.
            int[] x_Positions = new int[totalPeople];
            int[] y_Positions = new int[totalPeople];
            for (int i = 0; i < totalPeople; i++)
            {
                x_Positions[i] = personList[i].X_coord;
                y_Positions[i] = personList[i].Y_coord;
            }

            while (true)
            {

                for (int i = 0; i < 10; i++) //Antal gubbar som får ny riktning denna sekvens.
                {
                    personList[random.Next(totalPeople)].Direction = random.Next(8);
                }
                for (int steps = 0; steps < 5; steps++)//Antal steg i samma rikning.
                {
                    Console.Clear();
                    //____Rita staden____________
                    for (int i = 0; i <= width; i++)
                    {
                        Console.SetCursorPosition(i, 0);
                        Console.Write("-"); // ritar övre delen 
                        Console.SetCursorPosition(i, height + 1);
                        Console.Write("-"); // ritar undre delen 
                    }
                    for (int i = 0; i <= height + 1; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write("|");
                        Console.SetCursorPosition(width + 1, i);
                        Console.Write("|");
                    }

                    //____Rita fängelset_________________
                    Console.WriteLine("");
                    Console.SetCursorPosition(prisonX + 5, 0);
                    Console.Write("PRISON");
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
                    //____Rita fattighuset_________________
                    Console.WriteLine("");
                    Console.SetCursorPosition(poorHouseX + 5, poorHouseY - 1);
                    Console.Write("POORHOUSE");
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

                    //________Uppdatera och rita alla gubbar______________
                    for (int i = 0; i < totalPeople; i++)
                    {
                        int direction = personList[i].Direction;

                        Person person = personList[i];
                        int originalForegroundColor = (int)Console.ForegroundColor; // Spara den ursprungliga färgen
                        if (person is Police)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        else if (person is Citizen)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (person is Thief)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        //______Om personen(Citizen) sitter i fattighuset eller inte_____________________________
                        if (personList[i].PoorHouseInmate == true)//Om tjuven sitter i fängelset. 
                        {
                            UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                            if (y_Positions[i] > poorHouseY + (poorHouseHeight - 1)) { y_Positions[i] = poorHouseY + 1; }
                            else if (y_Positions[i] < poorHouseY + 1) { y_Positions[i] = poorHouseY + (poorHouseHeight - 1); }
                            if (x_Positions[i] > poorHouseX + (poorHouseWidth - 1)) { x_Positions[i] = poorHouseX + 1; }
                            else if (x_Positions[i] < poorHouseX + 1) { x_Positions[i] = poorHouseX + (poorHouseWidth - 1); }
                        }
                        else
                        {
                            UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                            if (y_Positions[i] > height) { y_Positions[i] = 1; }
                            else if (y_Positions[i] < 1) { y_Positions[i] = height; }
                            if (x_Positions[i] > width) { x_Positions[i] = 1; }
                            else if (x_Positions[i] < 1) { x_Positions[i] = width; }
                        }
                        //______Om personen(Tjuven) sitter i fängelset eller inte_____________________________
                        if (personList[i].PrisonInmate == true)//Om tjuven sitter i fängelset. 
                        {
                            UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                            if (y_Positions[i] > prisonY + (prisonHeight - 1)) { y_Positions[i] = prisonY + 1; }
                            else if (y_Positions[i] < prisonY + 1) { y_Positions[i] = prisonY + (prisonHeight - 1); }
                            if (x_Positions[i] > prisonX + (prisonWidth - 1)) { x_Positions[i] = prisonX + 1; }
                            else if (x_Positions[i] < prisonX + 1) { x_Positions[i] = prisonX + (prisonWidth - 1); }
                        }
                        else
                        {
                            UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                            if (y_Positions[i] > height) { y_Positions[i] = 1; }
                            else if (y_Positions[i] < 1) { y_Positions[i] = height; }
                            if (x_Positions[i] > width) { x_Positions[i] = 1; }
                            else if (x_Positions[i] < 1) { x_Positions[i] = width; }
                        }
                        Console.SetCursorPosition(x_Positions[i], y_Positions[i]);
                        Console.Write(personList[i].Symbol);
                        Console.ForegroundColor = (ConsoleColor)originalForegroundColor;
                    }

                    // Kontrollera om flera gubbar befinner sig på samma position Klar
                    for (int i = 0; i < totalPeople; i++)
                    {
                        for (int j = i + 1; j < totalPeople; j++)
                        {
                            if (x_Positions[i] == x_Positions[j] && y_Positions[i] == y_Positions[j])
                            {
                                var gubbe1 = personList[i];
                                var gubbe2 = personList[j];
                                if (gubbe1 is Police && gubbe2 is Thief)
                                {
                                    string eventDescription = $"{gubbe1.Namn} har fångat tjuven {gubbe2.Namn}!";
                                    latestEvents.Add(eventDescription);
                                    Police police = (Police)gubbe1;
                                    Thief thief = (Thief)gubbe2;
                                    police.CatchThief(thief);
                                    personList[j].PrisonInmate = true;
                                    y_Positions[i] = 3;
                                    x_Positions[j] = 106;
                                    num_Of_Prisoners++;
                                }
                                // Logik för att identifiera händelser (polis fångar tjuv, tjuv rånar medborgare osv.)
                                else if (gubbe1 is Citizen && gubbe2 is Thief)
                                {
                                    string eventDescription = $"Tjuven {gubbe2.Namn} har rånat medborgaren {gubbe1.Namn}!";
                                    latestEvents.Add(eventDescription);
                                    Citizen citizen = (Citizen)gubbe1;
                                    Thief thief = (Thief)gubbe2;
                                    thief.Steal(citizen);
                                    num_Of_robberies++;
                                }
                                else if (gubbe1 is Police && gubbe2 is Citizen)
                                {
                                    string eventDescription = $"{gubbe1.Namn} säger Hej! till medborgaren {gubbe2.Namn}!";
                                    latestEvents.Add(eventDescription);
                                }
                                if (latestEvents.Count > 5)
                                {
                                    latestEvents.RemoveAt(0);
                                }
                            }
                        }
                    }

                    Console.SetCursorPosition(103 + (prisonWidth + 5), 1);
                    Console.WriteLine("Antal rån: " + num_Of_robberies);
                    Console.SetCursorPosition(103 + (prisonWidth + 5), 2);
                    Console.WriteLine("Antal tagna tjuvar: " + num_Of_Prisoners);

                    for (int i = 0; i < totalPeople; i++)
                    {
                        Person person = personList[i];
                        if (person is Citizen)
                        {
                            var citizen = (Citizen)person;

                            if (citizen.Inventory.Count == 0)
                            {

                                personList[i].PoorHouseInmate = true;
                            }
                        }
                    }

                    // Skriv ut de senaste händelserna Klar
                    Console.SetCursorPosition(0, height + 2);
                    Console.WriteLine("Senaste händelser:");
                    int eventCount = 1;
                    foreach (var ev in latestEvents)
                    {
                        Console.SetCursorPosition(0, height + 2 + eventCount);
                        Console.WriteLine($"{eventCount}. {ev}");
                        eventCount++;
                    }
                    Thread.Sleep(600);
                }
            }
        }
        static void UpdatePosition(ref int x, ref int y, int direction)
        {
            // Uppdatera positionen baserat på riktningen
            switch (direction)
            {
                case 0: // Uppåt
                    y--;
                    break;
                case 1: // Neråt
                    y++;
                    break;
                case 2: // Höger
                    x++;
                    break;
                case 3: // Vänster
                    x--;
                    break;
                case 4: // Snedt höger upp
                    y--; x++;
                    break;
                case 5: // Snedt vänster upp
                    y--; x--;
                    break;
                case 6: // Snedt höger ned
                    y++; x++;
                    break;
                case 7: // Snedt vänster ned
                    y++; x--;
                    break;
            }
        }

    }

}