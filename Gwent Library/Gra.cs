using Gwent_Library.Karty;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gwent_Library
{

    public class Gra
    {
        public Gracz gracz1 { get; set; }
        public Gracz gracz2 { get; set; }
        public Gracz ostatniGracz { get; set; }

        public Gra(Gracz player1, Gracz player2)
        {

            System.Diagnostics.Debug.WriteLine("Log: Start xD");

            if (player1.KartyGracza.Count < 20)
            {
                // throw new ZaMaloKartException("Gracz " + player1.Imie + " posiada za mało kart! ");
            }

            if (player2.KartyGracza.Count < 20)
            {
                //  throw new ZaMaloKartException("Gracz " + player2.Imie + " posiada za mało kart! ");
            }


            gracz1 = player1;
            gracz2 = player2;
            ostatniGracz = gracz2;
        }


      

        public void WykonajRuch(Gracz gracz, Karta karta)
        {

            // if (ostatniGracz != gracz)
            //  {

            gracz.Plansza.KartyGraczaWRozgrywce.Remove(karta);

            if (karta is KartaPiechoty k)
            {
                gracz.Plansza.KartyPiechotyGracza.Add(k);
            }
            else if (karta is KartaLucznika k1)
            {
                gracz.Plansza.KartyStrzeleckieGracza.Add(k1);
            }
            else if (karta is KartaObleznika k2)
            {
                gracz.Plansza.KartyOblezniczeGracza.Add(k2);
            }
            else if (karta is KartaSpecjalna k3)
            {
                gracz.Plansza.KartySpecjalne.Add(k3);
            }

            PrzliczPunkty(gracz);
            ostatniGracz = gracz;



            // }
            //  else
            // {
            //     throw new KolejnoscRuchuException("To nie jest kolej ruchu gracza: " + gracz.Imie);
            // }

        }

      

        public void AkcjaPogody(Gracz gracz)
        {
            var kartyPogody = gracz.Plansza.KartySpecjalne.Where(karta => karta is KartaPogody);

            if (kartyPogody.Any())
            {
                foreach (KartaPogody item in kartyPogody)
                {
                    item.AkcjaGlobalna(gracz1, gracz2);
                    if (item is CzysteNiebo)
                        break;
                }
            }
        }
        public void AkcjaRogu(Gracz gracz)
        {
            var kartyRogDowodcy = gracz.Plansza.KartySpecjalne.Where(karta => karta is RogDowodcy);
            if (kartyRogDowodcy.Any())
            {
                foreach (RogDowodcy item in kartyRogDowodcy)
                {
                    item.Akcja(gracz, item.Umiejscowienie);
                }
            }
        }

        public void AkcjaPozogi(Gracz gracz)
        {
            var kartyPozoga = gracz.Plansza.KartySpecjalne.Where(karta => karta is Pozoga);

            if (kartyPozoga.Any())
            {
                for (int i = kartyPozoga.Count() - 1; i >= 0; i--)
                {
                    Pozoga item = (Pozoga)kartyPozoga.ElementAt(i);
                    item.AkcjaGlobalna(gracz1, gracz2);
                    PrzliczPunkty(gracz);
                }
            }
        }

        public void AkcjaDowodcy(Gracz gracz)
        {
            var kartyDowodcy = gracz.Plansza.KartySpecjalne.Where(karta => karta is KartaDowodcy);
            if (kartyDowodcy.Any())
            {
                for (int i = kartyDowodcy.Count() - 1; i >= 0; i--)
                {
                    KartaDowodcy item = (KartaDowodcy)kartyDowodcy.ElementAt(i);
                    item.AkcjaGlobalna(gracz1, gracz2);
                    gracz.Plansza.KartySpecjalne.Remove(kartyDowodcy.ElementAt(i));

                    PrzliczPunkty(gracz);
                    break;
                }
            }

        }

        private void PrzliczPunkty(Gracz gracz) {

            ResetujPunkty(gracz);
            AkcjaPogody(gracz);
           AkcjaRogu(gracz);
            AkcjaPozogi(gracz);
            AkcjaDowodcy(gracz);
            PrzliczPunkty();

        }
        public void PrzliczPunkty()
        {
            gracz1.Plansza.PunktyPiechoty= PrzeliczPunkty(gracz1.Plansza.KartyPiechotyGracza);
            gracz2.Plansza.PunktyPiechoty = PrzeliczPunkty(gracz2.Plansza.KartyPiechotyGracza);
            gracz1.Plansza.PunktyStrzeleckie = PrzeliczPunkty(gracz1.Plansza.KartyStrzeleckieGracza);
            gracz2.Plansza.PunktyStrzeleckie = PrzeliczPunkty(gracz2.Plansza.KartyStrzeleckieGracza);
            gracz1.Plansza.PunktyObleznicze = PrzeliczPunkty(gracz1.Plansza.KartyOblezniczeGracza);
            gracz2.Plansza.PunktyObleznicze = PrzeliczPunkty(gracz2.Plansza.KartyOblezniczeGracza);


            OdswierzSumePunktow();
        }

        private int PrzeliczPunkty<T>(Talia<T> list) where T : KartaJednostki
        {
            return list.Any() ? list.Sum(karta => karta.Sila) : 0;
        }

        private void ResetujPunkty(Gracz gracz)
        {
            UstawDomyslnaWartoscKart(gracz.Plansza.KartyPiechotyGracza);
            UstawDomyslnaWartoscKart(gracz.Plansza.KartyStrzeleckieGracza);
            UstawDomyslnaWartoscKart(gracz.Plansza.KartyOblezniczeGracza);
        }

        private void UstawDomyslnaWartoscKart<T>(Talia<T> karty) where T : KartaJednostki
        {
            karty.ForEach(karta => karta.Sila = karta.DomyslnaSila);
        }
      
        private void OdswierzSumePunktow()
        {
            gracz1.Plansza.PunktySuma = gracz1.Plansza.PunktyPiechoty + gracz1.Plansza.PunktyStrzeleckie + gracz1.Plansza.PunktyObleznicze;
            gracz2.Plansza.PunktySuma = gracz2.Plansza.PunktyPiechoty + gracz2.Plansza.PunktyStrzeleckie + gracz2.Plansza.PunktyObleznicze;
        }

        public static Talia<Karta> GenerateCard()
        {
            Talia<Karta> karty = new Talia<Karta>();
            Karta Cirilla = new KartaPiechoty("Cirilla Fiona Elen Riannon", 15,  false, " Ciri");   
            Karta Geralt = new KartaPiechoty("Geralt z Rivii", 5,  false, "Geralt");
            Karta Yennefer = new KartaLucznika("Yennefer z Vengerbergu", 7,  false, "Yennefer");
            Karta Balista1 = new KartaObleznika("Balista (I)", 6,  false, "Balista");
            Karta Balista2 = new KartaObleznika("Balista (II)", 6,  false, "Balista");
            Karta BiednaPierdolonaPiechota1 = new KartaPiechoty("Biedna Pierdolona Piechota (I)", 1, false, "BiednaPierdolonaPiechota");       
            Karta Detmold = new KartaLucznika("Detmold", 6, false, "Detmold");
            Karta Rog3 = new RogDowodcy("Róg dowódcy 3",Umiejscowienie.Piechoty, "Rog");
            Karta Pozoga1 = new Pozoga("Pożoga 1", "Pozoga");         
            Karta Mroz1 = new TrzaskającyMroz("Trzaskający mróz 1", "TrzaskającyMroz");          
            Karta GestaMgla1 = new GestaMgla("Gęsta mgła 1", "GestaMgla");          
            Karta UlewnyDeszcz1 = new GestaMgla("Ulewny deszcz 1", "UlewnyDeszcz");          
            Karta CzysteNiebo2 = new CzysteNiebo("Czyste niebo 2", "CzysteNiebo");

            karty.Add(Cirilla);  
            karty.Add(Mroz1);
            karty.Add(Pozoga1);
            karty.Add(Rog3);
            karty.Add(Balista1);
            karty.Add(UlewnyDeszcz1);
            karty.Add(Yennefer);
            karty.Add(Balista2);
            karty.Add(CzysteNiebo2);

            /*   karty.Add(Mroz1);
               
               karty.Add(Geralt);
               karty.Add(GestaMgla1);      
               karty.Add(BiednaPierdolonaPiechota1);         
               karty.Add(Detmold);*/
            return karty;
        }
    }
}
