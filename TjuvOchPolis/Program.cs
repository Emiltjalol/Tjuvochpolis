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
            int policeNum = 20; // Nummer av police
            int citizenNum = 20; // Nummer av Citizen
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
            int inventoryRollList = 0;
            List<Person> personList = new List<Person>();
            Random random = new Random();

            Console.CursorVisible = false; // Dölj pekaren

            string[] policeName = new string[]
            {
                "Polisen Svensson", "Polisen Johnsson", "Polisen Davidsson", "Polisen Willhelmsson", "Polisen Andersson", "Polisen Martinsson",
                "Polisen Börjesson", "Polisen Göransson", "Polisen Carlberg", "Polisen Nilsson"
            };
            for (int j = 0; j < policeNum; j++)
            {
                var police = new Police(policeName[j % policeName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'P', random.Next(8));
                personList.Add(police);
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
                personList.Add(citizen);
            }
            string[] thiefName = new string[]
            {
                "Tjuven Tommy", "Tjuven Susie", "Tjuven Bobby", "Tjuven Steve", "Tjuven Vicky", "Tjuven Danny", "Tjuven Rita", "Tjuven Eddie", "Tjuven Maggie",
                "Tjuven Frankie", "Tjuven Lenny", "Tjuven Connie", "Tjuven Ronny", "Tjuven Lucy", "Tjuven Harry", "Tjuven Penny", "Tjuven Vinny", "Tjuven Mia", "Tjuven Johnny", "Tjuven Gina", "Tjuven Larry"
            };
            for (int i = 0; i < thiefNum; i++)
            {
                var thief = new Thief(thiefName[i % citizenName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'T', false, random.Next(8));
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

                    //test för att få in nya karaktärer
                    if (Console.KeyAvailable)
                    {
                        int i = 0;
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        char key = keyInfo.KeyChar;
                        switch (key)
                        {
                            case 'C':
                            case 'c':
                                Citizen citizen = new Citizen(citizenName[i % citizenName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'C', random.Next(8), false);
                                citizen.Inventory.Add("Nycklar");
                                citizen.Inventory.Add("Mobiltelefon");
                                citizen.Inventory.Add("Plånbok");
                                citizen.Inventory.Add("Klocka");
                                personList.Add(citizen);
                                citizenNum++;
                                citizensInPoorHouse++;
                                break;

                            case 'T':
                            case 't':
                                Thief thief = new Thief(thiefName[i % thiefName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'T', false, random.Next(8));
                                personList.Add(thief);
                                thiefNum++;
                                thivesInPrison++;
                                break;

                            case 'P':
                            case 'p':
                                
                                Police police = new Police(policeName[i % policeName.Length], random.Next(1, width - 1), random.Next(1, height - 1), 'P', random.Next(8));
                                personList.Add(police);
                                for (int j = 0; j < totalPeople; j++)
                                {
                                    x_Positions[j] = personList[j].X_coord;
                                    y_Positions[j] = personList[j].Y_coord;
                                }
                                policeNum++;
                                break;
                        }
                    }
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
                    Console.SetCursorPosition(poorHouseX + 5, poorHouseY - 1);
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
                        //______Om personen(Tjuven) sitter i fängelset eller inte_____________________________
                        else if (personList[i].PrisonInmate == true)//Om tjuven sitter i fängelset. 
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
                                var meet_1 = personList[i];
                                var meet_2 = personList[j];
                                if (meet_1 is Police && meet_2 is Thief)
                                {
                                    Police police = (Police)meet_1;
                                    Thief thief = (Thief)meet_2;
                                    if (thief.Inventory.Count > 0)
                                    {                                    
                                        string eventDescription = $"{meet_1.Namn} har fångat {meet_2.Namn}!";
                                        latestEvents.Add(eventDescription);
                                        personList[j].PrisonInmate = true;
                                        y_Positions[i] = 3;
                                        x_Positions[j] = 106;
                                        thivesInPrison++;
                                      
                                    }
                                    police.CatchThief(thief);
                                }
                                // Logik för att identifiera händelser (polis fångar tjuv, tjuv rånar medborgare osv.)
                                else if (meet_1 is Citizen && meet_2 is Thief)
                                {
                                    string eventDescription = $"{meet_2.Namn} har rånat {meet_1.Namn}!";
                                    latestEvents.Add(eventDescription);
                                    Citizen citizen = (Citizen)meet_1;
                                    Thief thief = (Thief)meet_2;
                                    thief.Steal(citizen);
                                    numOfRobberies++;
                                }
                                else if (meet_1 is Police && meet_2 is Citizen)
                                {
                                    Police police = (Police)meet_1;
                                    Citizen citizen = (Citizen)meet_2;
                                    if (citizen.Inventory.Count > 0)
                                    {
                                        string eventDescription = $"{meet_1.Namn} säger Hej! till {meet_2.Namn}!";
                                        latestEvents.Add(eventDescription);
                                    }
                                    else
                                    {
                                        string eventDescription = $"{meet_1.Namn} Kastar {meet_2.Namn} in i fattighetshemmet!";
                                        latestEvents.Add(eventDescription);
                                        citizen.PoorHouseInmate = true;
                                        citizensInPoorHouse++;
                                    }
                                }
                                if (latestEvents.Count > 10)
                                {
                                    latestEvents.RemoveAt(0);
                                }
                            }
                        }
                    }

                    Console.SetCursorPosition(103 + (prisonWidth + 5), 1);
                    Console.WriteLine("Rån: " + numOfRobberies);
                    Console.SetCursorPosition(103 + (prisonWidth + 5), 2);
                    Console.WriteLine($"Fängelset: {thivesInPrison}");
                    Console.SetCursorPosition(103 + (prisonWidth + 5), 3);
                    Console.WriteLine($"Fattighuset: {citizensInPoorHouse}");
                     Console.SetCursorPosition(103 + (prisonWidth + 5), 4);
                    Console.WriteLine($"Medborgare: {citizenNum}");
                    Console.SetCursorPosition(103 + (prisonWidth + 5), 5);
                    Console.WriteLine($"Tjuvar: {thiefNum}");
                    Console.SetCursorPosition(103 + (prisonWidth + 5), 6);
                    Console.WriteLine($"Poliser: {policeNum}");

                    // Skriv ut de senaste händelserna Klar
                    Console.SetCursorPosition(width - 15, height + 2);
                    Console.WriteLine("Senaste händelser:");
                    int eventCount = 1;
                    foreach (var ev in latestEvents)
                    {
                        Console.SetCursorPosition(width - 15, height + 2 + eventCount);
                        Console.WriteLine($"{eventCount}. {ev}");
                        eventCount++;
                    }
                    //___Inventory_______
                    Console.SetCursorPosition(0, height + 2);
                    Console.WriteLine("Inventory: ");
                    Console.SetCursorPosition(0, height + 3);
                    for (int i = inventoryRollList; i < inventoryRollList + 10; i++)// 10 är rulllistans längd.
                    {
                        Console.Write($"{personList[i].Namn}: ");
                        int inventoryCount = 0;
                        foreach (string item in personList[i].Inventory)
                        {
                            Console.Write(" " + item);
                            inventoryCount++;
                        }
                        Console.WriteLine();
                    }
                    inventoryRollList++;
                    if (inventoryRollList > totalPeople - 10) { inventoryRollList = totalPeople - (inventoryRollList + 1); }// 10 är rulllistans längd.
                    else if (inventoryRollList >= totalPeople) { inventoryRollList = 0; }
                   
                    Thread.Sleep(200);
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