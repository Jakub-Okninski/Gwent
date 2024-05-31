using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class CardAllField : CardSpecial, ICloneable
    {
        public LocationCard LocationCard;
        public CardAllField(string name, LocationCard locationCard) : base(name)
        {
            LocationCard = locationCard;
        }
        public override string ToString()
        {
            return base.ToString() + $" LocationCard: {LocationCard}, ";
        }
    }
}
    public enum LocationCard
    {
        Piechoty,
        Lucznika,
        Obleznika,
    }
