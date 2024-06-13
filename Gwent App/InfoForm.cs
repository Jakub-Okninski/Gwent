using Gwent_Library;
using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
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
    public partial class InfoForm : Form
    {
        public Card card;
        public InfoForm(Card c)
        {
            InitializeComponent();
            label4.Hide();
            label5.Hide();
            label1.Hide();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Debug.WriteLine(baseDirectory);
            string[] baseDirectoryTab = baseDirectory.Split("Gwent App");
            string localPath = Path.Combine(baseDirectoryTab[0], "Gwent App", "LocalSources");  
            this.BackgroundImage = Image.FromFile(localPath + "\\tloInfo.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            string localPath2 = localPath;
            card = c;
            if (card is KartaLucznika)
            {
                localPath2 = localPath + "\\infoluk.jpg";
            }
            else if (card is KartaPiechoty)
            {
                localPath2 = localPath + "\\infomiecz.jpg";
            }
            else if (card is KartaObleznika)
            {
                localPath2 = localPath + "\\infobalista.jpg";
            }
            else if (card is CardWarrior)
            {
                localPath2 = localPath + "\\infokorona.jpg";
            }
            else if (card is RogDowodcy)
            {
                localPath2 = localPath + "\\inforog.jpg";
            }
            else if (card is Manekin)
            {
                localPath2 = localPath + "\\infomanekin.jpg";
            }
            else
            {
                localPath2 = localPath + "\\inforozdzka.jpg";
            }

            try
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Image = Image.FromFile(localPath2);

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(localPath + "\\" + c.ImgName + ".jpg");
            }
            catch (Exception e){ }
         
            label6.Text = c.Description + "";
            if (card is CardWarrior CW)
            {
                label1.Show();
                label1.Text = CW.ForceDefault + "";
                if (CW.Effect == CardEffects.Braterstwo)
                {
                    label4.Text = "Braterstwo: Użycie figury z tym efektem automatycznie wyciąga z kart dostępnych pod ręką i talii wszystkie jednostki tego samego rodzaju, by natychmiast je zagrać.";
                    label4.Show();
                    pictureBox1.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);
                    };
                }
                else if (CW.Effect == CardEffects.Wiez)
                {
                    label4.Text = "Więź: Figura podwaja swoją wartość wtedy, gdy obok niej, w tym samym rzędzie, znajdzie się analogiczna karta o tej samej nazwie.";
                    label4.Show();
                    pictureBox1.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, Color.Green, ButtonBorderStyle.Solid);
                    };
                }
                else if (CW.Effect == CardEffects.WysokieMorale)
                {
                    label4.Text = "Wysokie Morale: Figura podniesienie wartości kart w tym samym rzędzie, tego samego rodzaju o ich sumę";
                    label4.Show();
                    pictureBox1.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
                    };
                }

                if (CW.IsHero)
                {
                    label5.Text = "Karta Bohatera odporna na efekty pogodowe";
                    label5.Show();
                    pictureBox1.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, Color.LightYellow, ButtonBorderStyle.Solid);
                    };
                }
            }
            label2.Text = c.Name;
            label3.Text = Form1.GetCardTypeLabel(c) + "";
        }       
    }
}