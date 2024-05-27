using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class CardEffects
    {
        public static void Bractwo(Karta karta, Plansza plansza)
        {
            if (karta is KartaPiechoty)
            {        
                BractwoHelp(karta, plansza.KartyPiechotyGracza, plansza);
            }
            else if (karta is KartaLucznika)
            {          
                BractwoHelp(karta, plansza.KartyStrzeleckieGracza, plansza);
            }
            else if (karta is KartaObleznika)
            {
                BractwoHelp(karta, plansza.KartyOblezniczeGracza, plansza);
            }
        }

        private static void BractwoHelp<T>(Karta karta, Talia<T> talia, Plansza plansza) where T : KartaJednostki
        {
            talia.RemoveAll(k => k is KartaJednostki && k.Nazwa.Contains(karta.Nazwa) && k.Effect == Bractwo);

            var usunieteKarty = plansza.WszystkieKartyGracza.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == Bractwo);

            plansza.KartyGraczaWRozgrywce.RemoveAll(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa)
            && kp.Effect == Bractwo);
            foreach (T kk in usunieteKarty)
            {
                talia.Add(kk);
            }
        }

        public static void Wiez(Karta karta, Plansza plansza)
        {

            var silaKarty = plansza.WszystkieKartyGracza.FirstOrDefault(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == Wiez);

            int sila = silaKarty is KartaJednostki kartaPiechoty ? kartaPiechoty.Sila : 0;

            if (karta is KartaPiechoty)
            {
                WizeHelp(karta, plansza.KartyPiechotyGracza, sila);
            }
            else if (karta is KartaLucznika)
            {
                WizeHelp(karta, plansza.KartyStrzeleckieGracza, sila);
            }
            else if (karta is KartaObleznika)
            {
                WizeHelp(karta, plansza.KartyOblezniczeGracza, sila);
            }
        }


        private static void WizeHelp<T>(Karta karta,  Talia<T> talia, int sila) where T : KartaJednostki
        {
            var usunieteKarty = talia.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == Wiez);
            foreach (KartaJednostki k in usunieteKarty)
            {

                k.DomyslnaSila = sila * usunieteKarty.Count();
                k.DomyslnaWartosc();
            }
        }


   

        public static void WyskoieMorale(Karta karta, Plansza plansza)
        {

            var KartaSily = plansza.WszystkieKartyGracza.FirstOrDefault(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == WyskoieMorale);

            int sila = KartaSily is KartaJednostki kartaPiechoty ? kartaPiechoty.Sila : 0;

            if (karta is KartaPiechoty)
            {
                WyskoieMoraleHelp(karta, plansza.KartyPiechotyGracza, sila);
            }
            else if (karta is KartaLucznika)
            {            
                WyskoieMoraleHelp(karta, plansza.KartyStrzeleckieGracza, sila);
            }
            else if (karta is KartaObleznika)
            {
                WyskoieMoraleHelp(karta, plansza.KartyOblezniczeGracza, sila);
            }



        }
        private static void WyskoieMoraleHelp<T>(Karta karta, Talia<T> talia, int sila) where T : KartaJednostki
        {
            var usunieteKarty = talia.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == WyskoieMorale);
            foreach (KartaJednostki k in usunieteKarty)
            {

                if (usunieteKarty.Count() > 1)
                {
                    k.DomyslnaSila = sila + usunieteKarty.Count();
                    k.DomyslnaWartosc();
                }
            }
        }

    }
}
