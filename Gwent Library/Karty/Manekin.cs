using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty
{
    public class Manekin : KartaSpecjalna , IPiechoty, IStrzeleckich, IOblęzniczych
    {
        public Manekin(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
    }
}
