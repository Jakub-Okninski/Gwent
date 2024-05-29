using Gwent_Library;
using Gwent_Library.Karty;

namespace Gwent_App
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {

            Talia<Karta> karty = Gra.GenerateCard();
            Talia<Karta> karty2 = Gra.GenerateCard();

            Gracz gracz1 = new Gracz("Jakub", karty);
            Gracz gracz2 = new Gracz("Dawid", karty2);

            System.Diagnostics.Debug.WriteLine("");


            Talia<Karta> kopiaKart = new Talia<Karta>();
            foreach (Karta karta in karty)
            {
                kopiaKart.Add((Karta)karta.Clone());
            }

            Talia<Karta> kopiaKart2 = new Talia<Karta>();
            foreach (Karta karta in karty2)
            {
                kopiaKart2.Add((Karta)karta.Clone());
            }

            Talia<Karta> KartyWrozgrywce1 = new Talia<Karta>();

            Talia<Karta> KartyWrozgrywce2 = new Talia<Karta>();

            int i = 10;
            foreach (Karta k in kopiaKart.Take(i).ToList())
            {

                KartyWrozgrywce1.Add(k);
                kopiaKart.Remove(k);
            }
            foreach (Karta k in kopiaKart2.Take(i).ToList())
            {

                KartyWrozgrywce2.Add(k);
                kopiaKart2.Remove(k);
            }


            gracz1.UstawPlansze(kopiaKart, KartyWrozgrywce1);
            gracz2.UstawPlansze(kopiaKart2, KartyWrozgrywce2);
           
            

            Gra gra = new Gra(gracz1,gracz2);

            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine("Start Rozgrywki");
            Form2 form2 = new Form2();
            Form1 form1 = new Form1(gra, form2);
           form2.setForm(gra, form1);

            Application.Run(form1);

        }
    }
}