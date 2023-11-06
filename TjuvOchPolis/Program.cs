using System;
using System.ComponentModel.Design;
using System.Drawing;
using TjuvOchPolis;
using static TjuvOchPolis.Draw;

namespace Tjuv_Polis_MinUtveckling26Okt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> personsList = new List<Person>();
            List<string> latestEvents = new List<string>();
            //Mått för staden, fägelsett och fattighuset.
            int cityWidth = 100;
            int cityHeight = 25;
            int prisonPosX = cityWidth + 3;
            int prisonPosY = 1;
            int prisonWidth = 15;
            int prisonHeight = 10;
            int poorHousePosX = cityWidth + 3;
            int poorHousePosY = 15;
            int poorHouseWidth = 40; 
            int poorHouseHeight = 10;

            int policeNum = 10;
            int citizenNum = 30;
            int thiefNum = 20;
            int numOfRobberies = 0;
            int thivesInPrison = 0;
            int citizensInPoorHouse = 0;
            int inventoryRollDelay = 0;
            int inventoryStepCount = 0;
            bool autoScroll = true;
            bool eventSleep = false;
            Random random = new Random();
            Console.CursorVisible = false;
            //List<Police> policeList = PoliceFactory.CreatePolice(PoliceNum, width, height, random);
            //List<Citizen> citizenList = CitizenFactory.CreateCitizens(CitizenNum, width, height, random);
            //List<Thief> thiefList = ThiefFactory.CreateThieves(thiefNum, width, height, random);
            //Gör personer.
            string[] policeName = new string[]
            {
                "Polisen Svensson", "Polisen Johnsson", "Polisen Davidsson", "Polisen Willhelmsson", "Polisen Andersson", "Polisen Martinsson", "Polisen Börjesson", "Polisen Göransson", "Polisen Carlberg", "Polisen Nilsson",
                "Polisen Falk", "Polisen Johnson", "Polisen Eriksen", "Polisen Olofsson", "Polisen Lindberg", "Polisen Henriksson", "Polisen Andersson", "Polisen Mårtensson", "Polisen Bergqvist", "Polisen Magnusson", "Polisen Larsson",
                "Polisen Persson", "Polisen Eriksson", "Polisen Karlsson", "Polisen Johansson", "Polisen Bergström", "Polisen Gustafsson", "Polisen Lundqvist", "Polisen Nyström", "Polisen Holm", "Polisen Ahlström",
                "Polisen Larsson", "Polisen Sjöberg", "Polisen Andersson", "Polisen Gustavsson", "Polisen Wallin", "Polisen Karlberg", "Polisen Bergman", "Polisen Lindström", "Polisen Persson", "Polisen Sandberg"
            };
            for (int i = 0; i < policeNum; i++)
            {
                var police = new Police(policeName[i % policeName.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'P', random.Next(8));
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
                var citizen = new Citizen(citizenName[i % citizenName.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'C', random.Next(8), false);
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
                var thief = new Thief(thiefName[i % citizenName.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'T', false, random.Next(8));
                personsList.Add(thief);
            }
            while (true)
            {
                int totalPeople = policeNum + citizenNum + thiefNum;
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
                            Citizen citizen = new Citizen(citizenName[random.Next(citizenName.Length) % citizenName.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'C', random.Next(8), false);
                            citizen.Inventory.Add("Nycklar");
                            citizen.Inventory.Add("Mobiltelefon");
                            citizen.Inventory.Add("Plånbok");
                            citizen.Inventory.Add("Klocka");
                            personsList.Add(citizen);
                            break;
                        case 'T':
                            thiefNum++;
                            Thief thief = new Thief(thiefName[random.Next(thiefName.Length) % thiefName.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'T', false, random.Next(8));
                            personsList.Add(thief);
                            break;
                        case 'P':
                            policeNum++;
                            Police police = new Police(policeName[random.Next(policeName.Length) % policeName.Length], random.Next(1, cityWidth - 1), random.Next(1, cityHeight - 1), 'P', random.Next(8));
                            personsList.Add(police);
                            break;
                        case 'A':
                            if (autoScroll == true) { autoScroll = false; }
                            else if (autoScroll == false) { autoScroll = true; }
                            break;
                        case 'W'://Scrorlla Inventory uppåt.
                            if (autoScroll == true) { autoScroll = false; }
                            if (inventoryStepCount > 0) { inventoryStepCount--; inventoryStepCount--; }
                            break;
                        case 'S'://Scrorlla Inventory nedåt.
                            if (autoScroll == true) { autoScroll = false; }
                            if (inventoryStepCount == totalPeople - 2) {  inventoryStepCount++; }
                            else if (inventoryStepCount < totalPeople - 5) { inventoryStepCount++; inventoryStepCount++; }
                            break;
                    }
                }
                //Ritar upp allt.
                Console.Clear();
                Draw.DrawCity(cityWidth, cityHeight);
                Draw.DrawPrison(prisonPosX, prisonPosY, prisonWidth, prisonHeight);
                Draw.DrawPoorHouse(poorHousePosX, poorHousePosY, poorHouseWidth, poorHouseHeight);
                Draw.DrawStatistics(prisonWidth, numOfRobberies, thivesInPrison, citizensInPoorHouse, citizenNum, thiefNum, policeNum);
                for (int i = 0; i < totalPeople; i++)
                {
                    int direction = personsList[i].Direction;
                    Person person = personsList[i];
                    int originalForegroundColor = (int)Console.ForegroundColor;
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
                    //Updaterar positioner till alla personer i staden, fängelset och fattighuset.
                    //Så att personerna kommer ut i andra änden.
                    if (personsList[i].PoorHouseInmate == true)
                    {
                        UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                        if (y_Positions[i] > poorHousePosY + (poorHouseHeight - 1)) { y_Positions[i] = poorHousePosY + 1; }
                        else if (y_Positions[i] < poorHousePosY + 1) { y_Positions[i] = poorHousePosY + (poorHouseHeight - 1); }
                        if (x_Positions[i] > poorHousePosX + (poorHouseWidth - 1)) { x_Positions[i] = poorHousePosX + 1; }
                        else if (x_Positions[i] < poorHousePosX + 1) { x_Positions[i] = poorHousePosX + (poorHouseWidth - 1); }
                    }
                    else if (personsList[i].PrisonInmate == true)
                    {
                        UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                        if (y_Positions[i] > prisonPosY + (prisonHeight - 1)) { y_Positions[i] = prisonPosY + 1; }
                        else if (y_Positions[i] < prisonPosY + 1) { y_Positions[i] = prisonPosY + (prisonHeight - 1); }
                        if (x_Positions[i] > prisonPosX + (prisonWidth - 1)) { x_Positions[i] = prisonPosX + 1; }
                        else if (x_Positions[i] < prisonPosX + 1) { x_Positions[i] = prisonPosX + (prisonWidth - 1); }
                    }
                    else
                    {
                        UpdatePosition(ref x_Positions[i], ref y_Positions[i], direction);
                        if (y_Positions[i] > cityHeight) { y_Positions[i] = 1; }
                        else if (y_Positions[i] < 1) { y_Positions[i] = cityHeight; }
                        if (x_Positions[i] > cityWidth) { x_Positions[i] = 1; }
                        else if (x_Positions[i] < 1) { x_Positions[i] = cityWidth; }
                    }
                    Console.SetCursorPosition(x_Positions[i], y_Positions[i]);
                    Console.Write(personsList[i].Symbol);
                    Console.ForegroundColor = (ConsoleColor)originalForegroundColor;
                }
                // Kontrollera om flera gubbar befinner sig på samma position.
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
                                police.CatchThief(meet_1, meet_2, personsList, latestEvents, y_Positions, x_Positions, ref thivesInPrison, j, i);
                                eventSleep = true;
                            }
                            else if (meet_1 is Citizen && meet_2 is Thief)
                            {
                                Thief thief = (Thief)meet_2;
                                thief.Steal(meet_1, meet_2, latestEvents, ref numOfRobberies);
                                eventSleep = true;
                            }
                            else if (meet_1 is Police && meet_2 is Citizen)
                            {
                                Police police = (Police)meet_1;
                                police.AdmitToPoorHouse(meet_1, meet_2, latestEvents, ref citizensInPoorHouse);
                                if (meet_2.Inventory.Count == 0)
                                {
                                    eventSleep = true;
                                }
                            }
                        }
                    }
                }
                //Skriv ut de senaste händelserna, och vänder på listan.
                Console.SetCursorPosition(cityWidth - 15, cityHeight + 2);
                Console.WriteLine("Senaste händelser:");
                if (latestEvents.Count > 12)
                {
                    latestEvents.RemoveAt(0);
                }
                for (int i = 0; i < 12; i++) 
                {
                    int index = latestEvents.Count - 1 - i;
                    if (index >= 0)
                    {
                        Console.SetCursorPosition(cityWidth - 15, cityHeight + 3 + i);
                        Console.WriteLine($"{latestEvents[index]}");
                        
                    }
                }
                //Inventory listan.
                Console.SetCursorPosition(0, cityHeight + 2);
                Console.WriteLine("upp[W] ned[S] auto[A]  INVENTORY: ");
                Console.SetCursorPosition(0, cityHeight + 3);
                int rollListLength = 12;
                int inventoryCount = inventoryStepCount;
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
                if (autoScroll == true) 
                {
                    if (inventoryRollDelay < 4) { inventoryRollDelay++; }
                    if (inventoryRollDelay == 3)
                    {
                        if (inventoryCount < totalPeople) inventoryStepCount++; inventoryRollDelay = 0;
                    }
                }
                //Hastigheten ifall något händer
                //if (eventSleep == true)
                //{
                //    Thread.Sleep(1500);
                //    eventSleep = false;
                //}
                //else
                //{
                //    Thread.Sleep(400);
                //}
                Thread.Sleep(400);
                for (int i = 0; i < totalPeople; i++)//Sparar dom nya positionerna i Arrayerna.
                {
                      personsList[i].X_coord = x_Positions[i];
                      personsList[i].Y_coord = y_Positions[i];
                }
            }
        }
        static void UpdatePosition(ref int x, ref int y, int direction)
        {
            switch (direction)
            {
                case 0:
                    y--;
                    break;
                case 1:
                    y++;
                    break;
                case 2:
                    x++;
                    break;
                case 3:
                    x--;
                    break;
                case 4:
                    y--; x++;
                    break;
                case 5:
                    y--; x--;
                    break;
                case 6:
                    y++; x++;
                    break;
                case 7:
                    y++; x--;
                    break;
            }
        }
    }
}