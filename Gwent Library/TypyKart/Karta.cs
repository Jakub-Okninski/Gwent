using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public abstract class Karta
    {
        public Karta(string nazwa,  string nazwaZdjecia)
        {
            Nazwa = nazwa;
            NazwaZdjecia = nazwaZdjecia;
        }
        public string Nazwa {  get; set; }
        public string NazwaZdjecia { get; set; }
    } 
}