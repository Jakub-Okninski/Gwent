using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestDowódcaPółnocy : CardCommander, ICloneable
    {
        public FoltestDowódcaPółnocy(string name) : base(name, "Usuwa wszelkie efekty pogodowe.")
        {
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
            player1.playerBoard.CardsSpecial.RemoveAll(card => card is CardWeather);
            player2.playerBoard.CardsSpecial.RemoveAll(card => card is CardWeather);
            player1.playerBoard.CardsSpecial.Add(new CzysteNiebo("Czyste Niebo Dowódcy Północy"));
            player1.playerBoard.CardsSpecial.Remove(this);
        }  
    }
}
