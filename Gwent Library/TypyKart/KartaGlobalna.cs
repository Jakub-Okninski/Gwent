using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class KartaGlobalna : KartaSpecjalna, ICloneable
    {
        public KartaGlobalna(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
        public KartaGlobalna() : base()
        {
        }
        public abstract void AkcjaGlobalna(Gracz gracz1, Gracz gracz2);

    }
}
