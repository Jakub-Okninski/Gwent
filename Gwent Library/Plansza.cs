using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class Plansza
    {
        public Plansza(Talia<Karta> talia, Talia<Karta> taliaWrozgrywce)
        {
            WszystkieKartyGracza = talia;
            KartyGraczaWRozgrywce = taliaWrozgrywce;

            KartyPiechotyGracza = new Talia<KartaPiechoty>();
            KartyStrzeleckieGracza = new Talia<KartaLucznika>();
            KartyOblezniczeGracza = new Talia<KartaObleznika>();

            KartySpecjalne = new Talia<KartaSpecjalna>();
            PunktySuma = 0;

            PunktyPiechoty = 0;
            PunktyStrzeleckie = 0;
            PunktyObleznicze = 0;
        }
        public Talia<Karta> WszystkieKartyGracza { get; set; }
        public Talia<Karta> KartyGraczaWRozgrywce { get; set; }
        public Talia<KartaPiechoty> KartyPiechotyGracza { get; set; }
        public Talia<KartaLucznika> KartyStrzeleckieGracza { get; set; }
        public Talia<KartaObleznika> KartyOblezniczeGracza { get; set; }
        public Talia<KartaSpecjalna> KartySpecjalne { get; set; }

        public int PunktySuma { get; set; }
        public int PunktyPiechoty { get; set; }
        public int PunktyStrzeleckie { get; set; }
        public int PunktyObleznicze { get; set; }

        public Talia<KartaJednostki> listaKartJednostek()
        {
            Talia<KartaJednostki> lista = new Talia<KartaJednostki>();
            lista.AddRange(KartyPiechotyGracza);
            lista.AddRange(KartyStrzeleckieGracza);
            lista.AddRange(KartyOblezniczeGracza);
            return lista;
        }
        public void ResetujPanel(){
            PunktySuma = 0;
            PunktyPiechoty = 0;
            PunktyStrzeleckie = 0;
            PunktyObleznicze = 0;
            KartyPiechotyGracza.Clear();
            KartyStrzeleckieGracza.Clear();
            KartyOblezniczeGracza.Clear();
            KartySpecjalne.Clear();
     
        }   
    }
}
