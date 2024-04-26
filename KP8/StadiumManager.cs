using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KP8
{
    internal class StadiumManager : Person
    {
        public List<Stadium> Stadiums { get; set; } = new List<Stadium>();

        public StadiumManager(string firstName, string lastName, DateTime dateOfBirth) : base(firstName, lastName, dateOfBirth) { }
        public void addStadium(Stadium stadium)
        {
            Stadiums.Add(stadium);
        }
        public void removeStadium(string stadiumName)
        {
            Stadiums.RemoveAll(stadium => stadium.Name == stadiumName);
        }
        public Stadium findStadiumByName(string name)
        {
            return Stadiums.FirstOrDefault(stadium => stadium.Name == name);
        }
        public void ShowStadiumInfo(int stadiumIndex)
        {
            Console.WriteLine($"Name: {Stadiums[stadiumIndex].Name} Number of seats: {Stadiums[stadiumIndex].NumberOfSeats} | Price per seat: {Stadiums[stadiumIndex].PricePerSeat}");
            for (int i = 0; i < Stadiums[stadiumIndex].matches.Count; i++)
            {
                Console.WriteLine($"Match: {Stadiums[stadiumIndex].matches[i].FirstTeam.Name} vs {Stadiums[stadiumIndex].matches[i].SecondTeam.Name} | Status:{Stadiums[stadiumIndex].matches[i].Status}");
                Console.WriteLine($"Date: {Stadiums[stadiumIndex].matches[i].Date}");
            }
        }
    }
}
