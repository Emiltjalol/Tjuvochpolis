namespace TjuvOchPolis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 90; // Bredden på fyrkanten
            int height = 25; // Höjden på fyrkanten
            int numGubbarP = 10; // Antal "P"-gubbar
            int numGubbarT = 20; // Antal "T"-gubbar
            int numGubbarM = 30; // Antal "M"-gubbar
            int totalGubbar = numGubbarP + numGubbarT + numGubbarM;

            Console.CursorVisible = false; // Dölj pekaren

            List<Person> gubbar = new List<Person>();

            // Skapa "P"-gubbar
            for (int i = 0; i < numGubbarP; i++)
            {
                gubbar.Add(new Person('P'));
            }

            // Skapa "M"-gubbar
            for (int i = 0; i < numGubbarM; i++)
            {
                gubbar.Add(new Person('M'));
            }

            // Skapa "T"-gubbar
            for (int i = 0; i < numGubbarT; i++)
            {
                gubbar.Add(new Person('T'));
            }

            int[] xPositions = new int[totalGubbar];
            int[] yPositions = new int[totalGubbar];
            Random random = new Random();

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
                    Person person = gubbar[i];
                    person.Draw(xPositions[i], yPositions[i]);

                    int direction = random.Next(8); // Slumpmässig riktning

                    // Uppdatera positionen för gubben
                    UpdatePosition(ref xPositions[i], ref yPositions[i], direction, width, height);

                    // Lägg till gubben på dess nya position i dictionary
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
                        // Här kan du lägga till logik för att skriva ut meddelanden i konsollen
                        if (position.Value.Exists(person => person.Symbol == 'T') &&
                            position.Value.Exists(person => person.Symbol == 'P'))
                        {
                            Console.SetCursorPosition(0, height + 1);
                            Console.Write("En tjuv har blivit tagen av polisen och åker in i fängelset");

                        }
                        else if (position.Value.Exists(person => person.Symbol == 'T') &&
                                 position.Value.Exists(person => person.Symbol == 'M'))
                        {
                            Console.SetCursorPosition(0, height + 2);
                            Console.Write("En tjuv har rånat en medborgare");

                        }
                    }
                }

                // Vänta en kort stund för att visa rörelse
                Thread.Sleep(900);
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
    
