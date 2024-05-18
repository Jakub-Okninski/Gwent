using Gwent_Library.Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.KartyDowodcow
{
    public class FoltestKrólTemerii : KartaDowodcy
    {
        public FoltestKrólTemerii(string nazwa, string nazwaZdjecia, string opisWlasciwosci) : base(nazwa, nazwaZdjecia, "Znajduje kartę gęsta mgła i nią zagrywa")
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            gracz1.Plansza.KartySpecjalne.Add(new GestaMgla("GestaMglaKrolaTemerii", "GestaMgla"));
        }
    }
}
