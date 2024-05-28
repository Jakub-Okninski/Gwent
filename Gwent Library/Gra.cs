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


        public void SprawdzRuch(Gracz gracz)
        {

            if(gracz1.Play&& gracz2.Play)
            {
                if (gracz != ostatniGracz)
                {
                    ostatniGracz = gracz;
                }
                else
                {
                    //throw new KolejnoscRuchuException("Nie twój ruch");
                }
            }else if (gracz.Play)
            {
                if (gracz == gracz1)
                {
                    ostatniGracz = gracz2;
                }
                else if (gracz == gracz2)
                {
                    ostatniGracz = gracz1;
                }
            }
            else if (!(gracz1.Play && gracz2.Play))
            {
                throw new EndGameException("Koniec rundy");
            }

        }




        public void WykonajRuch<T>(Gracz gracz, Karta karta, T kartaZamiana) where T : KartaJednostki
        {
            SprawdzRuch(gracz);
            karta.PolozKarte(gracz.Plansza);

            if (karta is Manekin kM)
            {
                 kM.Akcja<T>(gracz, kartaZamiana);
            }

            PrzliczPunkty(gracz);
    
        }

        public void WykonajRuch(Gracz gracz, Karta karta)
        {
            SprawdzRuch(gracz);
            karta.PolozKarte(gracz.Plansza);

            if(karta is Pozoga kP)
            {
                kP.AkcjaGlobalna(gracz1, gracz2);
            }
            else if (karta is KartaDowodcy kD)
            {
                kD.AkcjaGlobalna(gracz1, gracz2);
            }
            PrzliczPunkty(gracz);
    
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
                    System.Diagnostics.Debug.WriteLine("Logroggg   akcjaaaaaaaaaaa: Start xD");

                    item.Akcja(gracz, item.Umiejscowienie);
                }
            }
        }

    

     

        private void PrzliczPunkty(Gracz gracz) {
            ResetujPunkty(gracz);
            AkcjaPogody(gracz);
            AkcjaRogu(gracz);
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
            karty.ForEach(karta => karta.DomyslnaWartosc());
        }
      
        private void OdswierzSumePunktow()
        {
            gracz1.Plansza.PunktySuma = gracz1.Plansza.PunktyPiechoty + gracz1.Plansza.PunktyStrzeleckie + gracz1.Plansza.PunktyObleznicze;
            gracz2.Plansza.PunktySuma = gracz2.Plansza.PunktyPiechoty + gracz2.Plansza.PunktyStrzeleckie + gracz2.Plansza.PunktyObleznicze;
        }

        public static Talia<Karta> GenerateCard()
        {
            Talia<Karta> karty = new Talia<Karta>();
            Karta Cirilla = new KartaPiechoty("Cirilla Fiona Elen Riannon", 15,  false, " Ciri", CardEffects.Bractwo); 
            Karta Cirilla2 = new KartaPiechoty("Cirilla Fiona Elen Riannon", 15, false, " Ciri", CardEffects.Bractwo);

            Karta Geralt = new KartaPiechoty("Geralt z Rivii", 5,  false, "Geralt", CardEffects.Bractwo);
            Karta Yennefer = new KartaLucznika("Yennefer z Vengerbergu", 7,  false, "Yennefer", CardEffects.WyskoieMorale);
            Karta Yennefer2 = new KartaLucznika("Yennefer z Vengerbergu", 7, false, "Yennefer", CardEffects.WyskoieMorale);
            Karta Yennefer3 = new KartaLucznika("Yennefer z Vengerbergu", 7, false, "Yennefer", CardEffects.WyskoieMorale);
            Karta Balista1 = new KartaObleznika("Balista", 6,  false, "Balista", CardEffects.Wiez);
            Karta Balista2 = new KartaObleznika("Balista", 6,  false, "Balista", CardEffects.Wiez);
            Karta BiednaPierdolonaPiechota1 = new KartaPiechoty("Biedna Pierdolona Piechota (I)", 1, false, "BiednaPierdolonaPiechota", CardEffects.Bractwo);       
            Karta Detmold = new KartaLucznika("Detmold", 6, false, "Detmold", CardEffects.Bractwo);

            Karta Rog = new RogDowodcy("Róg dowódcy 3", Umiejscowienie.Piechoty, "Rog");

            Karta Rog3 = new RogDowodcy("Róg dowódcy 3",Umiejscowienie.Piechoty, "Rog");
            Karta Pozoga1 = new Pozoga("Pożoga 1", "Pozoga");         
            Karta Mroz1 = new TrzaskającyMroz("Trzaskający mróz 1", "TrzaskającyMroz");          
            Karta GestaMgla1 = new GestaMgla("Gęsta mgła 1", "GestaMgla");          
            Karta UlewnyDeszcz1 = new GestaMgla("Ulewny deszcz 1", "UlewnyDeszcz");          
            Karta CzysteNiebo2 = new CzysteNiebo("Czyste niebo 2", "CzysteNiebo");

            Karta foltest = new FoltestZdobywca("FoltestZdobywca", "FoltestZdobywca");
            Karta Manekin = new Manekin("Manekin",Umiejscowienie.Piechoty, "Manekin");

            Karta FoltestDowódcaPółnocy = new FoltestDowódcaPółnocy("FoltestDowódcaPółnocy", "FoltestDowódcaPółnocy");
            Karta FoltestKrólTemerii = new FoltestKrólTemerii("FoltestKrólTemerii", "FoltestKrólTemerii");
            Karta FoltestZelaznyWładca = new FoltestZelaznyWładca("FoltestZelaznyWładca", "FoltestZelaznyWładca");

            karty.Add(FoltestDowódcaPółnocy);

            karty.Add(FoltestKrólTemerii);
            karty.Add(FoltestZelaznyWładca);



            karty.Add(Manekin);

            karty.Add(UlewnyDeszcz1);
            karty.Add(Pozoga1);
            karty.Add(CzysteNiebo2);
            karty.Add(Cirilla2);
            karty.Add(Mroz1);
            karty.Add(foltest);
            karty.Add(Yennefer3);
            karty.Add(Balista2);
            karty.Add(Balista1);


            karty.Add(Cirilla);  
      
            karty.Add(Rog3);
  

            
            return karty;
        }
       
    }
}
