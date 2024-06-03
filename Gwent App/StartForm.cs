using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gwent_App
{
    public partial class StartForm : Form
    {


        public StartForm()
        {
            InitializeComponent();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Debug.WriteLine(baseDirectory);
            string[] baseDirectoryTab = baseDirectory.Split("Gwent App");

            string localPath = Path.Combine(baseDirectoryTab[0], "Gwent App", "LocalSources");
            System.Diagnostics.Debug.WriteLine(localPath);
            localPath = localPath + "\\imgStart.jpg";
            this.BackgroundImage = Image.FromFile(localPath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            LoginForm loginForm = new LoginForm(this); ;
            loginForm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm RegisterForm = new RegisterForm(this); ;
            RegisterForm.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.flag = false;
            this.Close();
        }
    }
}
