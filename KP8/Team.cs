using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP8
{
    internal class Team
    {
        public string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Team(string name)
        {
            Name = name;
        }

        public void addPlayer(Player player)
        {
            player.Salary = 1000;
            player.Status = true;
            Players.Add(player);
        }
        public void removePlayer(int index) 
        {
            Players[index].Salary = 0;
            Players[index].Status = false;
            Players.RemoveAt(index);
        }
    }
}
