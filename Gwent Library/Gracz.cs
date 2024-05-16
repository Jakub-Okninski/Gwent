using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class Gracz 
    {
        public Gracz(string imie, Talia kartygracza)
        {
            Imie = imie;
            KartyGracza = kartygracza;
            UsunieteKartyGracza = new Talia();
            KartyGraczaWRozgrywce = new Talia();
            KartyPiechotyGracza = new List<IPiechoty>();
            KartyStrzeleckieGracza = new List<IStrzeleckich>();
            KartyOblezniczeGracza = new List<IOblęzniczych>();
            Punkty = 2; 

        }

        public string Imie {  get; set; }   
        public Talia KartyGracza { get; set; }
        public Talia UsunieteKartyGracza { get; set; }
        public Talia KartyGraczaWRozgrywce { get; set; }

        public List<IPiechoty> KartyPiechotyGracza { get; set; }
        public List<IStrzeleckich> KartyStrzeleckieGracza { get; set; }
        public List<IOblęzniczych> KartyOblezniczeGracza { get; set; }

        private int punkty;
        public int Punkty {   get { return punkty; } private set { } }

        private int punktySuma;
        public int PunktySuma { get { return punktySuma; } protected set { } }

        private int punktyPiechoty;
        public int PunktyPiechoty { get { return punktyPiechoty; } protected set { } }
        private int punktyStrzeleckie;
        public int PunktyStrzeleckie { get { return punktyStrzeleckie; } protected set { } }

        private int punktyObleznicze;
        public int PunktyObleznicze { get { return punktyObleznicze; } protected set { } }
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
            return this.Imie.CompareTo(other.Imie) == 0;
        }
    }
}
