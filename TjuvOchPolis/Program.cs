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
            "Officer Smith","Detective Johnson","Sergeant Davis",
            "Inspector Wilson","Captain Anderson","Lieutenant Martinez",
            "Officer Baker","Detective Clark",
            "Sergeant White","Inspector Harris"};

            for (int j = 0; j < 10; j++)
            {
                int randomX = random.Next(1, width - 1);
                int randomY = random.Next(1, height - 1);
                var police = new Police(policeName[j], randomX, randomY, 'P');
                police.XCoordinate = randomX; // Uppdatera XCoordinate för polisen
                police.YCoordinate = randomY; // Uppdatera YCoordinate för polisen
                police.Inventory.Add("Handcuffs");
                police.Inventory.Add("Badge");
                police.Inventory.Add("Gun");
                personList.Add(police);
            }

            string[] citizenName = new string[]{
            "John Smith","Jane Doe","Michael Johnson","Emily Davis","David Wilson","Lisa Anderson",
            "Sarah Martinez","Robert Baker","Mary Clark","William White","Jennifer Harris","Christopher Taylor",
            "Karen Lewis","Richard Walker","Patricia Turner","Joseph Moore","Linda Hall","Thomas Mitchell","Cynthia Garcia",
            "Charles Rodriguez","Nancy Scott","Daniel Young","Susan King","Matthew Wright","Helen Adams",
            "Kevin Campbell","Sandra Green","Andrew Reed","Maria Carter","James Hall","Dwayne Johnsson"
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

            // Create the thiefes

            string[] thiefName = new string[]{
            "Tommy the Sneak","Sly Susie","Bobby the Bandit","Shadow Steve","Vicky Vandal",
            "Danny the Pickpocket","Rita the Rascal","Eddie the Escapist","Maggie the Mischief",
            "Frankie the Filcher","Lenny the Looter","Connie the Crook","Ronny the Robber","Lucy the Lawbreaker",
            "Harry the Hoodlum","Penny the Pilferer","Vinny the Villain","Mia the Marauder","Johnny the Jewel Thief","Gina the Grifter","Larry the Imposter"
             };

            for (int i = 0; i < 20; i++)
            {
                int randomX = random.Next(1, width - 1);
                int randomY = random.Next(1, height - 1);
                var thief = new Thief(thiefName[i], randomX, randomY,'T');
                personList.Add(thief);
            }

            int[] xPositions = new int[totalGubbar];
            int[] yPositions = new int[totalGubbar];
            

            // Placera gubbarna i slumpmässiga startpositioner
            for (int i = 0; i < totalGubbar; i++)
            {
                xPositions[i] = random.Next(1, width - 1);
                yPositions[i] = random.Next(1, height - 1);
            }

            while (true)
            {
                Console.Clear(); // Rensa konsollen för att uppdatera positioner

                // Rita fyrkanten
                for (int i = 0; i <= width; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("-");  // ritar övre delen 
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

                // Skapa en dictionary för att hålla reda på vilka gubbar som befinner sig på varje position
                Dictionary<(int, int), List<Person>> gubbarPåPosition = new Dictionary<(int, int), List<Person>>();

                // Rita och uppdatera alla gubbar
                for (int i = 0; i < totalGubbar; i++)
                {
                    Person person = personList[i];

                    int originalForegroundColor = (int)Console.ForegroundColor; // Spara den ursprungliga färgen

                    if (person is Police) // Kontrollera om personen är en polis
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; // Ändra färgen till blå för polisen
                    }
                    else if (person is Citizen)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Ändra färgen till grön för medborgaren
                    }
                    else if (person is Thief)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Ändra färgen till röd för tjuven
                    }

                    person.Draw(xPositions[i], yPositions[i]);

                    // Återställ den ursprungliga färgen
                    Console.ForegroundColor = (ConsoleColor)originalForegroundColor;

                    // Uppdatera positionen och dictionary
                    int direction = random.Next(8); // Slumpmässig riktning
                    UpdatePosition(ref xPositions[i], ref yPositions[i], direction, width, height);

                    var position = (xPositions[i], yPositions[i]);
                    if (!gubbarPåPosition.ContainsKey(position))
                    {
                        gubbarPåPosition[position] = new List<Person>();
                    }
                    gubbarPåPosition[position].Add(person);
                }

                // Kolla om flera gubbar hamnar på samma position
                foreach (var position in gubbarPåPosition)
                {
                    if (position.Value.Count > 1)
                    {
                        var police = position.Value.OfType<Police>().FirstOrDefault();
                        var thief = position.Value.OfType<Thief>().FirstOrDefault();

                        if (police != null && thief != null)
                        {
                            Console.SetCursorPosition(0, height + 1);
                            Console.WriteLine($"Polisen {police.Namn} har fångat tjuven {thief.Namn}!");
                            
                        }

                    }
                    if (position.Value.Count > 1)
                    {
                        var citizen = position.Value.OfType<Citizen>().FirstOrDefault();
                        var thief = position.Value.OfType<Thief>().FirstOrDefault();

                        if (citizen != null && thief != null)
                        {
                            Console.SetCursorPosition(0, height + 1);
                            Console.WriteLine($"Tjuven {thief.Namn} har rånat medborgaren {citizen.Namn}!");

                        }

                    }
                }

                // Vänta en kort stund för att visa rörelse
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
    
