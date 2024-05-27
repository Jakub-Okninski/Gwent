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
    public class RogDowodcy : KartaWspoldzielona
    {
        public RogDowodcy(string nazwa, Umiejscowienie umiejscowienie, string nazwaZdjecia) : base(nazwa, umiejscowienie, nazwaZdjecia)
        {
        }
        public override void PolozKarte(Plansza plansza)
        {
            if (!(plansza.KartySpecjalne.Any(karta => karta is RogDowodcy k && k.Umiejscowienie == this.Umiejscowienie)))
            {
                plansza.KartyGraczaWRozgrywce.Remove(this);
                plansza.KartySpecjalne.Add(this);
            }
            else {
                Console.WriteLine("Karta Już istnijeje");
            }
         
        }
        public void Akcja(Gracz gracz, Umiejscowienie umiejscowienie)
        {
            if (gracz.Plansza.KartySpecjalne.Any(karta => karta is RogDowodcy k && k.Umiejscowienie == Umiejscowienie.Piechoty))
            {
                gracz.Plansza.KartyPiechotyGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));

            }
            else if (gracz.Plansza.KartySpecjalne.Any(karta => karta is RogDowodcy k && k.Umiejscowienie == Umiejscowienie.Lucznika))
            {
                gracz.Plansza.KartyStrzeleckieGracza.ForEach(karta => karta.Sila *= 2);
            }

            else if(gracz.Plansza.KartySpecjalne.Any(karta => karta is RogDowodcy k && k.Umiejscowienie == Umiejscowienie.Obleznicza))
            {
                gracz.Plansza.KartyOblezniczeGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));

            }
        }

      
    }    
}
