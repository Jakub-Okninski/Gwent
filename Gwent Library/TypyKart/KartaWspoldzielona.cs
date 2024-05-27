using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class KartaWspoldzielona : KartaSpecjalna
    {
        public Umiejscowienie Umiejscowienie;
        public KartaWspoldzielona(string nazwa, Umiejscowienie umiejscowienie, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
            Umiejscowienie = umiejscowienie;
        }
        public void UstawUmiejscowienie(Umiejscowienie umiejscowienie)
        {
            Umiejscowienie=umiejscowienie;
        }
    }
}
    public enum Umiejscowienie
    {
        Piechoty,
        Lucznika,
        Obleznicza
    }
