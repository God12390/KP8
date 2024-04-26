using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP8
{
    internal class Stadium
    {
        public List<Match> matches {  get; set; } = new List<Match>();
        public string Name {  get; set; }
        public int NumberOfSeats {  get; set; }
        public decimal PricePerSeat { get; set; }
        public Stadium(string name, int numberOfSeats, decimal pricePerSeat) 
        {
            Name = name;
            NumberOfSeats = numberOfSeats;
            PricePerSeat = pricePerSeat;
        }
    }
}
