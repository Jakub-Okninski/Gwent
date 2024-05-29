using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    [Serializable]
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

        public static void Default(Karta karta, Plansza plansza)
        {
            
        }
        private static void BractwoHelp<T>(Karta karta, Talia<T> talia, Plansza plansza) where T : KartaJednostki
        {
          //  talia.RemoveAll(k => k is KartaJednostki && k.Nazwa.Contains(karta.Nazwa) && k.Effect == Bractwo);

            var kartyInne = plansza.WszystkieKartyGracza.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == Bractwo);

            foreach (T kk in kartyInne)
            {
                talia.Add(kk);
            }
            plansza.WszystkieKartyGracza.RemoveAll(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa)
        && kp.Effect == Bractwo);


            var kartyInne2 = plansza.KartyGraczaWRozgrywce.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == Bractwo);

            foreach (T kk in kartyInne2)
            {
                talia.Add(kk);
            }

            plansza.KartyGraczaWRozgrywce.RemoveAll(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa)
            && kp.Effect == Bractwo);
           
        }

        public static void Wiez(Karta karta, Plansza plansza)
        {
            if (karta is KartaPiechoty)
            {
                WizeHelp(karta, plansza.KartyPiechotyGracza);
            }
            else if (karta is KartaLucznika)
            {
                WizeHelp(karta, plansza.KartyStrzeleckieGracza);
            }
            else if (karta is KartaObleznika)
            {
                WizeHelp(karta, plansza.KartyOblezniczeGracza);
            }
        }


        private static void WizeHelp<T>(Karta karta,  Talia<T> talia) where T : KartaJednostki
        {
            var usunieteKarty = talia.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == Wiez);
            foreach (KartaJednostki k in usunieteKarty)
            {
                k.DomyslnaSila = k.Default * usunieteKarty.Count();
               // k.DomyslnaWartosc();
            }
        }


   

        public static void WyskoieMorale(Karta karta, Plansza plansza)
        {
            if (karta is KartaPiechoty)
            {
                WyskoieMoraleHelp(karta, plansza.KartyPiechotyGracza);
            }
            else if (karta is KartaLucznika)
            {            
                WyskoieMoraleHelp(karta, plansza.KartyStrzeleckieGracza);
            }
            else if (karta is KartaObleznika)
            {
                WyskoieMoraleHelp(karta, plansza.KartyOblezniczeGracza);
            }

        }
        private static void WyskoieMoraleHelp<T>(Karta karta, Talia<T> talia) where T : KartaJednostki
        {
            var usunieteKarty = talia.Where(k => k is KartaJednostki kp && kp.Nazwa.Contains(karta.Nazwa) && kp.Effect == WyskoieMorale);
            foreach (KartaJednostki k in usunieteKarty)
            {
                if (usunieteKarty.Count() > 1)
                {
                    k.DomyslnaSila = k.Default + usunieteKarty.Count();
                    k.DomyslnaWartosc();
                }
            }
        }

    }
}
