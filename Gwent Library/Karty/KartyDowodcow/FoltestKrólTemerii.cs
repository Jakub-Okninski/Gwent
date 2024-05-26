﻿using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestKrólTemerii : KartaDowodcy
    {
        public FoltestKrólTemerii(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia, "Znajduje kartę gęsta mgła i nią zagrywa")
        {
        }
        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
            gracz1.Plansza.KartySpecjalne.Add(new GestaMgla("Gesta Mgla Krola Temerii", "GestaMgla"));
        }

  
    }
}
