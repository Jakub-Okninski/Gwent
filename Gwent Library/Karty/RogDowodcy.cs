using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty
{
    public class RogDowodcy : KartaSpecjalna, IPiechoty, IStrzeleckich, IOblęzniczych
    {
        public RogDowodcy (string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
    }
}
