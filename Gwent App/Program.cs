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
           
            
            System.Diagnostics.Debug.WriteLine("Start Rozgrywki");
          //  Form2 form2 = new Form2();
            Form1 form1 = new Form1(gra);
          // form2.SetForm(gra, form1);

            Application.Run(form1);

        }
    }
}