using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.KartyDowodcow;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class RogDowodcy : KartaWszystkichPol
    {
        public RogDowodcy (string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            WykonajRog(gracz1);
        }
       
        private void WykonajRog(Gracz gracz)
        {
            var wszystkieKartySpecjalne = gracz.Plansza.KartySpecjalne.Where(karta => karta is RogDowodcy);
            bool spr1 = true, spr2 = true, spr3 = true;
            foreach (RogDowodcy rog in wszystkieKartySpecjalne)
            {

                if (rog.kartaPolaJednostki is KartaPiechoty&& spr1)
                {
                    KartaPiechoty.PomnozPunkty(gracz);
                    spr1 = false;
                }
                if (rog.kartaPolaJednostki is KartaLucznika && spr2)
                {
                    KartaLucznika.PomnozPunkty(gracz);
                    spr3 = false;
                }
                if (rog.kartaPolaJednostki is KartaObleznika && spr3)
                {
                    KartaObleznika.PomnozPunkty(gracz);
                    spr3 = false;
                }
            }
        }
    }
}
