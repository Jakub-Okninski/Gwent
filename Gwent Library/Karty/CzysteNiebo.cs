﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class CzysteNiebo : KartaPogody
    {
        public CzysteNiebo(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            if (gracz1.Plansza.KartySpecjalne.Any(karta => karta is CzysteNiebo) || gracz2.Plansza.KartySpecjalne.Any(karta => karta is CzysteNiebo))
            {
             
                UstawSilePogody(gracz1.Plansza.KartyPiechotyGracza);
                UstawSilePogody(gracz2.Plansza.KartyPiechotyGracza);
                UstawSilePogody(gracz1.Plansza.KartyStrzeleckieGracza);
                UstawSilePogody(gracz2.Plansza.KartyStrzeleckieGracza);
                UstawSilePogody(gracz1.Plansza.KartyOblezniczeGracza);
                UstawSilePogody(gracz2.Plansza.KartyOblezniczeGracza);
                System.Diagnostics.Debug.WriteLine("Log: XDdsadsadadsaDD xD");

                gracz1.Plansza.KartySpecjalne.RemoveAll(karta => karta is KartaPogody);
                gracz2.Plansza.KartySpecjalne.RemoveAll(karta => karta is KartaPogody);
    
            }
        }
    }
}
