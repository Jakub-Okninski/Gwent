using Gwent_Library.Karty;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class KartaJednostki : Karta
    {
        public KartaJednostki(string nazwa, int sila, bool kartaBohatera, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
            DomyslnaSila = sila;
            Sila = sila;
            KartaBohatera = kartaBohatera;
        }
        public bool KartaBohatera { get; set; }
        public int Sila { get; set; }
        public int DomyslnaSila { get; }
        public virtual void DomyslnaWartosc()
        {
            Sila = DomyslnaSila;
        }
    }
}