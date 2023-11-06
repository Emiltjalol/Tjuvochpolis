using System;
using System.ComponentModel.Design;
using System.Drawing;
namespace Tjuv_Polis_MinUtveckling26Okt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> personsList = new List<Person>();//Alla gubbar
            List<string> latestEvents = new List<string>(); //lista för de senaste händelserna.
            int width = 100; // Bredden på fyrkanten
            int height = 25; // Höjden på fyrkanten
            int policeNum = 10; // Nummer av police
            int citizenNum = 30; // Nummer av Citizen
            int thiefNum = 20; // Nummer av Thief 
            int prisonX = width + 3; // X-koordinat för fängelsets övre vänstra hörn
            int prisonY = 1; // Y-koordinat för fängelsets övre vänstra hörn
            int prisonWidth = 15; // Bredden på fängelset
            int prisonHeight = 10; // Höjden på fängelset
            int poorHouseX = width + 3; // X-koordinat för fängelsets övre vänstra hörn
            int poorHouseY = 15; // Y-koordinat för fängelsets övre vänstra hörn
            int poorHouseWidth = 40; // Bredden på fängelset
            int poorHouseHeight = 10; // Höjden på fängelset
            int totalPeople = policeNum + citizenNum + thiefNum; 
            int numOfRobberies = 0;
            int thivesInPrison = 0;
            int citizensInPoorHouse = 0;
            int inventoryRollDelay = 0;
            int inventoryStepCount = 0;
            bool autoScroll = true;
            int eventCount = 1; // Börja med nummer 1 för den senaste händelsen
            Random random = new Random();
            Console.CursorVisible = false; // Dölj pekaren

            string[] policeName = new string[]
            {
                "Polisen Svensson", "Polisen Johnsson", "Polisen Davidsson", "Polisen Willhelmsson", "Polisen Andersson", "Polisen Martinsson",
                "Polisen Börjesson", "Polisen Göransson", "Polisen Carlberg", "Polisen Nilsson"
            };
            for (int i = 0; i < policeNum; i++)
            {
                var police = new Police(policeName[i % policeName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'P', random.Next(8));
                personsList.Add(police);
            }
            string[] citizenName = new string[]
            {
                "Medborgare Simonsson", "Medborgare Karlsson", "Medborgare Jonsson", "Medborgare Davidsson", "Medborgare Williamsson", "Medborgare Andersson",
                "Medborgare Martinsson", "Medborgare Börjesson", "Medborgare Klarksson", "Medborgare Andersson", "Medborgare Jenssen", "Medborgare Grenborg",
                "Medborgare Klausson", "Medborgare Waldemarsson", "Medborgare Tunberg", "Medborgare Mauritz", "Medborgare Hallberg", "Medborgare Larsson", "Medborgare Garcia",
                "Medborgare Rodriguez", "Medborgare Scottsson", "Medborgare Ljungberg", "Medborgare Klingberg", "Medborgare Wright", "Medborgare Adamsson",
                "Medborgare Klausson", "Medborgare Grenberg", "Medborgare Redström", "Medborgare Cartelberg", "Medborgare Hallström", "Medborgare Jakobsson"
            };
            for (int i = 0; i < citizenNum; i++)
            {
                var citizen = new Citizen(citizenName[i % citizenName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'C', random.Next(8), false);
                citizen.Inventory.Add("Nycklar");
                citizen.Inventory.Add("Mobil");
                citizen.Inventory.Add("Plånbok");
                citizen.Inventory.Add("Klocka");
                personsList.Add(citizen);
            }
            string[] thiefName = new string[]
            {
                "Tjuven Tommy", "Tjuven Susie", "Tjuven Bobby", "Tjuven Steve", "Tjuven Vicky", "Tjuven Danny", "Tjuven Rita", "Tjuven Eddie", "Tjuven Maggie",
                "Tjuven Frankie", "Tjuven Lenny", "Tjuven Connie", "Tjuven Ronny", "Tjuven Lucy", "Tjuven Harry", "Tjuven Penny", "Tjuven Vinny", "Tjuven Mia", "Tjuven Johnny", "Tjuven Gina", "Tjuven Larry"
            };
            for (int i = 0; i < thiefNum; i++)
            {
                var thief = new Thief(thiefName[i % citizenName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'T', false, random.Next(8));
                personsList.Add(thief);
            }
            
            while (true)
            {
                
                totalPeople = policeNum + citizenNum + thiefNum;
                int[] x_Positions = new int[totalPeople];
                int[] y_Positions = new int[totalPeople];
                for (int i = 0; i < totalPeople; i++)
                {
                    x_Positions[i] = personsList[i].X_coord;
                    y_Positions[i] = personsList[i].Y_coord;
                }
                for (int i = 0; i < 5; i++) //Antal gubbar som får ny riktning denna sekvens.
                {
                    personsList[random.Next(totalPeople)].Direction = random.Next(8);
                    personsList[random.Next(totalPeople)].Direction = random.Next(8);
                }
                //Skapa nya karaktärer och styrning av Inventory.
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    char key = char.ToUpper(keyInfo.KeyChar);
                    switch (key)
                    {
                        case 'C':
                            citizenNum++;
                            Citizen citizen = new Citizen(citizenName[random.Next(citizenName.Length) % citizenName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'C', random.Next(8), false);
                            citizen.Inventory.Add("Nycklar");
                            citizen.Inventory.Add("Mobiltelefon");
                            citizen.Inventory.Add("Plånbok");
                            citizen.Inventory.Add("Klocka");
                            personsList.Add(citizen);
                            break;
                        case 'T':
                            thiefNum++;
                            Thief thief = new Thief(thiefName[random.Next(thiefName.Length) % thiefName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'T', false, random.Next(8));
                            personsList.Add(thief);
                            break;
                        case 'P':
                            policeNum++;
                            Police police = new Police(policeName[random.Next(policeName.Length) % policeName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'P', random.Next(8));
                            personsList.Add(police);
                            break;
                        case 'A'://Automatisk Inventory scroll..
                            if (autoScroll == true) { autoScroll = false; }
                            else if (autoScroll == false) { autoScroll = true; }
                            break;
                        case 'W'://Scrorlla Inventory uppåt
                            if (autoScroll == true) { autoScroll = false; }
                            if (inventoryStepCount > 0) { inventoryStepCount--; inventoryStepCount--; }
                            break;
                        case 'S'://Scrorlla Inventory nedåt
                            if (autoScroll == true) { autoScroll = false; }
                            if (inventoryStepCount == totalPeople - 2) {  inventoryStepCount++; }
                            else if (inventoryStepCount < totalPeople - 5) { inventoryStepCount++; inventoryStepCount++; }
                            break;
                    }
                }
                
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
                //____Rita fattighuset_________________
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

                //________Uppdatera och rita alla gubbar______________
                for (int i = 0; i < totalPeople; i++)
                {
                    int direction = personsList[i].Direction;

                    Person person = personsList[i];
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
                    if (personsList[i].PoorHouseInmate == true)//Om tjuven sitter i fängelset. 
                    {
                        UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                        if (y_Positions[i] > poorHouseY + (poorHouseHeight - 1)) { y_Positions[i] = poorHouseY + 1; }
                        else if (y_Positions[i] < poorHouseY + 1) { y_Positions[i] = poorHouseY + (poorHouseHeight - 1); }
                        if (x_Positions[i] > poorHouseX + (poorHouseWidth - 1)) { x_Positions[i] = poorHouseX + 1; }
                        else if (x_Positions[i] < poorHouseX + 1) { x_Positions[i] = poorHouseX + (poorHouseWidth - 1); }
                    }
                    //______Om personen(Tjuven) sitter i fängelset eller inte_____________________________
                    else if (personsList[i].PrisonInmate == true)//Om tjuven sitter i fängelset. 
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
                    Console.Write(personsList[i].Symbol);
                    Console.ForegroundColor = (ConsoleColor)originalForegroundColor;
                }

                // Kontrollera om flera gubbar befinner sig på samma position Klar
                for (int i = 0; i < totalPeople; i++)
                {
                    for (int j = i + 1; j < totalPeople; j++)
                    {
                        if (x_Positions[i] == x_Positions[j] && y_Positions[i] == y_Positions[j])
                        {
                            var meet_1 = personsList[i];
                            var meet_2 = personsList[j];
                            if (meet_1 is Police && meet_2 is Thief)
                            {
                                Police police = (Police)meet_1;
                                Thief thief = (Thief)meet_2;
                                if (thief.Inventory.Count > 0)
                                {                                    
                                    string eventDescription = $"{meet_1.Name} har fångat {meet_2.Name}!";
                                    latestEvents.Add(eventDescription);
                                    
                                    personsList[j].PrisonInmate = true;
                                    y_Positions[i] = 3;
                                    x_Positions[j] = 106;
                                    thivesInPrison++;
                                }
                                police.CatchThief(thief);
                            }
                            // Logik för att identifiera händelser (polis fångar tjuv, tjuv rånar medborgare osv.)
                            else if (meet_1 is Citizen && meet_2 is Thief)
                            {
                                Citizen citizen = (Citizen)meet_1;
                                Thief thief = (Thief)meet_2;
                                if (citizen.Inventory.Count > 0)
                                {
                                    string eventDescription = $"{meet_2.Name} har rånat {meet_1.Name}!";
                                    latestEvents.Add(eventDescription);
                                    thief.Steal(citizen);
                                    numOfRobberies++;
                                    eventCount++;
                                }
                            }
                            else if (meet_1 is Police && meet_2 is Citizen)
                            {
                                Police police = (Police)meet_1;
                                Citizen citizen = (Citizen)meet_2;
                                if (citizen.Inventory.Count == 0)
                                {
                                    string eventDescription = $"{meet_1.Name} Kastar {meet_2.Name} i fattighuset!";
                                    latestEvents.Add(eventDescription);
                                    citizen.PoorHouseInmate = true;
                                    citizen.Inventory.Add("FATTIGHUSET");
                                    citizensInPoorHouse++;
                                    eventCount++;
                                }
                            }
                            if (latestEvents.Count > 12)
                            {
                                latestEvents.RemoveAt(0);
                            }
                        }
                    }
                }

                // Skriv ut de senaste händelserna Klar
                Console.SetCursorPosition(width - 15, height + 2);
                Console.WriteLine("Senaste händelser:");
                for (int i = 0; i < 12; i++) // Skriv ut de senaste 10 händelserna
                {
                    int index = latestEvents.Count - 1 - i; // Börja från det senaste och gå bakåt i listan
                    if (index >= 0)
                    {
                        Console.SetCursorPosition(width - 15, height + 3 + i); // Justera radpositionen
                        Console.WriteLine($"{latestEvents[index]}");
                        
                    }
                    else
                    {
                        break; // Om listan är kortare än 10, bryt loopen
                    }
                }

                //____Räkneverket uppe i högra hörnet_________________
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

                //___Inventory______________________________
                Console.SetCursorPosition(0, height + 2);
                Console.WriteLine("upp[W] ned[S] auto[A]  INVENTORY: ");
                Console.SetCursorPosition(0, height + 3);
                int rollListLength = 12;
                int inventoryCount = 0;
                inventoryCount = inventoryStepCount;
                for (int i = 0; i < rollListLength; i++)
                {
                    if (inventoryCount >= totalPeople - 1) { inventoryStepCount = 0; }
                    Console.Write($"{personsList[inventoryCount].Name}: "); 
                    foreach (string item in personsList[inventoryCount].Inventory)
                    {
                        Console.Write(" " + item);
                    }
                    Console.WriteLine();
                    if (inventoryCount >= totalPeople - 1) { break; }
                    if (inventoryCount < totalPeople - 1) { inventoryCount++; }
                }
                //____För att bromsa rullistan 4 sekvenser______
                if (autoScroll == true) 
                {
                    if (inventoryRollDelay < 4) { inventoryRollDelay++; }
                    if (inventoryRollDelay == 3)
                    {
                        if (inventoryCount < totalPeople) inventoryStepCount++; inventoryRollDelay = 0;
                    }
                }

                Thread.Sleep(400);//Hastigheten i staden.

                for (int i = 0; i < totalPeople; i++)//Sparar dom nya positionerna i Arrayerna.
                {
                      personsList[i].X_coord = x_Positions[i];
                      personsList[i].Y_coord = y_Positions[i];
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