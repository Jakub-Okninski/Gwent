﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class TrzaskającyMroz : KartaPogody
    {
        public TrzaskającyMroz (string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            System.Diagnostics.Debug.WriteLine("Log: XDDD xD");

            if ((!gracz1.Plansza.KartySpecjalne.Any(karta => karta is CzysteNiebo) || !gracz2.Plansza.KartySpecjalne.Any(karta => karta is CzysteNiebo)) &&
              (gracz1.Plansza.KartySpecjalne.Any(karta => karta is TrzaskającyMroz) || gracz2.Plansza.KartySpecjalne.Any(karta => karta is TrzaskającyMroz)))
            {

                System.Diagnostics.Debug.WriteLine("Log: Spogodaaaaaaaaaaaaaaaaaaaaaaaaaaart xD");

                UstawSilePogody(gracz1.Plansza.KartyPiechotyGracza, 1);
                UstawSilePogody(gracz2.Plansza.KartyPiechotyGracza, 1);             
            }
        }
    }
}
