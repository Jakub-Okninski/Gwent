using Gwent_Library;
using Gwent_Library.Karty;
using NAudio.CoreAudioApi;

namespace Gwent_App
{
    internal static class Program
    {
        
        public static Player player1 = null;
        public static Player player2 = null;

        public static User u1 = null;
        public static User u2 = null;
        [STAThread]
        static void Main()
        {

            StartForm startFrom = new StartForm();
            Application.Run(startFrom);

            if (player1 != null && player2 != null) {

 
                Game game = new Game(player1, player2);

                System.Diagnostics.Debug.WriteLine("Start Rozgrywki");
                Form2 form2 = new Form2();
                Form1 form1 = new Form1(game, form2);
                form2.setForm(game, form1);

                Application.Run(form1);
                try
                {
                    Player winner = game.ReturnWinner();

                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");
                    List<User> users = RegisterForm.LoadPlayers(filePath);

                    if (winner == player1)
                    {
                        u1.Winnings++;
                        u2.Losing++;
                        UpdateUser(users,u1);
                        UpdateUser(users, u2);
                    }
                    if (winner == player2)
                    {
                        u2.Winnings++;
                        u1.Losing++;
                        UpdateUser(users, u1);
                        UpdateUser(users, u2);
                    }
                    RegisterForm.SavePlayers(users, filePath);

           
                }
                catch (EndGameException ex) {

                    MessageBox.Show(ex.Message, "Wyst¹pi³ B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                player1 = null;
                player2 = null;

    }
        }
        public static void InitPlayer(User user1, User user2)
        {
            u1 = user1;
            u2 = user2;
            Deck<Card> cards = Game.GenerateCard();
            Deck<Card> cards2 = Game.GenerateCard();

            player1 = new Player(user1.Name, cards);
            player2 = new Player(user2.Name, cards2);



            Deck<Card> copyCards = new Deck<Card>();
            foreach (Card card in cards)
            {
                copyCards.Add((Card)card.Clone());
            }

            Deck<Card> copyCards2 = new Deck<Card>();
            foreach (Card card in cards2)
            {
                copyCards2.Add((Card)card.Clone());
            }

            Deck<Card> CardInGames = new Deck<Card>();

            Deck<Card> CardInGames2 = new Deck<Card>();

            int i = 10;
            foreach (Card c in copyCards.Take(i).ToList())
            {

                CardInGames.Add(c);
                copyCards.Remove(c);
            }
            foreach (Card c in copyCards2.Take(i).ToList())
            {

                CardInGames2.Add(c);
                copyCards2.Remove(c);
            }


            player1.SetBoard(copyCards, CardInGames);
            player2.SetBoard(copyCards2, CardInGames2);
        }

        public static void UpdateUser(List<User>users, User user)
        {   
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Name == user.Name)
                {
                    users[i] = user;
                    break;
                }
            }
        }
    }
}