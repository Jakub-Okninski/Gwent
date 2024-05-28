using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestZdobywca : KartaDowodcy
    {
        public FoltestZdobywca(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia, "Podwój siłę wszystkich swoich jednostek oblężniczych (chyba, że został użyty już róg dowódcy).")
        {
        }

        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
           if(!(gracz1.Plansza.KartySpecjalne.Any(karta=>karta is RogDowodcy Rg && Rg.Umiejscowienie == Umiejscowienie.Obleznicza)))
            {
               RogDowodcy rog =  new RogDowodcy("Rog Dowodcy Foltesta", Umiejscowienie.Obleznicza, "Rog");
               rog.PolozKarte(gracz1.Plansza);
            }
            else
            {
                throw new ExceptionDowodca("Został użyty już róg dowódcy.");
            }
            gracz1.Plansza.KartySpecjalne.Remove(this);

        }
    }
}
