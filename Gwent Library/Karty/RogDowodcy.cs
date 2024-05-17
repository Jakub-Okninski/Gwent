using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class RogDowodcy : KartaSpecjalna, IPolaPiechoty, IPolaStrzeleckie, IPolaOblęznicze
    {
        public RogDowodcy (string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
    }
}
