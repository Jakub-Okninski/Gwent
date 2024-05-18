using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaObleznika : KartaJednostki
    {
        public KartaObleznika(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia) : base(nazwa, sila, kartaBohatera, nazwaZdjecia)
        {
        }
       
      

        public static void PomnozPunkty(Gracz gracz)
        {
            gracz.Plansza.KartyOblezniczeGracza.ForEach(karta => karta.Sila *= 2);
        }
    }
}
