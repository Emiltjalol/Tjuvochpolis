using System;
using System.Collections.Generic;
using System.Threading;

namespace TjuvOchPolis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 90; // Bredden på fyrkanten
            int height = 25; // Höjden på fyrkanten
            List<Person> personList = new List<Person>();
            Random random = new Random();
            int totalGubbar = 60;

            Console.CursorVisible = false; // Dölj pekaren

            List<Person> gubbar = new List<Person>();

            string[] policeName = new string[]{
                "Officer Smith", "Detective Johnson", "Sergeant Davis",
                "Inspector Wilson", "Captain Anderson", "Lieutenant Martinez",
                "Officer Baker", "Detective Clark",
                "Sergeant White", "Inspector Harris"};

            for (int j = 0; j < 10; j++)
            {
                int randomX = random.Next(1, width - 1);
                int randomY = random.Next(1, height - 1);
                var police = new Police(policeName[j], randomX, randomY, 'P');
                police.Inventory.Add("Handcuffs");
                police.Inventory.Add("Badge");
                police.Inventory.Add("Gun");
                personList.Add(police);
            }

            string[] citizenName = new string[]{
                "John Smith", "Jane Doe", "Michael Johnson", "Emily Davis", "David Wilson", "Lisa Anderson",
                "Sarah Martinez", "Robert Baker", "Mary Clark", "William White", "Jennifer Harris", "Christopher Taylor",
                "Karen Lewis", "Richard Walker", "Patricia Turner", "Joseph Moore", "Linda Hall", "Thomas Mitchell", "Cynthia Garcia",
                "Charles Rodriguez", "Nancy Scott", "Daniel Young", "Susan King", "Matthew Wright", "Helen Adams",
                "Kevin Campbell", "Sandra Green", "Andrew Reed", "Maria Carter", "James Hall", "Dwayne Johnsson"
            };

            for (int i = 0; i < 30; i++)
            {
                int randomX = random.Next(1, width - 1);
                int randomY = random.Next(1, height - 1);
                var citizen = new Citizen(citizenName[i], randomX, randomY, 'C');
                citizen.Inventory.Add("Keys");
                citizen.Inventory.Add("Cellphone");
                citizen.Inventory.Add("Wallet");
                citizen.Inventory.Add("Watch");
                personList.Add(citizen);
            }

            // Create the thieves

            string[] thiefName = new string[]{
                "Tommy the Sneak", "Sly Susie", "Bobby the Bandit", "Shadow Steve", "Vicky Vandal",
                "Danny the Pickpocket", "Rita the Rascal", "Eddie the Escapist", "Maggie the Mischief",
                "Frankie the Filcher", "Lenny the Looter", "Connie the Crook", "Ronny the Robber", "Lucy the Lawbreaker",
                "Harry the Hoodlum", "Penny the Pilferer", "Vinny the Villain", "Mia the Marauder", "Johnny the Jewel Thief", "Gina the Grifter", "Larry the Imposter"
            };

            for (int i = 0; i < 20; i++)
            {
                int randomX = random.Next(1, width - 1);
                int randomY = random.Next(1, height - 1);
                var thief = new Thief(thiefName[i], randomX, randomY, 'T');
                personList.Add(thief);
            }

            int[] xPositions = new int[totalGubbar];
            int[] yPositions = new int[totalGubbar];

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
            }

            while (true)
            {
                Console.Clear(); // Rensa konsollen för att uppdatera positioner Klar

                // Rita fyrkanten
                for (int i = 0; i <= width; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("-"); // ritar övre delen 
                    Console.SetCursorPosition(i, height);
                    Console.Write("-"); // ritar undre delen 
                }

                for (int i = 0; i <= height; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("|");
                    Console.SetCursorPosition(width, i);
                    Console.Write("|");
                }

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
                    int direction = random.Next(8);
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
                                string eventDescription = $"Polisen {gubbe1.Namn} har fångat tjuven {gubbe2.Namn}!";
                                latestEvents.Add(eventDescription);
                            }
                            // Logik för att identifiera händelser (polis fångar tjuv, tjuv rånar medborgare osv.)
                            else if (gubbe1 is Citizen && gubbe2 is Thief)
                            {
                                string eventDescription = $"Tjuven {gubbe2.Namn} har rånat medborgaren {gubbe1.Namn}!";
                                latestEvents.Add(eventDescription);
                            }
                            else if (gubbe1 is Police && gubbe2 is Citizen)
                            {
                                string eventDescription = $"Polisen {gubbe1.Namn} hjälper medborgaren {gubbe2.Namn}!";
                                latestEvents.Add(eventDescription);
                            }
                        }
                    }
                }

                // Om listan över senaste händelser har fler än fem händelser, ta bort den äldsta Klar
                if (latestEvents.Count > 5)
                {
                    latestEvents.RemoveAt(0);
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

                Thread.Sleep(600);
            }
        }

        static void UpdatePosition(ref int x, ref int y, int direction, int width, int height)
        {
            // Uppdatera positionen baserat på riktningen
            switch (direction)
            {
                case 0: // Uppåt
                    if (y > 1)
                        y--;
                    break;
                case 1: // Neråt
                    if (y < height - 2)
                        y++;
                    break;
                case 2: // Höger
                    if (x < width - 2)
                        x++;
                    break;
                case 3: // Vänster
                    if (x > 1)
                        x--;
                    break;
                case 4: // Snedt höger upp
                    if (y > 1 && x < width - 2)
                    {
                        y--;
                        x++;
                    }
                    break;
                case 5: // Snedt vänster upp
                    if (y > 1 && x > 1)
                    {
                        y--;
                        x--;
                    }
                    break;
                case 6: // Snedt höger ned
                    if (y < height - 2 && x < width - 2)
                    {
                        y++;
                        x++;
                    }
                    break;
                case 7: // Snedt vänster ned
                    if (y < height - 2 && x > 1)
                    {
                        y++;
                        x--;
                    }
                    break;
            }
        }
    }
}
