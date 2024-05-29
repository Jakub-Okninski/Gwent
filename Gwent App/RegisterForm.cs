using Gwent_Library;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.Karty;
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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string playerName = textBox1.Text;
            string playerPassword = textBox2.Text;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(playerPassword))
            {
                MessageBox.Show("Please enter both name and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newPlayer = new Player(playerName, playerPassword);

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");
            RegisterPlayer(newPlayer, filePath);

            textBox1.Text = "";
            textBox2.Text = "";
        }

        public static void RegisterPlayer(Player newPlayer, string filePath)
        {
            List<Player> players = LoadPlayers(filePath);

            if (PlayerExists(newPlayer, players))
            {
                MessageBox.Show("Player already exists. Registration failed.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                players.Add(newPlayer);
                SavePlayers(players, filePath);
                MessageBox.Show("Player registered successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        public static bool PlayerExists(Player player, List<Player> players)
        {
            foreach (var playerInFile in players)
            {
                if (player.Equals(playerInFile))
                {
                    return true;
                }
            }
            return false;
        }

        public static void SavePlayers(List<Player> players, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(players, options);
            File.WriteAllText(filePath, jsonString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();

            // Pokaż formularz główny
            loginForm.Show();
        }
    }
}
