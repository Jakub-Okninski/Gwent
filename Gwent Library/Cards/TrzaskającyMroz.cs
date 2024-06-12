using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class TrzaskającyMroz : CardWeather, ICloneable
    {
        public TrzaskającyMroz (string name) : base(name)
        {
            Description = "Siła wszystkich jednostek walczących w zwarciu przyjmuje wartość 1.";
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
            if (player1.playerBoard.CardsSpecial.Any(card => card is TrzaskającyMroz) ||player2.playerBoard.CardsSpecial.Any(card => card is TrzaskającyMroz))
            {
                setForceWeather(player1.playerBoard.CardsPiechotyPlayer, 1);
                setForceWeather(player2.playerBoard.CardsPiechotyPlayer, 1);             
            }
        }
    }
}
