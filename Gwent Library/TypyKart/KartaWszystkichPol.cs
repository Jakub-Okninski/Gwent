using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty
{
    public abstract class KartaWszystkichPol : KartaSpecjalna
    {
        public KartaJednostki kartaPolaJednostki { get; set; }  
        public KartaWszystkichPol(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

       
    }
  

}

