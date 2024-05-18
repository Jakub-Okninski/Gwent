using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class RogDowodcy : KartaWszystkichPol
    {
        public RogDowodcy (string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

        public override void WykonajAkcje(Gracz gracz, Gracz gracz2)
        {
            var wszystkieKartySpecjalne = gracz.Plansza.KartySpecjalne.Where(karta => karta is RogDowodcy);
            bool spr1 = true, spr2 = true, spr3 = true;
            foreach (RogDowodcy rog in wszystkieKartySpecjalne)
            {

                if (rog.kartaPolaJednostki is KartaPiechoty && spr1)
                {
                    gracz.Plansza.KartyPiechotyGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));
                    spr1 = false;
                }
                if (rog.kartaPolaJednostki is KartaLucznika && spr2)
                {
                    gracz.Plansza.KartyStrzeleckieGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));
                    spr3 = false;
                }
                if (rog.kartaPolaJednostki is KartaObleznika && spr3)
                {
                    gracz.Plansza.KartyOblezniczeGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));
                    spr3 = false;
                }
            }
        }    
    }
}
