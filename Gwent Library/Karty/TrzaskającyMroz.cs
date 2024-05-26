using System;
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

        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {

            if (gracz1.Plansza.KartySpecjalne.Any(karta => karta is TrzaskającyMroz) ||gracz2.Plansza.KartySpecjalne.Any(karta => karta is TrzaskającyMroz))
            {
                UstawSilePogody(gracz1.Plansza.KartyPiechotyGracza, 1);
                UstawSilePogody(gracz2.Plansza.KartyPiechotyGracza, 1);             
            }
        }
    }
}
