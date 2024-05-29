using Gwent_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_App
{
    public class Player
    {
        public Player(string name, int winnings, List<Karta> cards)
        {
            Name = name;
            Winnings = winnings;
            Cards = cards;
        }

        public string Name { get; set; }    
        public int Winnings {  get; set; }
        public List<Karta> Cards { get; set; }  
    }
}
