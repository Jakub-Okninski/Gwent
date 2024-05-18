using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.KartyDowodcow
{
    public class FoltestDowódcaPółnocy : KartaDowodcy
    {
        public FoltestDowódcaPółnocy(string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia, "Usuwa wszelkie efekty pogodowe")
        {
        }

        public override void WykonajAkcje(Gracz gracz1, Gracz gracz2)
        {
            UstawSilePogody(gracz1.Plansza.KartyPiechotyGracza);
            UstawSilePogody(gracz2.Plansza.KartyPiechotyGracza);
            UstawSilePogody(gracz1.Plansza.KartyStrzeleckieGracza);
            UstawSilePogody(gracz2.Plansza.KartyStrzeleckieGracza);
            UstawSilePogody(gracz1.Plansza.KartyOblezniczeGracza);
            UstawSilePogody(gracz2.Plansza.KartyOblezniczeGracza);

            gracz1.Plansza.KartySpecjalne.RemoveAll(karta => karta is KartaPogody);
            gracz2.Plansza.KartySpecjalne.RemoveAll(karta => karta is KartaPogody);
        }
        protected void UstawSilePogody<T>(Talia<T> lista) where T : KartaJednostki
        {
            lista.Where(karta => !karta.KartaBohatera)
               .ToList()
               .ForEach(karta => karta.Sila = karta.DomyslnaSila);
        }
    }
}
