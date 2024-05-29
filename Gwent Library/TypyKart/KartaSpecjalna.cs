using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class KartaSpecjalna : Karta, ICloneable
    {
        public KartaSpecjalna(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
        public KartaSpecjalna() : base()
        {
        }
        public override void PolozKarte(Plansza plansza)
        {
            plansza.KartyGraczaWRozgrywce.Remove(this);
            plansza.KartySpecjalne.Add(this);
        }
    }
}
