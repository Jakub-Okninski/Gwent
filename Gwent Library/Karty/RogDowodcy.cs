﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class RogDowodcy : KartaWspoldzielona, ICloneable
    {
        public RogDowodcy(string nazwa, Umiejscowienie umiejscowienie, string nazwaZdjecia) : base(nazwa, umiejscowienie, nazwaZdjecia)
        {
        }
        public RogDowodcy() : base()
        {
        }
        public override void PolozKarte(Plansza plansza)
        {
            if (!(plansza.KartySpecjalne.Any(karta => (karta is RogDowodcy k && k.Umiejscowienie == this.Umiejscowienie))))
            {
                System.Diagnostics.Debug.WriteLine("Log: roggggggggggggggggggggStart xD");

                plansza.KartyGraczaWRozgrywce.Remove(this);
                plansza.KartySpecjalne.Add(this);
            }
            else {
                throw new ExceptionRog("Nie możesz położyć ponownie rogu na to samo pole");  
            }
         
        }
        public void Akcja(Gracz gracz, Umiejscowienie umiejscowienie)
        {
            if (umiejscowienie  == Umiejscowienie.Piechoty && gracz.Plansza.KartySpecjalne.Any(karta => (karta is RogDowodcy k && k.Umiejscowienie == Umiejscowienie.Piechoty)))
            {
                gracz.Plansza.KartyPiechotyGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));

            }
            else if (umiejscowienie == Umiejscowienie.Lucznika && gracz.Plansza.KartySpecjalne.Any(karta =>( karta is RogDowodcy k && k.Umiejscowienie == Umiejscowienie.Lucznika)))
            {
                gracz.Plansza.KartyStrzeleckieGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));
            }

            else if(umiejscowienie == Umiejscowienie.Obleznicza && gracz.Plansza.KartySpecjalne.Any(karta => (karta is RogDowodcy k && k.Umiejscowienie == Umiejscowienie.Obleznicza)))
            {
                gracz.Plansza.KartyOblezniczeGracza.ForEach(karta => karta.Sila = (karta.Sila * 2));

            }
        }

        public override string ToString()
        {
            return base.ToString()+ " Umiejscowienie "+ Umiejscowienie;
        }


    }    
}
