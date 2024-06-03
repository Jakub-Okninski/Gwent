using Gwent_Library;
using Gwent_Library.Karty;
using NAudio.CoreAudioApi;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Media;

namespace Gwent_App
{
    internal static class Program
    {
        
        public static Player player1 = null;
        public static Player player2 = null;

        public static User u1 = null;
        public static User u2 = null;

        public static bool flag = true;
        [STAThread]
        static void Main()
        {

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] baseDirectoryTab = baseDirectory.Split("Gwent App");
            System.Diagnostics.Debug.WriteLine(baseDirectoryTab[0]);
            string localPath = Path.Combine(baseDirectoryTab[0], "Gwent App", "LocalSources");


            while (flag)
            {
                WaveOutEvent outputDevice = new WaveOutEvent();
                AudioFileReader audioFile = new AudioFileReader(localPath + "\\main.wav");
                VolumeSampleProvider volumeProvider = new VolumeSampleProvider(audioFile.ToSampleProvider());
                volumeProvider.Volume = 0.08f;
                outputDevice.Init(volumeProvider);
                outputDevice.Play();

                StartForm startFrom = new StartForm();
                Application.Run(startFrom);

                outputDevice.Stop();

                if (player1 != null && player2 != null)
                {


                    Game game = new Game(player1, player2);

                    System.Diagnostics.Debug.WriteLine("Start Rozgrywki");
                    Form2 form2 = new Form2();
                    Form1 form1 = new Form1(game, form2);
                    form2.setForm(game, form1);

                    SoundPlayer soundPlayer = new SoundPlayer(localPath + "\\intro.wav");
                    soundPlayer.Play();
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
                            UpdateUser(users, u1);
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
                    catch (EndGameException ex)
                    {

                        MessageBox.Show(ex.Message, "Wyst¹pi³ B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    player1 = null;
                    player2 = null;

                }

            }

           
        }
        public static void InitPlayer(User user1, User user2)
        {
            u1 = user1;
            u2 = user2;

            Deck<Card> cards = Game.GenerateRandomCard(Game.GenerateCommanderCard(),1);
            Deck<Card> cards2 = Game.GenerateRandomCard(Game.GenerateCommanderCard(),1);
            cards.AddRange(Game.GenerateRandomCard(Game.GenerateCard(), 15));
            cards2.AddRange(Game.GenerateRandomCard(Game.GenerateCard(), 15));
         

            player1 = new Player(user1.Name, Game.GenerateCard());
            player2 = new Player(user2.Name, Game.GenerateCard());



            Deck<Card> restCards = new Deck<Card>();
            Deck<Card> restCards2 = new Deck<Card>();


            foreach (var card in player1.AllCardsPlayer)
            {
                Card clonedCard = (Card)card.Clone();
                restCards.Add(clonedCard);
            }

            foreach (var card in player1.AllCardsPlayer)
            {
                Card clonedCard = (Card)card.Clone();
                restCards2.Add(clonedCard);
            }


            Deck<Card> CardInGames = Game.GenerateRandomCard(restCards, 15);
            Deck<Card> CardInGames2 = Game.GenerateRandomCard(restCards2, 15);
            CardInGames.AddRange(Game.GenerateRandomCard(Game.GenerateCommanderCard(), 1));
            CardInGames2.AddRange(Game.GenerateRandomCard(Game.GenerateCommanderCard(), 1));




            player1.SetBoard(restCards, CardInGames);
            player2.SetBoard(restCards2, CardInGames2);
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