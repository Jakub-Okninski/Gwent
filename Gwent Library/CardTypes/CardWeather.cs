using Gwent_Library.Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.TypyKart
{
    public abstract class CardWeather : CardGlobal, ICloneable
    {
        public CardWeather(string name) : base(name)
        {
        }
        protected void setForceWeather<T>(Deck<T> deck, int force) where T : CardWarrior 
        {
            deck.Where(card => !card.IsHero)
               .ToList()
               .ForEach(card => card.Force = force);         
        }

        protected void setForceWeather<T>(Deck<T> deck) where T : CardWarrior
        {
            deck.Where(card => !card.IsHero)
               .ToList()
               .ForEach(card => card.Force = card.ForceTemporary);
        }

     

    }
}
