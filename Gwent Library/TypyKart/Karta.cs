using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public abstract class Karta : ICloneable
    {
        public Karta(string nazwa,  string nazwaZdjecia)
        {
            Nazwa = nazwa;
            NazwaZdjecia = nazwaZdjecia;
        }
        public string Nazwa {  get; set; }
        public string NazwaZdjecia { get; set; }
        public abstract void PolozKarte(Plansza plansza);

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return $"Nazwa: {Nazwa}, NazwaZdjecia: {NazwaZdjecia}";
        }

    }
}