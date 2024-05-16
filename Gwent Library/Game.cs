using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class Game
    {
        public Player player1 {  get; set; }
        public Player player2 { get; set; }
        public Game (Player player1, Player player2)
        {
            this.player1 = player1; 
            this.player2 = player2;
        }   
    }
}
