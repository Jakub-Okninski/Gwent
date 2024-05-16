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
            Application.Run(new Form1(gra));
        }

    

    }
}