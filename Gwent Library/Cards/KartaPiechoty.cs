using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaPiechoty : CardWarrior
    {
        public override void PutCard(PlayerBoard playerBoard)
        {
            playerBoard.PlayerCardsInGame.Remove(this);
            playerBoard.CardsPiechotyPlayer.Add(this);
            Effect?.Invoke(this, playerBoard);
        }
        public KartaPiechoty(string name, int force, bool isHero) : base(name, force, isHero)
        {
            Description = "Jednostka pola piechoty.";
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
