using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty
{
    public class UlewnyDeszcz : KartaPogody, ICloneable
    {
        public UlewnyDeszcz(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
        public UlewnyDeszcz() : base()
        {
        }
        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
            System.Diagnostics.Debug.WriteLine("proba deszczu");

            if (gracz1.Plansza.KartySpecjalne.Any(karta => karta is UlewnyDeszcz) || gracz2.Plansza.KartySpecjalne.Any(karta => karta is UlewnyDeszcz))
            {

                System.Diagnostics.Debug.WriteLine("ustawianier deszczu");

                UstawSilePogody(gracz1.Plansza.KartyOblezniczeGracza,1);
                UstawSilePogody(gracz2.Plansza.KartyOblezniczeGracza, 1);
            }
        }
    }
}
