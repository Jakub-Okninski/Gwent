using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class UlewnyDeszcz : KartaPogody
    {
        public UlewnyDeszcz(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            if ((!gracz1.Plansza.KartySpecjalne.Any(karta => karta is CzysteNiebo) && 
                !gracz2.Plansza.KartySpecjalne.Any(karta => karta is CzysteNiebo)) &&
                (gracz1.Plansza.KartySpecjalne.Any(karta => karta is UlewnyDeszcz) || 
                gracz2.Plansza.KartySpecjalne.Any(karta => karta is UlewnyDeszcz)))
            {       
                UstawSilePogody(gracz1.Plansza.KartyOblezniczeGracza, 1);
                UstawSilePogody(gracz2.Plansza.KartyOblezniczeGracza, 1);
            }
        }
    }
}
