using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class GestaMgla : KartaPogody, IPolaStrzeleckie
    {
        public GestaMgla(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
    }
}
