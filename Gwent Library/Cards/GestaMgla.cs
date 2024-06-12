using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class GestaMgla : CardWeather, ICloneable
    {   
        public GestaMgla(string name) : base(name)
        {
            Description = "Siła wszystkich jednostek strzeleckich przyjmuje wartość 1.";
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
        if(player1.playerBoard.CardsSpecial.Any(card => card is GestaMgla) ||
            player2.playerBoard.CardsSpecial.Any(card => card is GestaMgla))
            {
                setForceWeather(player1.playerBoard.CardsStrzeleckiePlayer, 1);
                setForceWeather(player2.playerBoard.CardsStrzeleckiePlayer, 1);
            }
        }
    }
}
