using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class KartaPogody : KartaSpecjalna, IPolaJednorazowe
    {
        public KartaPogody(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
    }
}
