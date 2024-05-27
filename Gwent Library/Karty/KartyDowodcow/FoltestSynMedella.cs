using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestSynMedella : KartaDowodcy
    {
        public FoltestSynMedella(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia, "Zniszcz najsilniejszą jednostkę strzelecką wroga, jeśli suma jednostek strzeleckich jest nie mniejsza niż 10.")
        {
        }

        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
            if (gracz2.Plansza.PunktyStrzeleckie >= 10)
            {
                int sila = gracz2.Plansza.KartyStrzeleckieGracza.Select(karta => karta.Sila).DefaultIfEmpty(0).Max();
                gracz2.Plansza.KartyStrzeleckieGracza.RemoveAll(karta => karta.Sila == sila);
            }
            gracz1.Plansza.KartySpecjalne.Remove(this);
        }
    }
}
