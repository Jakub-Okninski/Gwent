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
        StartForm startForm;
        public RegisterForm(StartForm startform)
        {
            InitializeComponent();
            startForm = startform;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Debug.WriteLine(baseDirectory);
            string[] baseDirectoryTab = baseDirectory.Split("Gwent App");

            string localPath = Path.Combine(baseDirectoryTab[0], "Gwent App", "LocalSources");
            System.Diagnostics.Debug.WriteLine(localPath);
            localPath = localPath + "\\imgregister.jpg";
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
            string playerPassword = textBox2.Text;

            if (string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(playerPassword))
            {
                MessageBox.Show("Please enter both name and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var newPlayer = new User(playerName, playerPassword);

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.json");
            RegisterPlayer(newPlayer, filePath);

            textBox1.Text = "";
            textBox2.Text = "";
        }
        public static void RegisterPlayer(User newPlayer, string filePath)
        {
            List<User> players = LoadPlayers(filePath);

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
        public static List<User> LoadPlayers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<User>();
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
        }

        public static bool PlayerExists(User player, List<User> players)
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
        public static void SavePlayers(List<User> players, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(players, options);
            File.WriteAllText(filePath, jsonString);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormClosing -= RegisterForm_FormClosing;
            startForm.Show();
            this.Close();
        }     
    }
}
