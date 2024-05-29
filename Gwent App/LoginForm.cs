using Gwent_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gwent_App
{
    public partial class LoginForm : Form
    {
        StartForm startForm;
        public LoginForm(StartForm startform)
        {
            startForm = startform;
            InitializeComponent();
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            textBox2.Enabled = true;
            textBox4.Enabled = true;
        }
        private bool playerflaglogin1 = false;
        private bool playerflaglogin2 = false;
        private Player p1;
        private Player p2;
        private void button1_Click(object sender, EventArgs e)
        {
            string playerName = textBox1.Text;
            string playerPassword = textBox3.Text;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(playerPassword))
            {
                label3.Text = "Błędne dane.";
                return;
            }

            var player = new Player(playerName, playerPassword);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");

            Player playerNew = AuthenticatePlayer(player, filePath);
            if (playerNew != null && !playerNew.Equals(new Player(textBox2.Text, textBox4.Text)))
            {
                label3.Text = "Oczekujący do rozgrywki...";
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                playerflaglogin1 = true;
                label5.Text = "Wygrane: " + playerNew.Winnings + " | Przegrane: " + playerNew.Losing;
                p1 = playerNew;
                if (playerflaglogin2)
                {
                    button3.Enabled = true;
                }

            }
            else
            {
                label3.Text = "Błędne dane.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string playerName = textBox2.Text;
            string playerPassword = textBox4.Text;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(playerPassword))
            {
                label4.Text = "Błędne dane.";
                return;
            }

            var player = new Player(playerName, playerPassword);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");
            Player playerNew = AuthenticatePlayer(player, filePath);
            if (playerNew != null && !playerNew.Equals(new Player(textBox1.Text, textBox3.Text)))
            {
                label4.Text = "Oczekujący do rozgrywki...";
                button2.Enabled = false;
                playerflaglogin2 = true;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                label6.Text = "Wygrane: " + playerNew.Winnings + " | Przegrane: " + playerNew.Losing;
                p2 = playerNew;
                if (playerflaglogin1)
                {
                    button3.Enabled = true;
                }
            }
            else
            {
                label4.Text = "Błędne dane.";
            }
        }



        public static Player AuthenticatePlayer(Player player, string filePath)
        {
            List<Player> players = LoadPlayers(filePath);

            foreach (var playerInFile in players)
            {
                if (player.Equals(playerInFile))
                {
                    return playerInFile;
                }
            }
            return null;
        }

        public static List<Player> LoadPlayers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Player>();
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Player>>(jsonString) ?? new List<Player>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            startForm.Close();
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            startForm.Show();

        }
    }
}
