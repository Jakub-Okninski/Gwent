using Gwent_Library;
using Gwent_Library.Karty;
using NAudio.CoreAudioApi;

namespace Gwent_App
{
    internal static class Program
    {
        
        public static Player player1 = null;
        public static Player player2 = null;
        [STAThread]
        static void Main()
        {

            

            StartForm startFrom = new StartForm();
            Application.Run(startFrom);

            if (player1 != null && player2 != null) {

 
                Game gra = new Game(player1, player2);

               

                System.Diagnostics.Debug.WriteLine("Start Rozgrywki");
                Form2 form2 = new Form2();
                Form1 form1 = new Form1(gra, form2);
                form2.setForm(gra, form1);

                Application.Run(form1);

                player1 = null;
                player2 = null;


    }
        }
        public static void InitPlayer(User user1, User user2)
        {
            Deck<Card> karty = Game.GenerateCard();
            Deck<Card> karty2 = Game.GenerateCard();

            player1 = new Player(user1.Name, karty);
            player2 = new Player(user2.Name, karty2);

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


            player1.SetBoard(kopiaKart, KartyWrozgrywce1);
            player2.SetBoard(kopiaKart2, KartyWrozgrywce2);
        }


    }
}