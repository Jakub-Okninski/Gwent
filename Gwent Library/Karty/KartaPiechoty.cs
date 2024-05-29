using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    [DataContract]
    public class KartaPiechoty : KartaJednostki, ICloneable
    {
        public override void PolozKarte(Plansza plansza)
        {
            plansza.KartyGraczaWRozgrywce.Remove(this);
            plansza.KartyPiechotyGracza.Add(this);
            Effect?.Invoke(this, plansza);
        }
        public KartaPiechoty() : base()
        {
        }
        public KartaPiechoty(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia, CardEffectDelegate effect) : base(nazwa, sila, kartaBohatera, nazwaZdjecia, effect)
        {
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
