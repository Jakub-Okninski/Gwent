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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gwent_App
{
    public partial class LoginForm : Form
    {
        StartForm startForm;
        private bool playerflaglogin1 = false;
        private bool playerflaglogin2 = false;
        private User p1;
        private User p2;
      
        public LoginForm(StartForm startform)
        {
            startForm = startform;
            InitializeComponent();
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            textBox2.Enabled = true;
            textBox4.Enabled = true;
            label4.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Debug.WriteLine(baseDirectory);
            string[] baseDirectoryTab = baseDirectory.Split("Gwent App");

            string localPath = Path.Combine(baseDirectoryTab[0], "Gwent App", "LocalSources");
            System.Diagnostics.Debug.WriteLine(localPath);
            localPath = localPath + "\\loginimg.jpeg";
            this.BackgroundImage = Image.FromFile(localPath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
           
            FormClosing += RegisterForm_FormClosing;
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            startForm.Close(); 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string playerName = textBox1.Text;
            string playerPassword = textBox3.Text;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(playerPassword))
            {
                label3.Text = "Błędne dane.";
                label3.Visible = true;

                return;
            }

            var player = new User(playerName, playerPassword);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");

            User playerNew = AuthenticatePlayer(player, filePath);
            if (playerNew != null && !playerNew.Equals(new User(textBox2.Text, textBox4.Text)))
            {
                label3.Text = "Oczekujący do rozgrywki...";
                label3.Visible = true;
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                playerflaglogin1 = true;
                label5.Text = "Wygrane: " + playerNew.Winnings + " | Przegrane: " + playerNew.Losing;
                label5.Visible = true;
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
            label3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string playerName = textBox2.Text;
            string playerPassword = textBox4.Text;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(playerPassword))
            {  
                label4.Text = "Błędne dane.";
                label4.Visible = true;
                return;
            }

            var player = new User(playerName, playerPassword);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");
            User playerNew = AuthenticatePlayer(player, filePath);
            if (playerNew != null && !playerNew.Equals(new User(textBox1.Text, textBox3.Text)))
            {
                label4.Text = "Oczekujący do rozgrywki...";
                button2.Enabled = false;
                playerflaglogin2 = true;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                label6.Text = "Wygrane: " + playerNew.Winnings + " | Przegrane: " + playerNew.Losing;
                label6.Visible = true;
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
            label4.Visible = true;
        }
        public static User AuthenticatePlayer(User player, string filePath)
        {
            List<User> players = LoadPlayers(filePath);

            foreach (var playerInFile in players)
            {
                if (player.Equals(playerInFile))
                {
                    return playerInFile;
                }
            }
            return null;
        }
        public static List<User> LoadPlayers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<User>();
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            startForm.Close();
            this.Close();
            Program.InitPlayer(p1, p2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FormClosing -= RegisterForm_FormClosing;
            startForm.Show();
            this.Close();
        }
    }
}
