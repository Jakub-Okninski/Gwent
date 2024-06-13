using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.TypyKart
{
    public abstract class CardSpecial : Card
    {
        public CardSpecial(string name) : base(name)
        {
        } 
        public override void PutCard(PlayerBoard playerBoard)
        {
            playerBoard.PlayerCardsInGame.Remove(this);
            playerBoard.CardsSpecial.Add(this);
        }
    }
}