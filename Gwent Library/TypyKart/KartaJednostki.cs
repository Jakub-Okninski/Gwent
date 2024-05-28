using Gwent_Library.Karty;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public delegate void CardEffectDelegate (KartaJednostki karta, Plansza plansza);

    public abstract class KartaJednostki : Karta, ICloneable
    {
        public KartaJednostki(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia, CardEffectDelegate effect) : base(nazwa, nazwaZdjecia)
        {
            DomyslnaSila = sila;
            Default = sila;
            Sila = sila;
            KartaBohatera = kartaBohatera;
            Effect = effect;
        }
        public CardEffectDelegate Effect { get; set; }
        public bool KartaBohatera { get; set; }
        public int Sila { get; set; }
        public int DomyslnaSila { get; set; }
        public int Default { get; }
        public virtual void DomyslnaWartosc()
        {
            Sila = DomyslnaSila; 
        }
        public override string ToString()
        {
            return base.ToString() + $"Nazwa: {Nazwa}, Siła: {Sila}, Karta Bohatera: {KartaBohatera} , Default: {Default} , DomyslnaSila: {DomyslnaSila}";
        }

    }
}