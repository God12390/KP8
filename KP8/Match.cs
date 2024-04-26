using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP8
{
    internal class Match
    {


        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public DateTime Date {  get; set; }
        public Stadium Place {  get; set; }
        public Result Status { get; set; }
        public int NumberOfViewers { get; set; }

        public Match(Team firstTeam, Team secondTeam, DateTime date, Stadium placeOfMatch, Result status, int numberOfViewers)
        {
            FirstTeam = firstTeam;
            SecondTeam = secondTeam;
            Date = date;
            Place = placeOfMatch;
            Status = status;
            NumberOfViewers = numberOfViewers;
        }
    }
}
