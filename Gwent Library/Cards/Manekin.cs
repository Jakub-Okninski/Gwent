using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.Karty
{
    public class Manekin : CardAllField, ICloneable
    {
        public Manekin(string name, LocationCard location) : base(name, location)
        {
        }
        public void ManekinAction<T> (Player player, T oldCard) where T : CardWarrior
        {
            if (player.playerBoard.CardsPiechotyPlayer.Any(card =>  card is KartaPiechoty kp && kp == oldCard)&& this.LocationCard == LocationCard.Piechoty)
            {
                player.playerBoard.CardsPiechotyPlayer.RemoveAll(card=> card== oldCard);
                player.playerBoard.PlayerCardsInGame.Add(oldCard);
            }
            else if(player.playerBoard.CardsStrzeleckiePlayer.Any(card => card is KartaLucznika kl && kl == oldCard) && this.LocationCard == LocationCard.Lucznika)
            {
                player.playerBoard.CardsStrzeleckiePlayer.RemoveAll(card => card == oldCard);
                player.playerBoard.PlayerCardsInGame.Add(oldCard);

            }
            else if (player.playerBoard.CardsOblezniczePlayer.Any(card => card is KartaObleznika ko && ko == oldCard) && this.LocationCard == LocationCard.Obleznika)
            {
                player.playerBoard.CardsOblezniczePlayer.RemoveAll(card => card == oldCard);
                player.playerBoard.PlayerCardsInGame.Add(oldCard);

            }
        }
    }
}
