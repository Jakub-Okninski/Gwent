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
                if (gracz != ostatniGracz){
                    ostatniGracz = gracz;
                }else{
                    throw new KolejnoscRuchuException("Nie twój ruch");
                }
            }else if (gracz.Play)
            {
                if (gracz == gracz1){
                    ostatniGracz = gracz2;
                }else if (gracz == gracz2) {
                    ostatniGracz = gracz1;
                }
            }     
        }

        public void KonczRunde(Gracz gracz)
        {
            if (!gracz1.Play && !gracz1.Play)
            {
                PrzliczPunkty(gracz1);
                PrzliczPunkty(gracz2);
                if (gracz1.Plansza.PunktySuma> gracz2.Plansza.PunktySuma)
                {
                    gracz2.Punkty--;
                }
                else if (gracz1.Plansza.PunktySuma < gracz2.Plansza.PunktySuma)
                {
                    gracz1.Punkty--;
                }
                else if (gracz1.Plansza.PunktySuma == gracz2.Plansza.PunktySuma)
                {
                    if (gracz1 == gracz)
                    {
                        gracz2.Punkty--;
                    }
                    else
                    {
                        gracz1.Punkty--;
                    }
                }
                gracz1.Plansza.ResetujPanel();
                gracz2.Plansza.ResetujPanel();
            }

        }


        public Gracz ZwrocZwyciezce()
        {

            if (gracz1.Punkty > 0 && gracz2.Punkty > 0)
            {
                throw new EndGameException("Rozgrywka się nie zakończyła ");
            }
            if (gracz1.Punkty == 0)
            {
                return gracz2;
            }
            else if (gracz2.Punkty == 0)
            {
                return gracz1;
            }
            else
            {
                return null;
            }

        }




        public void KonczRozgrywke()
        {
            if (gracz1.Punkty==0)
            {
                throw new EndGameException(gracz2.Imie + " Wygrał !!!");
            }

            else if (gracz2.Punkty == 0)
            {
                throw new EndGameException(gracz1.Imie + " Wygrał !!!");
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

            if (karta is Pozoga kP)
            {
                kP.AkcjaGlobalna(gracz1, gracz2);
            }
            else if (karta is KartaDowodcy kD)
            {
                kD.AkcjaGlobalna(gracz1, gracz2);
            }
            PrzliczPunkty(gracz);


        }
      
        public void AkcjaPogody()
        {

            var kartyPogody = gracz1.Plansza.KartySpecjalne.Where(karta => karta is KartaPogody);
            
            if (kartyPogody.Any())
            {
                foreach (KartaPogody item in kartyPogody)
                {     
                    item.AkcjaGlobalna(gracz1, gracz2);
                    if (item is CzysteNiebo)
                        break;
                }
                
            }
            var kartyPogody2 = gracz2.Plansza.KartySpecjalne.Where(karta => karta is KartaPogody);

            if (kartyPogody2.Any())
            {
                foreach (KartaPogody item in kartyPogody2)
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

    

     

        private void PrzliczPunkty(Gracz gracz) {
            ResetujPunkty(gracz); 
            AkcjaPogody();   
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
            Talia<Karta> talia = new Talia<Karta>();




            Karta FoltestDowódcaPółnocy = new FoltestDowódcaPółnocy("Foltest Dowódca Północy", "FoltestDowódcaPółnocy");
            Karta FoltestKrólTemerii = new FoltestKrólTemerii("Foltest Król Temerii", "FoltestKrólTemerii");
            Karta FoltestZdobywca = new FoltestZdobywca("Foltest Zdobywca", "FoltestZdobywca");

            Karta Balista1 = new KartaObleznika("Balista", 6, false, "Balista", CardEffects.Bractwo);
            Karta Balista2 = new KartaObleznika("Balista", 6, false, "Balista", CardEffects.Bractwo);

            Karta BiednaPiechota = new KartaPiechoty("Biedna Pierdolona Piechota (I)", 1, false, "BiednaPiechota", CardEffects.Wiez);
            Karta BiednaPiechota2 = new KartaPiechoty("Biedna Pierdolona Piechota (II)", 1, false, "BiednaPiechota", CardEffects.Wiez);
            Karta BiednaPiechota3 = new KartaPiechoty("Biedna Pierdolona Piechota (III)", 1, false, "BiednaPiechota", CardEffects.Wiez);

            Karta Detmold = new KartaLucznika("Detmold", 6, false, "Yennefer", CardEffects.Default);
            Karta Esterad = new KartaPiechoty("Esterad Thyssen", 10, false, "Esterad", CardEffects.Default);
            Karta Filippa = new KartaObleznika("Filippa Eilhart", 10, false, "Filippa", CardEffects.Default);
            Karta JanNatalis = new KartaPiechoty("Jan Natalis", 10, false, "Jan Natalis", CardEffects.Default);
            Karta Katapulta1 = new KartaObleznika("Katapulta", 8, false, "Katapulta", CardEffects.Wiez);
            Karta Katapulta2 = new KartaObleznika("Katapulta", 8, false, "Katapulta", CardEffects.Wiez);
            Karta KeiraMetz = new KartaLucznika("Keira Metz", 5, false, "Keira Metz", CardEffects.Default);
            Karta Komandos1 = new KartaPiechoty("Komandos Niebieskich Pasów", 4, false, "Komandos Niebieskich Pasów", CardEffects.Wiez);
            Karta Komandos2 = new KartaPiechoty("Komandos Niebieskich Pasów", 4, false, "Komandos Niebieskich Pasów", CardEffects.Wiez);
            Karta Komandos3 = new KartaPiechoty("Komandos Niebieskich Pasów", 4, false, "Komandos Niebieskich Pasów", CardEffects.Wiez);
            Karta KsiążęStennis = new KartaPiechoty("Książę Stennis", 5, false, "Książę Stennis", CardEffects.Default);
   
            Karta MistrzOblezeń1 = new KartaPiechoty("Mistrz oblężeń z Kaedwen", 1, false, "Mistrz oblężeń z Kaedwen", CardEffects.WyskoieMorale);
            Karta MistrzOblezeń2 = new KartaPiechoty("Mistrz oblężeń z Kaedwen", 1, false, "Mistrz oblężeń z Kaedwen", CardEffects.WyskoieMorale);
            Karta MistrzOblezeń3 = new KartaPiechoty("Mistrz oblężeń z Kaedwen", 1, false, "Mistrz oblężeń z Kaedwen", CardEffects.WyskoieMorale);

            Karta RedańskiPiechur1 = new KartaPiechoty("Redański piechur (I)", 1, false, "Redański piechur", CardEffects.Default);
            Karta RedańskiPiechur2 = new KartaPiechoty("Redański piechur (II)", 1, false, "Redański piechur", CardEffects.Default);

            Karta Rębacze1 = new KartaLucznika("Rębacze z Crinfrid", 5, false, "Rębacze z Crinfrid", CardEffects.Wiez);
            Karta Rębacze2 = new KartaLucznika("Rębacze z Crinfrid", 5, false, "Rębacze z Crinfrid", CardEffects.Wiez);
            Karta Rębacze3 = new KartaLucznika("Rębacze z Crinfrid", 5, false, "Rębacze z Crinfrid", CardEffects.Wiez);
            Karta SabrinaGlevissig = new KartaPiechoty("Sabrina Glevissig", 4, false, "Sabrina Glevissig", CardEffects.Default);
            Karta ShealaDeTancarville = new KartaPiechoty("Sheala de Tancarville", 5, false, "Sheala de Tancarville", CardEffects.Default);
            Karta SheldonSkaggs = new KartaPiechoty("Sheldon Skaggs", 4, false, "Sheldon Skaggs", CardEffects.Default);
            Karta SigismundDijkstra = new KartaLucznika("Sigismund Dijkstra", 4, false, "Sigismund Dijkstra", CardEffects.Default);
            Karta Talar = new KartaObleznika("Talar", 1, false, "Talar", CardEffects.Default);
            Karta Trebusz1 = new KartaObleznika("Trebusz", 6, false, "Trebusz", CardEffects.Bractwo);
            Karta Trebusz2 = new KartaObleznika("Trebusz", 6, false, "Trebusz", CardEffects.Bractwo);
            Karta WieżaObleżnicza = new KartaObleznika("Wieża oblężnicza", 6, false, "Wieża oblężnicza", CardEffects.Default);
            Karta VernonRoche = new KartaPiechoty("Vernon Roche", 10, false, "Vernon Roche", CardEffects.Default);
            Karta Ves = new KartaPiechoty("Ves", 5, false, "Ves", CardEffects.Default);
            Karta YarpenZigrin = new KartaPiechoty("Yarpen Zigrin", 2, false, "Yarpen Zigrin", CardEffects.Default);
            Karta ZygfrydZDenesle = new KartaPiechoty("Zygfryd z Denesle", 5, false, "Zygfryd z Denesle", CardEffects.Default);

            Karta Manekin1 = new Manekin("Manekin", Umiejscowienie.Piechoty, "Manekin");
            Karta Rog1 = new RogDowodcy("Rog Dowodcy", Umiejscowienie.Piechoty, "RogDowodcy");
            Karta Pozoga1 = new Pozoga("Pozoga", "Pozoga");
            Karta TrzaskającyMroz1 = new TrzaskającyMroz("Trzaskający Mroz", "TrzaskającyMroz");
            Karta GestaMgla1 = new GestaMgla("Gesta Mgla", "GestaMgla");
            Karta UlewnyDeszcz1 = new UlewnyDeszcz("Ulewny Deszcz", "UlewnyDeszcz");
            Karta CzysteNiebo1 = new CzysteNiebo("Czyste Niebo", "CzysteNiebo");



            talia.Add(FoltestDowódcaPółnocy);
            talia.Add(FoltestKrólTemerii);
            talia.Add(FoltestZdobywca);
            talia.Add(Balista1);
            talia.Add(Balista2);
            talia.Add(BiednaPiechota);
            talia.Add(BiednaPiechota2);
            talia.Add(BiednaPiechota3);
            talia.Add(Detmold);
            talia.Add(Esterad);
            talia.Add(Filippa);
            talia.Add(JanNatalis);
            talia.Add(Katapulta1);
            talia.Add(Katapulta2);
            talia.Add(KeiraMetz);
            talia.Add(Komandos1);
            talia.Add(Komandos2);
            talia.Add(Komandos3);
            talia.Add(KsiążęStennis);
            talia.Add(MistrzOblezeń1);
            talia.Add(MistrzOblezeń2);
            talia.Add(MistrzOblezeń3);
            talia.Add(RedańskiPiechur1);
            talia.Add(RedańskiPiechur2);
            talia.Add(Rębacze1);
            talia.Add(Rębacze2);
            talia.Add(Rębacze3);
            talia.Add(SabrinaGlevissig);
            talia.Add(ShealaDeTancarville);
            talia.Add(SheldonSkaggs);
            talia.Add(SigismundDijkstra);
            talia.Add(Talar);
            talia.Add(Trebusz1);
            talia.Add(Trebusz2);
            talia.Add(WieżaObleżnicza);
            talia.Add(VernonRoche);
            talia.Add(Ves);
            talia.Add(YarpenZigrin);
            talia.Add(ZygfrydZDenesle);
            talia.Add(Manekin1);
            talia.Add(Rog1);
            talia.Add(Pozoga1);
            talia.Add(TrzaskającyMroz1);
            talia.Add(GestaMgla1);
            talia.Add(UlewnyDeszcz1);
            talia.Add(CzysteNiebo1);

            return talia;
        }

        public static Talia<Karta> GeneratePremiumCard()
        {
            Talia<Karta> karty = new Talia<Karta>();


            Karta Cirilla = new KartaPiechoty("Cirilla", 15, true, "Ciri", CardEffects.Default);
            Karta Emiel = new KartaPiechoty("Emiel Regis", 5, true, "Emiel", CardEffects.Default);
            Karta Geralt = new KartaPiechoty("Geralt", 15, true, "Geralt", CardEffects.Default);
            Karta Jaskier = new KartaPiechoty("Jaskier", 2, true, "Jaskier", CardEffects.WyskoieMorale);
            Karta Triss = new KartaPiechoty("Triss", 7, true, "Triss", CardEffects.Default);
            Karta Vesemir = new KartaPiechoty("Vesemir", 6, true, "Vesemir", CardEffects.Default);
            Karta Villentretenmerth = new KartaPiechoty("Villentretenmerth", 7, true, "Villentretenmerth", CardEffects.Default);
            Karta Yennefer = new KartaLucznika("Yennefer", 7, true, "Yennefer", CardEffects.Default);
            Karta Zoltan = new KartaPiechoty("Zoltan", 5, true, "Zoltan", CardEffects.Default);


         
            Karta Manekin2 = new Manekin("Manekin", Umiejscowienie.Piechoty, "Manekin");
            Karta Manekin3 = new Manekin("Manekin", Umiejscowienie.Piechoty, "Manekin");
            Karta Rog2 = new RogDowodcy("Rog Dowodcy", Umiejscowienie.Piechoty, "RogDowodcy");
            Karta Rog3 = new RogDowodcy("Rog Dowodcy", Umiejscowienie.Piechoty, "RogDowodcy");
            Karta Pozoga2 = new Pozoga("Pozoga", "Pozoga");
            Karta Pozoga3 = new Pozoga("Pozoga", "Pozoga");

            Karta TrzaskającyMroz2 = new TrzaskającyMroz("Trzaskający Mroz", "TrzaskającyMroz");
            Karta TrzaskającyMroz3 = new TrzaskającyMroz("Trzaskający Mroz", "TrzaskającyMroz");

           
            Karta GestaMgla2 = new GestaMgla("Gesta Mgla", "GestaMgla");
            Karta GestaMgla3 = new GestaMgla("Gesta Mgla", "GestaMgla");

          
            Karta UlewnyDeszcz2 = new UlewnyDeszcz("Ulewny Deszcz", "UlewnyDeszcz");
            Karta UlewnyDeszcz3 = new UlewnyDeszcz("Ulewny Deszcz", "UlewnyDeszcz");


            Karta CzysteNiebo2 = new CzysteNiebo("Czyste Niebo", "CzysteNiebo");

            karty.Add(Cirilla);
            karty.Add(Pozoga2);
            karty.Add(TrzaskającyMroz2);
            karty.Add(GestaMgla2);
            karty.Add(UlewnyDeszcz3);
            karty.Add(Emiel);
            karty.Add(Geralt);
            karty.Add(Jaskier);
            karty.Add(Triss);
            karty.Add(Vesemir);
            karty.Add(Villentretenmerth);
            karty.Add(Yennefer);
            karty.Add(Zoltan);  
            karty.Add(Manekin2);
            karty.Add(Manekin3);
            karty.Add(Rog2);
            karty.Add(Rog3);
            karty.Add(Pozoga3);
            karty.Add(TrzaskającyMroz3);  
            karty.Add(GestaMgla3);
            karty.Add(UlewnyDeszcz2);
            karty.Add(CzysteNiebo2);


            return karty;
        }

    }
}
