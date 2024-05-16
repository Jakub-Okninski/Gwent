using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty
{
    public class KartaDowodcy : KartaSpecjalna
    {
        public string OpisWlasciwosci {  get; set; }
        public KartaDowodcy(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia)
        {
            OpisWlasciwosci = opisWlasciwosci;  
        }
    }
}
