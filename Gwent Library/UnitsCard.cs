using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    //jednsotki
    abstract class UnitsCard : Card
    {
        protected UnitsCard(string nazwa, string nazwaZdjecia, int sila) : base(nazwa, nazwaZdjecia)
        {
            Sila=sila;
        }

        public int Sila {  get; set; }
    }
}
