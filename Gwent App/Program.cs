using Gwent_Library;
using Gwent_Library.Karty;

namespace Gwent_App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {


            StartForm startFrom = new StartForm();
            Application.Run(startFrom);



            Deck<Card> karty = Game.GenerateCard();
            Deck<Card> karty2 = Game.GenerateCard();

            Player gracz1 = new Player("Jakub", karty);
            Player gracz2 = new Player("Dawid", karty2);

            System.Diagnostics.Debug.WriteLine("");


            Deck<Card> kopiaKart = new Deck<Card>();
            foreach (Card karta in karty)
            {
                kopiaKart.Add((Card)karta.Clone());
            }

            Deck<Card> kopiaKart2 = new Deck<Card>();
            foreach (Card karta in karty2)
            {
                kopiaKart2.Add((Card)karta.Clone());
            }

            Deck<Card> KartyWrozgrywce1 = new Deck<Card>();

            Deck<Card> KartyWrozgrywce2 = new Deck<Card>();

            int i = 10;
            foreach (Card k in kopiaKart.Take(i).ToList())
            {

                KartyWrozgrywce1.Add(k);
                kopiaKart.Remove(k);
            }
            foreach (Card k in kopiaKart2.Take(i).ToList())
            {

                KartyWrozgrywce2.Add(k);
                kopiaKart2.Remove(k);
            }


            gracz1.SetBoard(kopiaKart, KartyWrozgrywce1);
            gracz2.SetBoard(kopiaKart2, KartyWrozgrywce2);



            Game gra = new Game(gracz1, gracz2);

            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine("Start Rozgrywki");
            Form2 form2 = new Form2();
            Form1 form1 = new Form1(gra, form2);
            form2.setForm(gra, form1);
        
            Application.Run(form1);





        }

        public static void InitPlayer(User player1, User player2)
        {

        }
    }
}