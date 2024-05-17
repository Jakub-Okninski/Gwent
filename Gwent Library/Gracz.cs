using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library
{
    public class Gracz 
    {
        public Gracz(string imie, Talia<Karta> kartygracza)
        {
            Imie = imie;
            KartyGracza = kartygracza;
            KartyUsunieteGracza = new Talia<Karta>();
            KartyGraczaWRozgrywce = new Talia<Karta>();
            KartyPiechotyGracza = new Talia<IPolaPiechoty>();
            KartyStrzeleckieGracza = new Talia<IPolaStrzeleckie>();
            KartyOblezniczeGracza = new Talia<IPolaOblęznicze>();
            KartyJedenorazowe = new Talia<IPolaJednorazowe>();
            KartyJednostekGracza = new Talia<KartaJednostki>();

            Punkty = 2;
            PunktySuma = 0;
            PunktyPiechoty = 0;
            PunktyStrzeleckie = 0;
            PunktyObleznicze = 0;


    }


        public Talia<KartaJednostki> KartyJednoskiGracza()
        {

            KartyJednostekGracza.Clear();
            KartyJednostekGracza.AddRange(KartyOblezniczeGracza.OfType<KartaJednostki>());
            KartyJednostekGracza.AddRange(KartyPiechotyGracza.OfType<KartaJednostki>());
            KartyJednostekGracza.AddRange(KartyStrzeleckieGracza.OfType<KartaJednostki>());

            return KartyJednostekGracza;
        }

        public override string ToString()
        {
            return $"Gracz: {Imie}, Punkty: {Punkty}, PunktySuma: {PunktySuma}, PunktyPiechoty: {PunktyPiechoty}, PunktyStrzeleckie: {PunktyStrzeleckie}, PunktyObleznicze: {PunktyObleznicze}, " +
                   $"Liczba kart: {KartyGracza.Count}";
        }


        public string Imie {  get; set; }   
        public Talia<Karta> KartyGracza { get; set; }


        public Talia<Karta> KartyGraczaWRozgrywce { get; set; }
        public Talia<Karta> KartyUsunieteGracza { get; set; }
        public Talia<KartaJednostki> KartyJednostekGracza { get; set; }

        public Talia<IPolaPiechoty> KartyPiechotyGracza { get; set; }
        public Talia<IPolaStrzeleckie> KartyStrzeleckieGracza { get; set; }
        public Talia<IPolaOblęznicze> KartyOblezniczeGracza { get; set; }

        public Talia<IPolaJednorazowe> KartyJedenorazowe { get; set; }


    public int Punkty {  get; set; }
        public int PunktySuma { get; set; }
        public int PunktyPiechoty { get; set; }
        public int PunktyStrzeleckie { get; set; }
        public int PunktyObleznicze { get; set; }

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
    }
}
