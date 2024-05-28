using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.Karty;
using Gwent_Library.TypyKart;

namespace Gwent_Library
{
    public class Gracz 
    {
        public string Imie { get; set; }
        public Talia<Karta> KartyGracza { get; set; }
        public Plansza Plansza { get; set; }
        public int Punkty { get; set; }
        public bool Play { get; set; } 

        public Gracz(string imie, Talia<Karta> kartygracza)
        {
            Imie = imie;
            KartyGracza = kartygracza;
            Punkty = 2;
            Play = true;
        }
        public void UstawPlansze(Talia<Karta> kartygracza, Talia<Karta> taliawRozgrywce)
        {
            Plansza = new Plansza(kartygracza, taliawRozgrywce);

        }
        public void ZmniejszPunkty()
        {
            Punkty--;
            if (Punkty <= 0)
            {
                throw new EndGameException("Gracz "+Imie+ " stracił wszystkie punkty! ");
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Gracz))
            {
                return false;
            }

            Gracz other = (Gracz)obj;
            return Imie.CompareTo(other.Imie) == 0;
        }
        public override string ToString()
        {
            return $"Gracz: {Imie}, Punkty: {Punkty}, PunktySuma: {Plansza.PunktySuma}, PunktyPiechoty: {Plansza.PunktyPiechoty}, PunktyStrzeleckie: {Plansza.PunktyStrzeleckie}, PunktyObleznicze: {Plansza.PunktyObleznicze}, " +
                   $"Liczba kart: {KartyGracza.Count}";
        }
    }
}
