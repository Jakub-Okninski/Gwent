using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class RogDowodcy : CardAllField, ICloneable
    {
        public RogDowodcy(string name, LocationCard locationCard) : base(name, locationCard)
        {
        }
        public override void PutCard(PlayerBoard playerBoard)
        {
            if (!(playerBoard.CardsSpecial.Any(card => (card is RogDowodcy rg && rg.LocationCard == this.LocationCard))))
            {
                playerBoard.PlayerCardsInGame.Remove(this);
                playerBoard.CardsSpecial.Add(this);
            }
            else {
                throw new RogException("Nie możesz położyć ponownie rogu na to samo pole");  
            }
         
        }
        public void ActionRog(Player player, LocationCard locationCard)
        {
            if (locationCard  == LocationCard.Piechoty && player.playerBoard.CardsSpecial.Any(card => (card is RogDowodcy rg && rg.LocationCard == LocationCard.Piechoty)))
            {
                player.playerBoard.CardsPiechotyPlayer.ForEach(card => card.Force = (card.Force * 2));

            }
            else if (locationCard == LocationCard.Lucznika && player.playerBoard.CardsSpecial.Any(card =>( card is RogDowodcy rg && rg.LocationCard == LocationCard.Lucznika)))
            {
                player.playerBoard.CardsStrzeleckiePlayer.ForEach(card => card.Force = (card.Force * 2));
            }

            else if(locationCard == LocationCard.Obleznika && player.playerBoard.CardsSpecial.Any(card => (card is RogDowodcy rg && rg.LocationCard == LocationCard.Obleznika)))
            {
                player.playerBoard.CardsOblezniczePlayer.ForEach(card => card.Force = (card.Force * 2));

            }
        }
        public override string ToString()
        {
            return base.ToString()+ " Location Card " + LocationCard;
        }
    }    
}
