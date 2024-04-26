using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KP8
{
    internal class PlayersAgent : Person
    {
        public List<Player> Players {  get; set; } = new List<Player>();
        public List<Team> Teams { get; set; } = new List<Team>();

        public PlayersAgent(string firstName, string lastName, DateTime dateOfBirth) : base(firstName, lastName, dateOfBirth) { }

        public void addPlayer(Player player)
        {
            Players.Add(player);
        }
        public void removePlayer(int indexToRemove) 
        { 
            Players.RemoveAt(indexToRemove);
        }
        public void showAllPlayersInfo()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Console.WriteLine($"Name: {Players[i].FirstName} | Surname: {Players[i].LastName}");
            }
        }
        public void showPlayer(int playerIndex)
        {
            Console.WriteLine(Players[playerIndex].ToString());
        }
        public void addTeam(string name)
        {
            Teams.Add(new Team(name));
        }
        public void removeTeam(string name)
        { 
            Teams.RemoveAll(team =>  team.Name == name);
        }
        public void addPlayerToTeam(int index, string teamName)
        {
            Team team = Teams.FirstOrDefault(t => t.Name == teamName);

            if (team != null)
            {
                Player player = Players[index];
                team.Players.Add(player);
                Players[index].Status = true;
                Console.WriteLine($"Player with index {index} added to team {teamName}.");
            }
            else
            {
                Console.WriteLine($"Team with name {teamName} not found.");
            }
        }
        public void RemovePlayerFromTeam(int index, string teamName)
        {
            Team team = Teams.FirstOrDefault(t => t.Name == teamName);

            if (team != null)
            {
                Player player = Players[index];
                bool removed = team.Players.Remove(player);
                if (removed)
                {
                    Players[index].Status = false;
                    Console.WriteLine($"Player with index {index} removed from team {teamName}.");
                }
                else
                {
                    Console.WriteLine($"Player with index {index} is not in team {teamName}.");
                }
            }
            else
            {
                Console.WriteLine($"Team with name {teamName} not found.");
            }
        }
        public void showAllTeams()
        {
            for (int i = 0; i < Teams.Count; i++)
            {
                Console.WriteLine($"Name: {Teams[i].Name} | Amount of players: {Teams[i].Players.Count}");
            }
        }
        public void SearchPlayer(string nameOrSurname)
        {
            Console.WriteLine("Found Players:");
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].FirstName == nameOrSurname || Players[i].LastName == nameOrSurname)
                    Console.WriteLine($"{Players[i].FirstName} {Players[i].LastName}");
            }
        }
    }
}
