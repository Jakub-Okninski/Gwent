using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class Pozoga : KartaGlobalna
    {
        public Pozoga(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }

        public override void AkcjaGlobalna(Gracz gracz1, Gracz gracz2)
        {
            if (gracz1.Plansza.KartySpecjalne.Any(karta => karta is Pozoga) 
                || gracz2.Plansza.KartySpecjalne.Any(karta => karta is Pozoga))
            {
                var wszystkieKarty = new List<KartaJednostki>();

                wszystkieKarty.AddRange(gracz1.Plansza.listaKartJednostek());
                wszystkieKarty.AddRange(gracz2.Plansza.listaKartJednostek());
                int maksymalnaSila = wszystkieKarty.Any() ? wszystkieKarty.Max(karta => karta.Sila) : 0;

                UsunKartyOPodanejSile(gracz1.Plansza.KartyPiechotyGracza, maksymalnaSila);
                UsunKartyOPodanejSile(gracz2.Plansza.KartyPiechotyGracza, maksymalnaSila);

                UsunKartyOPodanejSile(gracz1.Plansza.KartyStrzeleckieGracza, maksymalnaSila);
                UsunKartyOPodanejSile(gracz2.Plansza.KartyStrzeleckieGracza, maksymalnaSila);

                UsunKartyOPodanejSile(gracz1.Plansza.KartyOblezniczeGracza, maksymalnaSila);
                UsunKartyOPodanejSile(gracz2.Plansza.KartyOblezniczeGracza, maksymalnaSila);

                gracz2.Plansza.KartySpecjalne.RemoveAll(karta => karta is Pozoga);
                gracz1.Plansza.KartySpecjalne.RemoveAll(karta => karta is Pozoga);
            }
        }


        private void UsunKartyOPodanejSile<T>(Talia<T> lista, int sila) where T : KartaJednostki
        {
            lista.RemoveAll(karta => karta.Sila == sila);
        }
    }
}
