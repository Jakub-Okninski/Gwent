using Gwent_Library.Karty;
using System;
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
                throw new ZaMaloKartException("Gracz " + player1.Imie + " posiada za mało kart! ");
            }

            if (player2.KartyGracza.Count < 20)
            {
                throw new ZaMaloKartException("Gracz " + player2.Imie + " posiada za mało kart! ");
            }
            gracz1 = player1;
            gracz2 = player2;
            ostatniGracz = gracz2;

        }

        public void WykonajRuch(Gracz gracz, Karta karta)
        {

            if (ostatniGracz != gracz)
            {
                gracz.KartyGraczaWRozgrywce.Remove(karta);
                PolozKarte(gracz, karta);
                ostatniGracz = gracz;
            }
            else
            {
                throw new KolejnoscRuchuException("To nie jest kolej ruchu gracza: " + gracz.Imie);
            }

        }
        private void WykonajPorzoge(Gracz gracz)
        {
            // Połącz wszystkie listy w jedną listę kard


            var kartyDziedziczacePoJednostkach = gracz.KartyPiechotyGracza.Where(karta => karta is KartaJednostek).ToList();


            var wszystkieKarty = gracz.KartyPiechotyGracza.Cast<Karta>()
                                    .Concat(KartyStrzeleckieGracza.Cast<IKarta>())
                                    .Concat(KartyOblezniczeGracza.Cast<IKarta>());

            // Znajdź największą kartę z punktami siły
            var najwiekszaKarta = wszystkieKarty.OrderByDescending(karta => karta.PunktySily).FirstOrDefault();

        }
        private void PrzeliczPiechote(Gracz gracz)
        {
          
        }

        private void PolozKarte (Gracz gracz, Karta karta)
        {

            if (karta is IPiechoty && karta is IStrzeleckich && karta is IOblęzniczych)
            {

            }
            else if(karta is IPiechoty k)
            {
                gracz.KartyPiechotyGracza.Add(k);
                PrzeliczPiechote(gracz);
            }
            else if(karta is IStrzeleckich k1)
            {
                gracz.KartyStrzeleckieGracza.Add(k1);
            }
            else if(karta is IOblęzniczych k2)
            {
                gracz.KartyOblezniczeGracza.Add(k2);
            }
            else if (karta is IBezPola)
            {
                gracz.UsunieteKartyGracza.Add(karta);
            }
        


        }


        public static Talia GenerateCard()
        {
            Talia karty = new Talia(); 

            IWlasciwosc wysokieMorale = new WysokieMorale();
            IWlasciwosc wiez = new Wiez();
            IWlasciwosc szpieg = new Szpieg();
            IWlasciwosc medyk = new Medyk();
            IWlasciwosc atakujacy = new Atakujacy();



            Karta Cirilla = new KartaPiechoty("Cirilla Fiona Elen Riannon", 15, true, "Cirilla", atakujacy);
            Karta EmielRegis = new KartaPiechoty("Emiel Regis Rohellec Terzieff", 5, true, "EmielRegis", atakujacy);
            Karta Geralt = new KartaPiechoty("Geralt z Rivii", 5, true, "Geralt", atakujacy);

            Karta FilippaEilhart = new KartaObleznika("Filippa Eilhart", 10, false, "FilippaEilhart", atakujacy);
            Karta Jaskier = new KartaPiechoty("Jaskier", 2, true, "Jaskier", wysokieMorale);




            Karta TajemniczyElf = new KartaPiechoty("Tajemniczy elf", 0, true, "TajemniczyElf", szpieg);
            Karta Triss = new KartaPiechoty("Triss Medigold", 7, true, "Triss", atakujacy);
            Karta Vesemir = new KartaPiechoty("Vesemir", 6, true, "Vesemir", atakujacy);
            Karta Villentretenmerth = new KartaPiechoty("Villentretenmerth", 7, true, "Villentretenmerth", atakujacy);
            Karta Yennefer = new KartaLucznika("Yennefer z Vengerbergu", 7, true, "Yennefer", medyk);
            Karta Zoltan = new KartaLucznika("Zoltan Chivay", 5, true, "Zoltan", atakujacy);



            Karta Balista1 = new KartaObleznika("Balista (I)", 6, false, "Balista", atakujacy);
            Karta Balista2 = new KartaObleznika("Balista (II)", 6, false, "Balista", atakujacy);

            Karta BiednaPierdolonaPiechota1 = new KartaPiechoty("Biedna Pierdolona Piechota (I)", 1, false, "BiednaPierdolonaPiechota", wiez);
            Karta BiednaPierdolonaPiechota2 = new KartaPiechoty("Biedna Pierdolona Piechota (II)", 1, false, "BiednaPierdolonaPiechota", wiez);
            Karta BiednaPierdolonaPiechota3 = new KartaPiechoty("Biedna Pierdolona Piechota (III)", 1, false, "BiednaPierdolonaPiechota", wiez);

            Karta Detmold = new KartaLucznika("Detmold", 6, false, "Detmold", atakujacy);

            Karta EsteradThyssen = new KartaPiechoty("Esterad Thyssen", 10, false, "EsteradThyssen", atakujacy);

  


            Karta JanNatalis = new KartaPiechoty("Jan Natalis", 10, false, "JanNatalis", atakujacy);

            Karta Katapulta1 = new KartaObleznika("Katapulta (I)", 8, false, "Katapulta", wiez);
            Karta Katapulta2 = new KartaObleznika("Katapulta (II)", 8, false, "Katapulta", wiez);

            Karta KeiraMetz = new KartaLucznika("Keira Metz", 5, false, "KeiraMetz", atakujacy);

            Karta KomandosNiebieskichPasow1 = new KartaPiechoty("Komandos Niebieskich Pasów (I)", 4, false, "KomandosNiebieskichPasow", wiez);
            Karta KomandosNiebieskichPasow2 = new KartaPiechoty("Komandos Niebieskich Pasów (II)", 4, false, "KomandosNiebieskichPasow", wiez);
            Karta KomandosNiebieskichPasow3 = new KartaPiechoty("Komandos Niebieskich Pasów (III)", 4, false, "KomandosNiebieskichPasow", wiez);

            Karta KsiazeStennis = new KartaPiechoty("Książę Stennis", 5, false, "KsiazeStennis", szpieg);

            Karta MedyczkaBurejChoragwi = new KartaObleznika("Medyczka Burej Chorągwi", 5, false, "MedyczkaBurejChoragwi", medyk);

            Karta MistrzOblezenZKaedwen1 = new KartaObleznika("Mistrz oblężeń z Kaedwen (I)", 1, false, "MistrzOblezenZKaedwen", wysokieMorale);
            Karta MistrzOblezenZKaedwen2 = new KartaObleznika("Mistrz oblężeń z Kaedwen (II)", 1, false, "MistrzOblezenZKaedwen", wysokieMorale);
            Karta MistrzOblezenZKaedwen3 = new KartaObleznika("Mistrz oblężeń z Kaedwen (III)", 1, false, "MistrzOblezenZKaedwen", wysokieMorale);

            Karta RedanskiPiechur1 = new KartaPiechoty("Redański piechur (I)", 1, false, "RedanskiPiechur", atakujacy);
            Karta RedanskiPiechur2 = new KartaPiechoty("Redański piechur (II)", 1, false, "RedanskiPiechur", atakujacy);

            Karta RebaczeZCrinfrid1 = new KartaLucznika("Rębacze z Crinfrid (I)", 5, false, "RebaczeZCrinfrid", wiez);
            Karta RebaczeZCrinfrid2 = new KartaLucznika("Rębacze z Crinfrid (II)", 5, false, "RebaczeZCrinfrid", wiez);
            Karta RebaczeZCrinfrid3 = new KartaLucznika("Rębacze z Crinfrid (III)", 5, false, "RebaczeZCrinfrid", wiez);

            Karta SabrinaGlevissig = new KartaLucznika("Sabrina Glevissig", 4, false, "SabrinaGlevissig", atakujacy);

            Karta ShealaDeTancarville = new KartaLucznika("Sheala de Tancarville", 5, false, "ShealaDeTancarville", atakujacy);

            Karta SheldonSkaggs = new KartaLucznika("Sheldon Skaggs", 4, false, "SheldonSkaggs", atakujacy);

            Karta SigismundDijkstra = new KartaPiechoty("Sigismund Dijkstra", 4, false, "SigismundDijkstra", szpieg);

            Karta Talar = new KartaObleznika("Talar", 1, false, "Talar", szpieg);

            Karta Trebusz1 = new KartaObleznika("Trebusz (I)", 6, false, "Trebusz", atakujacy);
            Karta Trebusz2 = new KartaObleznika("Trebusz (II)", 6, false, "Trebusz", atakujacy);

            Karta WiezaObleznicza = new KartaObleznika("Wieża oblężnicza", 6, false, "WiezaObleznicza", atakujacy);

            Karta VernonRoche = new KartaPiechoty("Vernon Roche", 10, false, "VernonRoche", atakujacy);

            Karta Ves = new KartaPiechoty("Ves", 5, false, "Ves", atakujacy);

            Karta YarpenZigrin = new KartaPiechoty("Yarpen Zigrin", 2, false, "YarpenZigrin", atakujacy);

            Karta ZygfrydZDenesle = new KartaPiechoty("Zygfryd z Denesle", 5, false, "ZygfrydZDenesle", atakujacy);






            Karta Manekin1 = new Manekin("Manekin do ćwiczeń 1", "Manekin");
            Karta Manekin2 = new Manekin("Manekin do ćwiczeń 2", "Manekin");
            Karta Manekin3 = new Manekin("Manekin do ćwiczeń 3", "Manekin");
            Karta Rog1 = new RogDowodcy("Róg dowódcy 1", "Rog");
            Karta Rog2 = new RogDowodcy("Róg dowódcy 2", "Rog");
            Karta Rog3 = new RogDowodcy("Róg dowódcy 3", "Rog");
            Karta Pozoga1 = new Pozoga("Pożoga 1", "Pozoga");
            Karta Pozoga2 = new Pozoga("Pożoga 2", "Pozoga");
            Karta Pozoga3 = new Pozoga("Pożoga 3", "Pozoga");
            Karta Mroz1 = new TrzaskającyMroz("Trzaskający mróz 1", "TrzaskającyMroz");
            Karta Mroz2 = new TrzaskającyMroz("Trzaskający mróz 2", "TrzaskającyMroz");
            Karta Mroz3 = new TrzaskającyMroz("Trzaskający mróz 3", "TrzaskającyMroz");
            Karta GestaMgla1 = new GestaMgla("Gęsta mgła 1", "GestaMgla");
            Karta GestaMgla2 = new GestaMgla("Gęsta mgła 2", "GestaMgla");
            Karta GestaMgla3 = new GestaMgla("Gęsta mgła 3", "GestaMgla");
            Karta UlewnyDeszcz1 = new GestaMgla("Ulewny deszcz 1", "UlewnyDeszcz");
            Karta UlewnyDeszcz2 = new GestaMgla("Ulewny deszcz 2", "UlewnyDeszcz");
            Karta CzysteNiebo1 = new CzysteNiebo("Czyste niebo 1", "CzysteNiebo");
            Karta CzysteNiebo2 = new CzysteNiebo("Czyste niebo 2", "CzysteNiebo");



            Karta FoltestDowodcaPólnocy = new KartaDowodcy("Foltest Dowódca Północy", "FoltestDowodcaPólnocy", "Usuwa aktywne efekty kart pogodowych");


            Karta FoltestKrolTemerii = new KartaDowodcy("Foltest Król Temerii", "FoltestKrolTemerii", "Natychmiastowe przywołanie i użycie karty: Gęsta mgła");

            Karta FoltestZdobywca = new KartaDowodcy("Foltest Zdobywca", "FoltestZdobywca", "Podwaja siłę twoich jednostek oblężniczych. Nie kumuluje się z efektem: Rogu dowódcy");

            Karta FoltestZelaznyWładca = new KartaDowodcy("Foltest Żelazny Władca", "FoltestZelaznyWładca", "Niszczy najsilniejszą jednostkę lub jednostki oblężnicze przeciwnika. Działa wyłącznie, gdy siła jednostek oblężniczych przeciwnika wynosi 10 bądź więcej");


            karty.Add(Cirilla);
      
            karty.Add(Pozoga3);
            karty.Add(Mroz1);
            karty.Add(FoltestKrolTemerii);
            karty.Add(FoltestZdobywca);
            karty.Add(CzysteNiebo2);
            karty.Add(Pozoga2);
            karty.Add(GestaMgla3);

            karty.Add(Rog2);
            karty.Add(EmielRegis);
            karty.Add(Geralt);
            karty.Add(FilippaEilhart);
            karty.Add(UlewnyDeszcz1);
            karty.Add(RebaczeZCrinfrid3);
            karty.Add(SabrinaGlevissig);
            karty.Add(Jaskier);
            karty.Add(TajemniczyElf);
            karty.Add(Triss);
            karty.Add(Vesemir);
            karty.Add(Villentretenmerth);
            karty.Add(Yennefer);
            karty.Add(Zoltan);



            karty.Add(FoltestDowodcaPólnocy);

            karty.Add(FoltestZelaznyWładca);



            karty.Add(Manekin1);
            karty.Add(Manekin2);
            karty.Add(Manekin3);
            karty.Add(Rog1);
            karty.Add(Rog3);
            karty.Add(Pozoga1);
            karty.Add(Pozoga3);
            karty.Add(Mroz1);
            karty.Add(Mroz2);
            karty.Add(Mroz3);
            karty.Add(GestaMgla1);
            karty.Add(GestaMgla2);
         
            karty.Add(UlewnyDeszcz2);
            karty.Add(CzysteNiebo1);
           
            
   

            karty.Add(Balista1);
            karty.Add(Balista2);
            karty.Add(BiednaPierdolonaPiechota1);
            karty.Add(BiednaPierdolonaPiechota2);
            karty.Add(BiednaPierdolonaPiechota3);
            karty.Add(Detmold);
            karty.Add(EsteradThyssen);
        
            karty.Add(JanNatalis);
            karty.Add(Katapulta1);
            karty.Add(Katapulta2);
            karty.Add(KeiraMetz);
            karty.Add(KomandosNiebieskichPasow1);
            karty.Add(KomandosNiebieskichPasow2);
            karty.Add(KomandosNiebieskichPasow3);
            karty.Add(KsiazeStennis);
            karty.Add(MedyczkaBurejChoragwi);
            karty.Add(MistrzOblezenZKaedwen1);
            karty.Add(MistrzOblezenZKaedwen2);
            karty.Add(MistrzOblezenZKaedwen3);
            karty.Add(RedanskiPiechur1);
            karty.Add(RedanskiPiechur2);
            karty.Add(RebaczeZCrinfrid1);
            karty.Add(RebaczeZCrinfrid2);

            karty.Add(ShealaDeTancarville);
            karty.Add(SheldonSkaggs);
            karty.Add(SigismundDijkstra);
            karty.Add(Talar);
            karty.Add(Trebusz1);
            karty.Add(Trebusz2);
            karty.Add(WiezaObleznicza);
            karty.Add(VernonRoche);
            karty.Add(Ves);
            karty.Add(YarpenZigrin);
            karty.Add(ZygfrydZDenesle);

            return karty;
        }
    }
}
