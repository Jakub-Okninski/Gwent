using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaObleznika : CardWarrior
    {
        public override void PutCard(PlayerBoard playerBoard)
        {
            playerBoard.PlayerCardsInGame.Remove(this);
            playerBoard.CardsOblezniczePlayer.Add(this);
            Effect?.Invoke(this, playerBoard);
        }
        public KartaObleznika(string nazwa, int force, bool isHero) : base(nazwa, force, isHero)
        {
            Description = "Jednostka pola oblężniczego.";
        }
    }
}
