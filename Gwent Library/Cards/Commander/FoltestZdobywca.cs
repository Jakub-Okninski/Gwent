using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestZdobywca : CardCommander
    {
        public FoltestZdobywca(string name) : base(name, "Podwaja siłę wszystkich swoich jednostek oblężniczych (chyba, że został użyty już róg dowódcy).")
        {
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
           if(!(player1.playerBoard.CardsSpecial.Any(card=>(card is RogDowodcy Rg && Rg.LocationCard == LocationCard.Obleznika))))
            {
               RogDowodcy rog =  new RogDowodcy("Róg Dowódcy Foltesta Zdobywcy", LocationCard.Obleznika);
               rog.PutCard(player1.playerBoard);
            }
            else
            {
                throw new ExceptionDowodca("Został użyty już róg dowódcy.");
            }
            player1.playerBoard.CardsSpecial.Remove(this);
        }
    }
}
