using Gwent_Library.Karty;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.KartyDowodcow
{
    public class FoltestZdobywca : KartaDowodcy
    {
        public FoltestZdobywca(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia, "Podwaja siłę wszystkich jednostek oblężniczych (o ile nie ma w tym rzędzie już rogu dowódcy)")
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            gracz1.Plansza.KartySpecjalne.Add(new RogDowodcy("RogDowodcy FoltestZdobywca", "RogDowodcy"));    
        }
    }
}
