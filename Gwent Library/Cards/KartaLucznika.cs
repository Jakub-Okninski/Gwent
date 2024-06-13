using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaLucznika : CardWarrior
    {
        public override void PutCard(PlayerBoard playerBoard)
        {
            playerBoard.PlayerCardsInGame.Remove(this);
            playerBoard.CardsStrzeleckiePlayer.Add(this);
            Effect?.Invoke(this, playerBoard);
        }
        public KartaLucznika(string name, int force, bool isHero) : base(name, force, isHero)
        {
            Description = "Jednostka pola strzeleckiego.";
        }
    }
}
