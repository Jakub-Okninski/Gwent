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
            Talia<Karta> karty = new Talia<Karta>();


            Karta Cirilla = new KartaPiechoty("Cirilla", 15,  true, "Ciri", CardEffects.Default);
            Karta Emiel = new KartaPiechoty("Emiel Regis", 5, true, "Emiel", CardEffects.Default);
            Karta Geralt = new KartaPiechoty("Geralt", 15, true, "Geralt", CardEffects.Default);
            Karta Jaskier = new KartaPiechoty("Jaskier", 2, true, "Jaskier", CardEffects.WyskoieMorale);
            Karta Triss = new KartaPiechoty("Triss", 7, true, "Triss", CardEffects.Default);
            Karta Vesemir = new KartaPiechoty("Vesemir", 6, true, "Vesemir", CardEffects.Default);
            Karta Villentretenmerth = new KartaPiechoty("Villentretenmerth", 7, true, "Villentretenmerth", CardEffects.Default);
            Karta Yennefer = new KartaLucznika("Yennefer", 7, true, "Yennefer", CardEffects.Default);

            Karta Zoltan = new KartaPiechoty("Zoltan", 5, true, "Zoltan", CardEffects.Default);


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



            karty.Add(Balista1);
            karty.Add(Balista2);
            karty.Add(BiednaPiechota);
            karty.Add(BiednaPiechota2);
            karty.Add(BiednaPiechota3);
            karty.Add(Detmold);
            karty.Add(Esterad);
            karty.Add(Filippa);
            karty.Add(JanNatalis);
            karty.Add(Katapulta1);
            karty.Add(Katapulta2);
            karty.Add(KeiraMetz);
            karty.Add(Komandos1);
            karty.Add(Komandos2);
            karty.Add(Komandos3);
            karty.Add(KsiążęStennis);
            karty.Add(MistrzOblezeń1);
            karty.Add(MistrzOblezeń2);
            karty.Add(MistrzOblezeń3);
            karty.Add(RedańskiPiechur1);
            karty.Add(RedańskiPiechur2);
            karty.Add(Rębacze1);
            karty.Add(Rębacze2);
            karty.Add(Rębacze3);
            karty.Add(SabrinaGlevissig);
            karty.Add(ShealaDeTancarville);
            karty.Add(SheldonSkaggs);
            karty.Add(SigismundDijkstra);
            karty.Add(Talar);
            karty.Add(Trebusz1);
            karty.Add(Trebusz2);
            karty.Add(WieżaObleżnicza);
            karty.Add(VernonRoche);
            karty.Add(Ves);
            karty.Add(YarpenZigrin);
            karty.Add(ZygfrydZDenesle);




            karty.Add(Cirilla);
            karty.Add(Emiel);
            karty.Add(Geralt);
            karty.Add(Jaskier);
            karty.Add(Triss);
            karty.Add(Vesemir);
            karty.Add(Villentretenmerth);
            karty.Add(Yennefer);
            karty.Add(Zoltan);
            karty.Add(FoltestDowódcaPółnocy);
            karty.Add(FoltestKrólTemerii);
            karty.Add(FoltestZdobywca);
           






            return karty;
        }
       
    }
}
