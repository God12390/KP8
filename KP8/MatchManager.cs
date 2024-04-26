using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KP8
{
    internal class MatchManager : Person
    {
        public List<Match> matches {  get; set; } = new List<Match>();
        public MatchManager(string firstName, string lastName, DateTime dateOfBirth) : base(firstName, lastName, dateOfBirth)
        {
            
        }

        public void addMatch(Match match)
        {
            matches.Add(match);
        }
        public void removeMatch(int matchIndex) 
        {
            matches.RemoveAt(matchIndex);
        }
        public void showAllMatches()
        {
            for (int i = 0; i < matches.Count; i++)
            {
                showMatch(i);
            }
        }
        public void showMatch(int matchIndex)
        {
            Console.WriteLine($"Match: {matches[matchIndex].FirstTeam.Name}[players: {matches[matchIndex].FirstTeam.Players.Count}] vs {matches[matchIndex].SecondTeam.Name}[players: {matches[matchIndex].SecondTeam.Players.Count}] | Date: {matches[matchIndex].Date} | Status: {matches[matchIndex].Status}");
        }
        public void SearchMatch(DateTime date, string opponentTeam)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Date == date && matches[i].SecondTeam.Name == opponentTeam)
                    Console.WriteLine($"{matches[i].FirstTeam.Name} vs {matches[i].SecondTeam.Name} on {matches[i].Date.ToShortDateString()}");
            }
        }
    }
}
