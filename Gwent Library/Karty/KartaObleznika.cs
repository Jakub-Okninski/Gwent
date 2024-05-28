using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaObleznika : KartaJednostki, ICloneable
    {
        public override void PolozKarte(Plansza plansza)
        {
            plansza.KartyGraczaWRozgrywce.Remove(this);
            plansza.KartyOblezniczeGracza.Add(this);
            Effect?.Invoke(this, plansza);
        }
        public KartaObleznika(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia, CardEffectDelegate effect) : base(nazwa, sila, kartaBohatera, nazwaZdjecia, effect)
        {
        }
    }
}
