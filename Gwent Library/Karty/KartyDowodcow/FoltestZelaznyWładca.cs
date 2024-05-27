using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestZelaznyWładca : KartaDowodcy
    {
        public FoltestZelaznyWładca(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia, "Zniszcz najsilniejszą jednostkę oblężniczą wroga, jeśli suma jednostek oblężniczych jest nie mniejsza niż 10.")
        {
        }

        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
            if (gracz2.Plansza.PunktyObleznicze >= 10)
            {
                int sila = gracz2.Plansza.KartyOblezniczeGracza.Select(karta => karta.Sila).DefaultIfEmpty(0).Max();
                gracz2.Plansza.KartyOblezniczeGracza.RemoveAll(karta => karta.Sila == sila);
            }
            gracz1.Plansza.KartySpecjalne.Remove(this);

        }
    }
}
