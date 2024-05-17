﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class KartaDowodcy : KartaSpecjalna, IPolaJednorazowe
    {
        public string OpisWlasciwosci {  get; set; }
        public KartaDowodcy(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia)
        {
            OpisWlasciwosci = opisWlasciwosci;  
        }
    }
}
