using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public abstract class KartaDowodcy : KartaGlobalna, ICloneable
    {
        public string OpisWlasciwosci {get; set;}
        public KartaDowodcy(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia)
        {
            OpisWlasciwosci = opisWlasciwosci;  
        }

    }
}
