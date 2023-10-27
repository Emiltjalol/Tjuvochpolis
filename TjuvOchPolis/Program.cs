using System;
using System.Collections.Generic;
using System.Threading;
using static TjuvOchPolis.Draw;

namespace TjuvOchPolis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 100; // Bredden på fyrkanten
            int height = 25; // Höjden på fyrkanten
            int PoliceNum = 10; // Nummer av police
            int CitizenNum = 20; // Nummer av Citizen
            int thiefNum = 10; // Nummer av Thief 
            int prisonX = width + 2; // X-koordinat för fängelsets övre vänstra hörn
            int prisonY = 1; // Y-koordinat för fängelsets övre vänstra hörn
            int prisonWidth = 15; // Bredden på fängelset
            int prisonHeight = 10; // Höjden på fängelset

            Random random = new Random();
            List<Police> policeList = PoliceFactory.CreatePolice(PoliceNum, width, height, random);
            List<Citizen> citizenList = CitizenFactory.CreateCitizens(CitizenNum, width, height, random);
            List<Thief> thiefList = ThiefFactory.CreateThieves(thiefNum, width, height, random);


            List<Person> personList = new List<Person>();

            int totalGubbar = PoliceNum + CitizenNum + thiefNum;

            Console.CursorVisible = false;

            // Gör poliser
            personList.AddRange(policeList);
            // Gör medborgare
            personList.AddRange(citizenList);
            // Gör tjuvar
            personList.AddRange(thiefList);       


            int[] xPositions = new int[totalGubbar];
            int[] yPositions = new int[totalGubbar];
            int[] directions = new int[totalGubbar];

            //  lista för de senaste händelserna Klar
            List<string> latestEvents = new List<string>();

            // Skapa dictionaries för att hålla reda på x- och y-koordinater för gubbarna Klar
            Dictionary<int, int> gubbarPåPositionX = new Dictionary<int, int>();
            Dictionary<int, int> gubbarPåPositionY = new Dictionary<int, int>();

            // Placera gubbarna i slumpmässiga startpositioner
            for (int i = 0; i < totalGubbar; i++)
            {
                xPositions[i] = random.Next(1, width - 1);
                yPositions[i] = random.Next(1, height - 1);
                gubbarPåPositionX[i] = xPositions[i];
                gubbarPåPositionY[i] = yPositions[i];
                directions[i] = random.Next(6); // Slumpmässig riktning
            }

            while (true)
            {
                Console.Clear(); // Rensa konsollen för att uppdatera positioner Klar

                Draw.DrawCity(width, height);
                Draw.DrawPrison(prisonX, prisonY, prisonWidth, prisonHeight);

                // Uppdatera och rita alla gubbar
                for (int i = 0; i < totalGubbar; i++)
                {
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

                    person.Draw(xPositions[i], yPositions[i]);

                    Console.ForegroundColor = (ConsoleColor)originalForegroundColor;

                    // Uppdatera positionen
                    int direction = directions[i];
                    UpdatePosition(ref xPositions[i], ref yPositions[i], direction, width, height);

                    // Uppdatera dictionaries för gubbarnas positioner
                    gubbarPåPositionX[i] = xPositions[i];
                    gubbarPåPositionY[i] = yPositions[i];
                }

                // Kontrollera om flera gubbar befinner sig på samma position Klar
                for (int i = 0; i < totalGubbar; i++)
                {
                    for (int j = i + 1; j < totalGubbar; j++)
                    {
                        if (gubbarPåPositionX[i] == gubbarPåPositionX[j] && gubbarPåPositionY[i] == gubbarPåPositionY[j])
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
                            }
                            // Logik för att identifiera händelser (polis fångar tjuv, tjuv rånar medborgare osv.)
                            else if (gubbe1 is Citizen && gubbe2 is Thief)
                            {
                                string eventDescription = $"Tjuven {gubbe2.Namn} har rånat medborgaren {gubbe1.Namn}!";
                                latestEvents.Add(eventDescription);
                                Citizen citizen = (Citizen)gubbe1;
                                Thief thief = (Thief)gubbe2;
                                thief.Steal(citizen);

                            }

                            // ta bort detta för att slippa spam?

                            //////else if (gubbe1 is Police && gubbe2 is Citizen)
                            //////{
                            //////    string eventDescription = $" {gubbe1.Namn} säger Hej! till medborgaren {gubbe2.Namn}!";
                            //////    latestEvents.Add(eventDescription);
                            //////}
                            if (latestEvents.Count > 5)
                            {
                                latestEvents.RemoveAt(0);
                            }
                        }
                    }
                }

                // Skriv ut de senaste händelserna Klar
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine("Senaste händelser:");
                int eventCount = 1;
                foreach (var ev in latestEvents)
                {
                    Console.SetCursorPosition(0, height + 1 + eventCount);
                    Console.WriteLine($"{eventCount}. {ev}");
                    eventCount++;
                }

                Thread.Sleep(400);
            }
        }

      // UPPDATERAT DETTA -  EMIL
            static void UpdatePosition(ref int x, ref int y, int direction, int width, int height)
            {
                // Uppdatera positionen baserat på riktningen
                switch (direction)
                {
                    case 0: // Uppåt
                        y = (y - 1 + height) % height;
                        break;
                    case 1: // Neråt
                        y = (y + 1) % height;
                        break;
                    case 2: // Höger
                        x = (x + 1) % width;
                        break;
                    case 3: // Vänster
                        x = (x - 1 + width) % width;
                        break;
                    case 4: // Snedt höger upp
                        y = (y - 1 + height) % height;
                        x = (x + 1) % width;
                        break;
                    case 5: // Snedt vänster upp
                        y = (y - 1 + height) % height;
                        x = (x - 1 + width) % width;
                        break;
                    case 6: // Snedt höger ned
                        y = (y + 1) % height;
                        x = (x + 1) % width;
                        break;
                    case 7: // Snedt vänster ned
                        y = (y + 1) % height;
                        x = (x - 1 + width) % width;
                        break;
                }
            }
        }
}