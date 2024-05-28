using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestDowódcaPółnocy : KartaDowodcy, ICloneable
    {
        public FoltestDowódcaPółnocy(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia, "Usuwa wszelkie efekty pogodowe")
        {
        }

        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
     

            gracz1.Plansza.KartySpecjalne.RemoveAll(karta => karta is KartaPogody);
            gracz2.Plansza.KartySpecjalne.RemoveAll(karta => karta is KartaPogody);

            gracz1.Plansza.KartySpecjalne.Add(new CzysteNiebo("Czyste Niebo Krola Temerii", "CzysteNiebo"));

            gracz1.Plansza.KartySpecjalne.Remove(this);
        }

      

       
    }
}
