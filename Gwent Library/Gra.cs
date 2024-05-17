using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections;
using System.Collections.Generic;
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
            gracz.KartyGraczaWRozgrywce.Remove(karta);
            PolozKarte(gracz, karta);
            ostatniGracz = gracz;
            // }
            //  else
            // {
            //     throw new KolejnoscRuchuException("To nie jest kolej ruchu gracza: " + gracz.Imie);
            // }

        }


        private void PrzeliczPiechote(List<IPolaPiechoty> lista, Gracz gracz)
        {
            gracz.PunktyPiechoty = lista.OfType<KartaJednostki>().Sum(karta => karta.Sila);
            OdswierzSumePunktow(gracz);

        }
        private void PrzeliczStrzeleckie(List<IPolaStrzeleckie> lista, Gracz gracz)
        {
            gracz.PunktyStrzeleckie = lista.OfType<KartaJednostki>().Sum(karta => karta.Sila);
            OdswierzSumePunktow(gracz);
        }
        private void PrzeliczOblęznicze(List<IPolaOblęznicze> lista, Gracz gracz)
        {
            gracz.PunktyObleznicze = lista.OfType<KartaJednostki>().Sum(karta => karta.Sila);
            OdswierzSumePunktow(gracz);
        }



        private void OdswierzSumePunktow(Gracz gracz)
        {
            gracz.PunktySuma = gracz.PunktyPiechoty + gracz.PunktyStrzeleckie + gracz.PunktyObleznicze;
        }



        private void PolozKarte(Gracz gracz, Karta karta)
        {


            if (karta is IPolaPiechoty k)
            {
                gracz.KartyPiechotyGracza.Add(k);          
            }
            else if (karta is IPolaStrzeleckie k1)
            {
                gracz.KartyStrzeleckieGracza.Add(k1);         
            }
            else if (karta is IPolaOblęznicze k2)
            {
                gracz.KartyOblezniczeGracza.Add(k2);    
            }
            else if (karta is IPolaJednorazowe k3)
            {
                gracz.KartyJedenorazowe.Add(k3);
            }

            AktualizujWszystikePunktyTalii();
        }

        private void AktualizujWszystikePunktyTalii()
        {
            AktualizujPogode();


            AktualizujPozoga();

        }

        private void PrzeliczPunkty()
        {
            PrzeliczPiechote(gracz1.KartyPiechotyGracza, gracz1);
            PrzeliczStrzeleckie(gracz1.KartyStrzeleckieGracza, gracz1);
            PrzeliczOblęznicze(gracz1.KartyOblezniczeGracza, gracz1);

            PrzeliczPiechote(gracz2.KartyPiechotyGracza, gracz2);
            PrzeliczStrzeleckie(gracz2.KartyStrzeleckieGracza, gracz2);
            PrzeliczOblęznicze(gracz2.KartyOblezniczeGracza, gracz2);
        

        }

        private void AktualizujPozoga()
        {
            if (gracz1.KartyJedenorazowe.Any(karta => karta is Pozoga) || gracz2.KartyJedenorazowe.Any(karta => karta is Pozoga))
            {

                var l1 = gracz1.KartyJednoskiGracza();
                var l2 = gracz2.KartyJednoskiGracza();
                var wszystkieKarty = new List<KartaJednostki>();
                wszystkieKarty.AddRange(l1);
                wszystkieKarty.AddRange(l2);           
                int maksymalnaSila = wszystkieKarty.Any() ? wszystkieKarty.Max(karta => karta.Sila) : 0;

      



                PrzeliczPunkty();
                System.Diagnostics.Debug.WriteLine("+dasdasdas"+gracz1.KartyPiechotyGracza.Count);


            }
        }

        public void UsunPoSile(Gracz gracz) 
        {

           
            foreach(var i in gracz1.KartyJednoskiGracza.)

        }
        private void AktualizujPogode()
        {
      
            if (gracz1.KartyPiechotyGracza.Any(karta => karta is CzysteNiebo) || gracz2.KartyPiechotyGracza.Any(karta => karta is CzysteNiebo))
            {
                UstawSileKartPogody(gracz1.KartyPiechotyGracza.OfType<KartaJednostki>());
                UstawSileKartPogody(gracz2.KartyPiechotyGracza.OfType<KartaJednostki>());
                UstawSileKartPogody(gracz1.KartyStrzeleckieGracza.OfType<KartaJednostki>());
                UstawSileKartPogody(gracz2.KartyStrzeleckieGracza.OfType<KartaJednostki>());
                UstawSileKartPogody(gracz1.KartyOblezniczeGracza.OfType<KartaJednostki>());
                UstawSileKartPogody(gracz2.KartyOblezniczeGracza.OfType<KartaJednostki>());

                



                gracz1.KartyPiechotyGracza.RemoveAll(karta => karta is KartaPogody);
                gracz2.KartyPiechotyGracza.RemoveAll(karta => karta is KartaPogody);
                gracz1.KartyStrzeleckieGracza.RemoveAll(karta => karta is KartaPogody);
                gracz2.KartyStrzeleckieGracza.RemoveAll(karta => karta is KartaPogody);
                gracz1.KartyOblezniczeGracza.RemoveAll(karta => karta is KartaPogody);
                gracz2.KartyOblezniczeGracza.RemoveAll(karta => karta is KartaPogody);
            }
            else
            {
                if (gracz1.KartyPiechotyGracza.Any(karta => karta is TrzaskającyMroz) || gracz2.KartyPiechotyGracza.Any(karta => karta is TrzaskającyMroz))
                {
                    UstawSileKartPogody(gracz1.KartyPiechotyGracza.OfType<KartaJednostki>(), 1);
                    UstawSileKartPogody(gracz2.KartyPiechotyGracza.OfType<KartaJednostki>(), 1);
                }

                if (gracz1.KartyStrzeleckieGracza.Any(karta => karta is GestaMgla) || gracz2.KartyStrzeleckieGracza.Any(karta => karta is GestaMgla))
                {
                    UstawSileKartPogody(gracz1.KartyStrzeleckieGracza.OfType<KartaJednostki>(), 1);
                    UstawSileKartPogody(gracz2.KartyStrzeleckieGracza.OfType<KartaJednostki>(), 1);
                }
                if (gracz1.KartyOblezniczeGracza.Any(karta => karta is UlewnyDeszcz) || gracz2.KartyOblezniczeGracza.Any(karta => karta is UlewnyDeszcz))
                {
                    UstawSileKartPogody(gracz1.KartyOblezniczeGracza.OfType<KartaJednostki>(), 1);
                    UstawSileKartPogody(gracz2.KartyOblezniczeGracza.OfType<KartaJednostki>(), 1);
                }
            }
            PrzeliczPunkty();
        }

   

        private void UstawSileKartPogody(IEnumerable<KartaJednostki> lista, int sila = 0)
        {
            if (sila != 0)
            {
                foreach (var karta in lista)
                {
                    if (!karta.KartaBohatera)
                    {
                        karta.Sila = sila;
                    }
                }
            }
            else
            {
                foreach (var karta in lista)
                {
                    if (!karta.KartaBohatera)
                    {
                        karta.Sila = karta.DomyslnaSila;
                    }
                }
            }
          
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
            Karta Manekin1 = new Manekin("Manekin do ćwiczeń 1", "Manekin");       
            Karta Rog3 = new RogDowodcy("Róg dowódcy 3", "Rog");
            Karta Pozoga1 = new Pozoga("Pożoga 1", "Pozoga");         
            Karta Mroz1 = new TrzaskającyMroz("Trzaskający mróz 1", "TrzaskającyMroz");          
            Karta GestaMgla1 = new GestaMgla("Gęsta mgła 1", "GestaMgla");          
            Karta UlewnyDeszcz1 = new GestaMgla("Ulewny deszcz 1", "UlewnyDeszcz");          
            Karta CzysteNiebo2 = new CzysteNiebo("Czyste niebo 2", "CzysteNiebo");
            Karta FoltestDowodcaPólnocy = new KartaDowodcy("Foltest Dowódca Północy", "FoltestDowodcaPólnocy", "Usuwa aktywne efekty kart pogodowych");

            karty.Add(Cirilla);  
            karty.Add(Mroz1);
                        karty.Add(Pozoga1);
            karty.Add(Mroz1);

            karty.Add(Balista1);
            karty.Add(Geralt);
            karty.Add(UlewnyDeszcz1);    
            karty.Add(Yennefer);        
            karty.Add(CzysteNiebo2);      
            karty.Add(FoltestDowodcaPólnocy);
            karty.Add(Manekin1);        
            karty.Add(Rog3);
            karty.Add(GestaMgla1);      
            karty.Add(Balista2);
            karty.Add(BiednaPierdolonaPiechota1);         
            karty.Add(Detmold);
       
            return karty;
        }
    }
}
