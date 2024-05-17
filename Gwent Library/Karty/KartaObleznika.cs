using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaObleznika : KartaJednostki, IPolaOblęznicze
    {
        public KartaObleznika(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia) : base(nazwa, sila, kartaBohatera, nazwaZdjecia)
        {
        }
    }
}
