using KP8;
using System;
using System.Reflection;
using System.Threading.Channels;

public enum Result
{
    NotYetPlayed,
    FirstTeamWon,
    SecondTeamWon,
    Draw
}
class Program
{
    static void Main(string[] args)
    {

        PlayersAgent agent = new PlayersAgent("Name","Surname", new DateTime(2000, 10, 10));
        MatchManager matchManager = new MatchManager("Name", "Surname", new DateTime(2000, 10, 10));
        StadiumManager stadiumManager = new StadiumManager("Name", "Surname", new DateTime(2000, 10, 10));
        displayMainMenu();
        int choice = ValidateIntegerInput(Console.ReadLine(), 1, 7);
        while (choice != 6)
        {
            switch (choice)
            {
                case 1:
                    PlayersManagement(agent);
                    Console.Clear();
                    break;
                case 2:
                    TeamsManagement(agent);
                    Console.Clear();
                    break;
                case 3:
                    MatchesManagement(agent, matchManager, stadiumManager);
                    Console.Clear();
                    break;
                case 4:
                    StadiumsManagement(stadiumManager);
                    Console.Clear();
                    break;
                case 5:
                    Search(agent, matchManager, stadiumManager);
                    Console.Clear();
                    break;
            }
            displayMainMenu();
            choice = ValidateIntegerInput(Console.ReadLine(), 1, 7);
        }
    }
    static void PlayersManagement(PlayersAgent agent)
    {
        showPlayerManagementMenu();
        string option = Console.ReadLine();
        while (option != "6")
        {
            switch (option)
            {
                case "1":
                    Console.Write("name: ");
                    string name = Console.ReadLine();
                    Console.Write("surname: ");
                    string surname = Console.ReadLine();
                    Console.Write("Date of birth(YYYY-MM-DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth)) { }
                    else { Console.WriteLine("Invalid date format. Please enter a valid date in YYYY-MM-DD format."); }
                    Console.Write("Health status[healthy - true | unhealthy - false]: ");
                    bool healthStatus = true;
                    try { healthStatus = Convert.ToBoolean(Console.ReadLine()); }
                    catch (FormatException ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    agent.addPlayer(new Player(name, surname, dateOfBirth, healthStatus));
                    break;
                case "2":
                    Console.Write($"Enter player index to remove[0 - {agent.Players.Count - 1}]: ");
                    int playerIndexToRemove = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
                    agent.removePlayer(playerIndexToRemove);
                    break;
                case "3":
                    EditPlayer(agent);
                    break;
                case "4":
                    Console.WriteLine($"Enter player index to check info[0 - {agent.Players.Count - 1}]");
                    int playerIndexToCheck = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
                    agent.showPlayer(playerIndexToCheck);
                    break;
                case "5":
                    agent.showAllPlayersInfo();
                    break;
                default:
                    break;
            }
            showPlayerManagementMenu();
            option = Console.ReadLine();
        }

    }
    static void TeamsManagement(PlayersAgent agent)
    {
        displayTeamManagementMenu();
        string option = Console.ReadLine();
        while (option != "6")
        {
            switch (option)
            {
                case "1":
                    Console.Write("Name: ");
                    string nameOfNewTeam = Console.ReadLine();
                    agent.addTeam(nameOfNewTeam);
                    break;
                case "2":
                    Console.Write("Name: ");
                    string nameOfNewTeamToRemove = Console.ReadLine();
                    agent.removeTeam(nameOfNewTeamToRemove);
                    break;
                case "3":
                    Console.Write($"Player index[0-{agent.Players.Count-1}]: ");
                    int playerIndex = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
                    Console.Write("Name of team: ");
                    string nameOfTeam = Console.ReadLine();
                    agent.Players[playerIndex].Salary = 1000m;
                    agent.addPlayerToTeam(playerIndex, nameOfTeam);
                    break;
                case "4":
                    Console.Write($"Player index[0-{agent.Players.Count - 1}]: ");
                    int playerIndexToRemove = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
                    Console.Write("Name of team: ");
                    string nameOfTeamToRemove = Console.ReadLine();
                    agent.Players[playerIndexToRemove].Salary = 0;
                    agent.RemovePlayerFromTeam(playerIndexToRemove, nameOfTeamToRemove);
                    break;
                case "5":
                    agent.showAllTeams();
                    Console.WriteLine("Enter anyting to continue");
                    break;

            }
            displayTeamManagementMenu();
            option = Console.ReadLine();
        }

    }
    static void MatchesManagement(PlayersAgent agent, MatchManager matchManager, StadiumManager stadiumManager)
    {
        if (stadiumManager.Stadiums.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Firstly you need to create a stadium.");
            Console.ResetColor();
            return;
        }
        displayMatchManagementMenu();
        string option = Console.ReadLine();
        while (option != "8")
        {

            switch (option)
            {
                case "1":
                    AddMatch(agent, matchManager, stadiumManager);
                    break;
                case "2":
                    RemoveMatch(matchManager);
                    break;
                case "3":
                    EditMatch(agent, matchManager, stadiumManager);
                    break;
                case "4":
                    ViewMatch(matchManager);
                    break;
                case "5":
                    ViewAllMatches(matchManager);
                    break;
                case "6":
                    matchManager.matches.Sort((match1, match2) => match1.Date.CompareTo(match2.Date));
                    break;
                case "7":
                    Dictionary<Result, int> sortOrder = new Dictionary<Result, int>
                    {
                        { Result.FirstTeamWon, 1 },
                        { Result.SecondTeamWon, 2 },
                        { Result.Draw, 3 },
                        { Result.NotYetPlayed, 4 }
                    };
                    matchManager.matches.Sort((match1, match2) => sortOrder[match1.Status].CompareTo(sortOrder[match2.Status]));
                    break;
            }
            displayMatchManagementMenu();
            option = Console.ReadLine();
        }
    }
    public static void StadiumsManagement(StadiumManager stadiumManager)
    {
        displayStadiumManagementMenu();
        int choice = Convert.ToInt32(Console.ReadLine());
        while (choice != 5)
        {
            switch (choice)
            {
                case 1:
                    Console.Write("Stadium Name: ");
                    string stadiumName = Console.ReadLine();
                    Console.Write("Number of seats: ");
                    int numOfSeats = ValidateIntegerInput(Console.ReadLine(), 1, 1000);
                    Console.Write("Price Per Seat: ");
                    int pricePerSeat = ValidateIntegerInput(Console.ReadLine(), 1, 1000);
                    stadiumManager.addStadium(new Stadium(stadiumName, numOfSeats, pricePerSeat));
                    break;
                case 2:
                    Console.Write("Stadium Name To Remove: ");
                    string nameToRemove = Console.ReadLine();
                    stadiumManager.removeStadium(nameToRemove);
                    break;
                case 3:
                    Console.WriteLine("Choose an option to edit:\n1) Number Of Seats\n2) Price Per Seat");
                    string option = Console.ReadLine();
                    Console.Write($"Stadium Index[0 - {stadiumManager.Stadiums.Count - 1}]: ");
                    int stadiumIndex = ValidateIntegerInput(Console.ReadLine(), 0, stadiumManager.Stadiums.Count);
                    if (stadiumIndex == -1) return;
                    switch (option)
                    {
                        case "1":
                            Console.Write("Number Of Seats: ");
                            int newNum = Convert.ToInt32(Console.ReadLine());
                            stadiumManager.Stadiums[stadiumIndex].NumberOfSeats = newNum;
                            break;
                        case "2":
                            Console.Write("Price Per Seat: ");
                            decimal newPrice = Convert.ToInt32(Console.ReadLine());
                            stadiumManager.Stadiums[stadiumIndex].PricePerSeat = newPrice;
                            break;
                    }
                    break;
                case 4:
                    Console.Write($"Stadium Index[0 - {stadiumManager.Stadiums.Count - 1}]: ");
                    int stadiumIndx = Convert.ToInt32(Console.ReadLine());
                    stadiumManager.ShowStadiumInfo(stadiumIndx);
                    break;
                default:
                    break;
            }
            displayStadiumManagementMenu();
            choice = Convert.ToInt32(Console.ReadLine());
        }
    }
    public static void Search(PlayersAgent agent, MatchManager matchManager, StadiumManager stadiumManager)
    {
        displaySearchMenu();
        string choice = Console.ReadLine();
        while (choice != "4")
        {
            switch (choice)
            {
                case "1":
                    SearchPlayer(agent);
                    break;
                case "2":
                    SearchMatch(matchManager);
                    break;
                case "3":
                    SearchStadium(stadiumManager);
                    break;
            }
            displaySearchMenu();
            choice = Console.ReadLine();
        }
    }
    public static void SearchPlayer(PlayersAgent agent)
    {
        Console.Write("Enter player name or surname: ");
        string nameOrSurname = Console.ReadLine();
        agent.SearchPlayer(nameOrSurname);
    }
    public static void SearchMatch(MatchManager matchManager)
    {
        Console.Write("Enter date (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime searchDate))
        {
            Console.Write("Enter opponent team name: ");
            string opponentTeam = Console.ReadLine();
            matchManager.SearchMatch(searchDate, opponentTeam);
        }
        else
            Console.WriteLine("Invalid date format. Please enter a valid date in YYYY-MM-DD format.");
    }
    public static void SearchStadium(StadiumManager stadiumManager)
    {
        Console.Write("Enter stadium name: ");
        string searchStadium = Console.ReadLine();
        Stadium foundStadium = stadiumManager.findStadiumByName(searchStadium);
        if (foundStadium != null)
            Console.WriteLine($"Stadium Found: {foundStadium.Name}\nNumber of Seats: {foundStadium.NumberOfSeats}\nPrice Per Seat: {foundStadium.PricePerSeat}");
        else
            Console.WriteLine("Stadium not found.");
    }
    static void AddMatch(PlayersAgent agent, MatchManager matchManager, StadiumManager stadiumManager)
    {
        Console.WriteLine("Teams:");
        for (int i = 0; i < agent.Teams.Count; i++)
            Console.WriteLine($"Name: {agent.Teams[i].Name} [{i}]");

        Console.Write("First team index: ");
        int index1 = ValidateIntegerInput(Console.ReadLine(), 0, agent.Teams.Count);
        if (index1 == -1){ return; }

        Console.Write("Second team index: ");
        int index2 = ValidateIntegerInput(Console.ReadLine(), 0, agent.Teams.Count);
        if (index2 == -1 || index1 == index2) 
        {
            Console.WriteLine("You need to enter different index");
            return; 
        }

        Console.Write("Date (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date)) { }
        else
            Console.WriteLine("Invalid date format. Please enter a valid date in YYYY-MM-DD format.");

        Console.Write("Stadium name: ");
        string stadiumName = Console.ReadLine();
        Stadium stadium = stadiumManager.findStadiumByName(stadiumName);
        if (stadium == null)
        {
            Console.WriteLine("Stadium not found.");
            return;
        }

        Console.Write("Number of viewers: ");
        int viewers = ValidateIntegerInput(Console.ReadLine(), 0, stadium.NumberOfSeats+1);
        if (viewers == -1)
        {
            Console.WriteLine("Amount Of Seats Not Enough");
            return;
        }  

        Match match = new Match(agent.Teams[index1], agent.Teams[index2], date, stadium, Result.NotYetPlayed, viewers);
        matchManager.addMatch(match);
        stadiumManager.Stadiums[stadiumManager.Stadiums.IndexOf(stadium)].matches.Add(match);
        Console.WriteLine("Match added successfully.");
    }
    static void RemoveMatch(MatchManager matchManager)
    {
        Console.Write($"Enter match index to remove[0-{matchManager.matches.Count - 1}]: ");
        int removeIndex = ValidateIntegerInput(Console.ReadLine(), 0, matchManager.matches.Count);
        if (removeIndex == -1) { return; }
        matchManager.removeMatch(removeIndex);
        Console.WriteLine("Match removed successfully.");
    }
    static void ViewMatch(MatchManager matchManager)
    {
        Console.Write($"Enter match index to view[0-{matchManager.matches.Count - 1}]: ");
        int index = ValidateIntegerInput(Console.ReadLine(), 0, matchManager.matches.Count);
        if (index == -1) return; 
        matchManager.showMatch(index);
    }
    static void ViewAllMatches(MatchManager matchManager)
    {
        matchManager.showAllMatches();
    }
    static void EditPlayer(PlayersAgent agent)
    {
        Console.WriteLine("Choose a field to edit:\n1) Name\n2) Surname\n3) Date of birth\n4) Status\n5) Health status\n6) Salary");
        int editField = Convert.ToInt32(Console.ReadLine());
        switch (editField)
        {
            case 1:
                EditPlayerField("Name", (player) => player.FirstName = Console.ReadLine(), agent);
                break;
            case 2:
                EditPlayerField("Surname", (player) => player.LastName = Console.ReadLine(), agent);
                break;
            case 3:
                EditPlayerField("Date of birth", (player) => player.DateOfBirth = Convert.ToDateTime(Console.ReadLine()), agent);
                break;
            case 4:
                EditPlayerField("Status", (player) => player.Status = Convert.ToBoolean(Console.ReadLine()), agent);
                break;
            case 5:
                EditPlayerField("Health status", (player) => player.HealthStatus = Convert.ToBoolean(Console.ReadLine()), agent);
                break;
            case 6:
                EditPlayerField("Salary", (player) => player.Salary = Convert.ToDecimal(Console.ReadLine()), agent);
                break;
        }
    }
    static void EditPlayerField(string fieldName, Action<Player> editAction, PlayersAgent agent)
    {
        int index = GetPlayerIndex(agent);
        Console.Write("New " + fieldName + ": ");
        if (fieldName == "Salary" && !agent.Players[index].Status)
        {
            Console.WriteLine("Cannot edit Salary when player haven't team.");
            return;
        }
        editAction(agent.Players[index]);
    }
    static int GetPlayerIndex(PlayersAgent agent)
    {
        Console.Write($"Enter player index[0-{agent.Players.Count - 1}]: ");
        int index = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
        if (index == -1)  throw new IndexOutOfRangeException("Invalid index");
        return index;
    }
    static void EditMatch(PlayersAgent agent, MatchManager matchManager, StadiumManager stadiumManager)
    {
        Console.WriteLine("1) Add player\n2) Remove player\n3) Change date\n4) Change stadium\n5) Change viewers amount\n6) Edit result\n7)Exit");
        string option = Console.ReadLine();

        int matchIndex;
        Match match;
        while(option != "7") 
        {
            switch (option)
            {
                case "1":
                    matchIndex = GetMatchIndex(matchManager);
                    match = matchManager.matches[matchIndex];
                   
                    AddPlayerToTeam(agent, match);
                    break;
                case "2":
                    matchIndex = GetMatchIndex(matchManager);
                    match = matchManager.matches[matchIndex];
                    RemovePlayerFromTeam(agent, match);
                    break;
                case "3":
                    matchIndex = GetMatchIndex(matchManager);
                    match = matchManager.matches[matchIndex];
                    Console.Write("Date (YYYY-MM-DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date)) { }
                    else
                        Console.WriteLine("Invalid date format. Please enter a valid date in YYYY-MM-DD format.");
                    matchManager.matches[matchIndex].Date = date;
                    break;
                case "4":
                    matchIndex = GetMatchIndex(matchManager);
                    match = matchManager.matches[matchIndex];
                    Console.WriteLine("Stadium: ");
                    string stadiumName = Console.ReadLine();
                    match.Place = stadiumManager.findStadiumByName(stadiumName);
                    break;
                case "5":
                    matchIndex = GetMatchIndex(matchManager);
                    match = matchManager.matches[matchIndex];
                    Console.WriteLine("Viewers: ");
                    int viewers = Convert.ToInt32(Console.ReadLine());
                    match.NumberOfViewers = viewers;
                    break;
                case "6":
                    matchIndex = GetMatchIndex(matchManager);
                    match = matchManager.matches[matchIndex];
                    EditMatchResult(match);
                    break;
                default:
                    break;
            }
            Console.WriteLine("1) Add player\n2) Remove player\n3) Change date\n4) Change stadium\n5) Change viewers amount\n6) Edit result\n7)Exit");
            option = Console.ReadLine();
        }
    }
    static int GetMatchIndex(MatchManager matchManager)
    {
        Console.Write($"Match index[0-{matchManager.matches.Count-1}]: ");
        int matchIndex = ValidateIntegerInput(Console.ReadLine(), 0, matchManager.matches.Count);
        if (matchIndex == -1) throw new IndexOutOfRangeException("Invalid index");
        return matchIndex;
    }
    static void AddPlayerToTeam(PlayersAgent agent, Match match)
    {
        Console.Write($"Player index[0-{agent.Players.Count - 1}]: ");
        int playerIndex = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
        Console.WriteLine("Add to first team: 1\nAdd to second team: other");
        string option = Console.ReadLine();
        if (option == "1")
            match.FirstTeam.addPlayer(agent.Players[playerIndex]);
        else
            match.SecondTeam.addPlayer(agent.Players[playerIndex]);
    }
    static void RemovePlayerFromTeam(PlayersAgent agent, Match match)
    {
        Console.Write($"Player index[0-{agent.Players.Count-1}]: ");
        int playerIndex = ValidateIntegerInput(Console.ReadLine(), 0, agent.Players.Count);
        if (playerIndex == -1) return;
        Console.WriteLine("Remove from first team: 1\nRemove from second team: anything else");
        string option = Console.ReadLine();
        if (option == "1") {  match.FirstTeam.removePlayer(playerIndex); }
        else if (option == "2") {  match.SecondTeam.removePlayer(playerIndex);  }
    }
    static void EditMatchResult(Match match)
    {
        Console.Write("1) First team won\n2) Second team won\nAnything else: Draw\noption: ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                match.Status = Result.FirstTeamWon;
                break;
            case "2":
                match.Status = Result.SecondTeamWon;
                break;
            default:
                match.Status = Result.Draw;
                break;
        }
    }
    public static int ValidateIntegerInput(string input, int minValue, int maxValue)
    {
        if (!int.TryParse(input, out int value) || value < minValue || value >= maxValue)
        {
            Console.WriteLine("Invalid input.");
            return -1;
        }
        return value; 
    }
    static void displayMainMenu()
    {
        Console.Title = "Football Management System";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to Football Management System");
        Console.ResetColor();
        Console.WriteLine("Choose an option:\n1) Player Management\n2) Team Management\n3) Matches Management\n4) Stadiums Management\n5) Search\n6) Exit");
    }
    static void displaySearchMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nChoose an option:");
        Console.ResetColor();
        Console.WriteLine("1) Search Player\n2) Search Match\n3) Search Stadium\n4) Exit");
    }

    static void displayTeamManagementMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Players Management");
        Console.ResetColor();
        Console.WriteLine("1) Add team\n2) Remove team\n3) Add player to team\n4) Remove player from team\n5) View all teams\n6)Exit");
    }
    public static void displayStadiumManagementMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Stadiums Management");
        Console.ResetColor();
        Console.WriteLine("1) Add Stadium\n2) Remove Stadium\n3) Edit data\n4) Show info\n5) Exit");
    }

    public static void displayMatchManagementMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Matches Management");
        Console.ResetColor();
        Console.WriteLine("1) Add match\n2) Remove match\n3) Edit match data\n4) View info about match\n5) View all matches\n6) Sort matches by date\n7) Sort matches by status\n8) Exit");
    }
    static void showPlayerManagementMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Players Management");
        Console.ResetColor();
        Console.WriteLine("1) Add player\n2) Remove player\n3) Edit player\n4) View player\n5) View all players\n6) Exit");
    }
}

