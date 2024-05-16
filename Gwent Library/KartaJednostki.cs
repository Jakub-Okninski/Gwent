using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public abstract class KartaJednostki : Karta
    {
        public KartaJednostki(string nazwa,int sila,bool kartaBohatera, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
            Sila = sila;
            KartaBohatera = kartaBohatera;
        }
        public bool KartaBohatera {  get; set; }
        public int Sila {  get; set; }
    }
}
