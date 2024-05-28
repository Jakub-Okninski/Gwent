using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.Karty
{
    public class Manekin : KartaWspoldzielona, ICloneable
    {
        public Manekin(string nazwa, Umiejscowienie umiejscowienie, string nazwaZdjecia) : base(nazwa, umiejscowienie, nazwaZdjecia)
        {
        }

        public void Akcja<T> (Gracz gracz, T kartaOld) where T : KartaJednostki
        {
            if (gracz.Plansza.KartyPiechotyGracza.Any(karta =>  karta is KartaPiechoty kJ && kJ == kartaOld)&& this.Umiejscowienie==Umiejscowienie.Piechoty)
            {
                gracz.Plansza.KartyPiechotyGracza.RemoveAll(karta=> karta== kartaOld);
                gracz.Plansza.KartyGraczaWRozgrywce.Add(kartaOld);
            }
            else if(gracz.Plansza.KartyStrzeleckieGracza.Any(karta => karta is KartaLucznika kJ && kJ == kartaOld) && this.Umiejscowienie == Umiejscowienie.Lucznika)
            {
                gracz.Plansza.KartyStrzeleckieGracza.RemoveAll(karta => karta == kartaOld);
                gracz.Plansza.KartyGraczaWRozgrywce.Add(kartaOld);

            }
            else if (gracz.Plansza.KartyOblezniczeGracza.Any(karta => karta is KartaObleznika kJ && kJ == kartaOld) && this.Umiejscowienie == Umiejscowienie.Obleznicza)
            {
                gracz.Plansza.KartyOblezniczeGracza.RemoveAll(karta => karta == kartaOld);
                gracz.Plansza.KartyGraczaWRozgrywce.Add(kartaOld);

            }
        }
    }
}
