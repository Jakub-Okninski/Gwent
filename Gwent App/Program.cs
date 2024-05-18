using Gwent_Library;
using Gwent_Library.Karty;

namespace Gwent_App
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Gracz gracz1 = new Gracz("Jakub", Gra.GenerateCard());
            Gracz gracz2 = new Gracz("Dawid", Gra.GenerateCard());
            Gra gra = new Gra(gracz1,gracz2);


            ApplicationConfiguration.Initialize();
            // Uruchomienie formy 1 w osobnym w¹tku
            Form2 f2 =  new Form2(gra);

            Thread thread1 = new Thread(() => Application.Run(new Form2(gra)));
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();

            // Uruchomienie formy 2 w g³ównym w¹tku
            Application.Run(new Form1(gra,f2));
        }
    }
}