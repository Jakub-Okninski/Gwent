using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class CzysteNiebo : CardWeather, ICloneable
    {
        public CzysteNiebo(string name) : base(name)
        {
            Description = "Odrzuca wszystkie karty Trzaskającego mrozu, Ulewnego deszczu i Gęstej mgły znajdujące się na polu bitwy.";
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
            if (player1.playerBoard.CardsSpecial.Any(card => card is CzysteNiebo) 
                || player2.playerBoard.CardsSpecial.Any(card => card is CzysteNiebo))
            {
                setForceWeather(player1.playerBoard.CardsPiechotyPlayer);
                setForceWeather(player2.playerBoard.CardsPiechotyPlayer);
                setForceWeather(player1.playerBoard.CardsStrzeleckiePlayer);
                setForceWeather(player2.playerBoard.CardsStrzeleckiePlayer);
                setForceWeather(player1.playerBoard.CardsOblezniczePlayer);
                setForceWeather(player2.playerBoard.CardsOblezniczePlayer);

                player1.playerBoard.CardsSpecial.RemoveAll(card => card is CardWeather);
                player2.playerBoard.CardsSpecial.RemoveAll(card => card is CardWeather);
            }
        }
    }
}
