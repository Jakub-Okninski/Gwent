﻿using Gwent_Library.Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public abstract class KartaPogody : KartaGlobalna, ICloneable
    {
        public KartaPogody(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
        public KartaPogody() : base()
        {
        }
        protected void UstawSilePogody<T>(Talia<T> lista, int sila) where T : KartaJednostki 
        {

            lista.Where(karta => !karta.KartaBohatera)
               .ToList()
               .ForEach(karta => karta.Sila = sila);         
        }

        protected void UstawSilePogody<T>(Talia<T> lista) where T : KartaJednostki
        {
            lista.Where(karta => !karta.KartaBohatera)
               .ToList()
               .ForEach(karta => karta.Sila = karta.DomyslnaSila);
        }

     

    }
}
