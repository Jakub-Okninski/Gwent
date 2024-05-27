using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaLucznika : KartaJednostki
    {

        public override void PolozKarte(Plansza plansza)
        {
            plansza.KartyGraczaWRozgrywce.Remove(this);
            plansza.KartyStrzeleckieGracza.Add(this);
            Effect?.Invoke(this, plansza);
        }

        public KartaLucznika(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia, CardEffectDelegate effect) : base(nazwa, sila, kartaBohatera, nazwaZdjecia, effect)
        {
        }
    }
}
