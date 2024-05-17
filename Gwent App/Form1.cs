using Gwent_Library;
using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System.Windows.Forms;

namespace Gwent_App
{
    public partial class Form1 : Form
    {
        public Gracz gracz1;
        public Gracz gracz2;
        public Gra gra;

        private PictureBox selectedPictureBox = null;


        public Form1(Gra g)
        {
            gracz1 = g.gracz1;
            gracz2 = g.gracz2;
            gra = g;

            InitializeComponent();
            InitDefaultInfoComponent(gracz1, gracz2);
            InitImgPlayer(gracz1, gracz2);


            //edit xD

            GeneratePictureBoxCard(panelGracza);

            panelGracza.AllowDrop = true;
          


            panelGracza.DragEnter += Panel_DragEnter;
            panelGracza.DragDrop += Panel_DragDrop;
           
            panelOblezniczeGracz1.DragEnter += Panel_DragEnter;
            panelOblezniczeGracz1.DragDrop += Panel_DragDrop;

            panelDystansGracz1.DragEnter += Panel_DragEnter;
            panelDystansGracz1.DragDrop += Panel_DragDrop;

            panelZwarcieGracz1.DragEnter += Panel_DragEnter;
            panelZwarcieGracz1.DragDrop += Panel_DragDrop;


            panelRoguDystansGracz1.DragEnter += Panel_DragEnter;
            panelRoguDystansGracz1.DragDrop += Panel_DragDrop;

            panelRoguOblezniczeGracz1.DragEnter += Panel_DragEnter;
            panelRoguOblezniczeGracz1.DragDrop += Panel_DragDrop;

            panelRoguZwarcieGracz1.DragEnter += Panel_DragEnter;
            panelRoguZwarcieGracz1.DragDrop += Panel_DragDrop;

            panelSpecjalnaGracz1.DragEnter += Panel_DragEnter;
            panelSpecjalnaGracz1.DragDrop += Panel_DragDrop;


            panelWspolnePole.DragEnter += Panel_DragEnter;
            panelWspolnePole.DragDrop += Panel_DragDrop;



        }



        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            selectedPictureBox = sender as PictureBox;

            var karta = selectedPictureBox.Tag;

            labelDystansGracz1.Text = "dupa";

            panelZwarcieGracz1.AllowDrop = false;
            panelDystansGracz1.AllowDrop = false;
            panelOblezniczeGracz1.AllowDrop = false;

            panelRoguDystansGracz1.AllowDrop = false;
            panelRoguOblezniczeGracz1.AllowDrop = false;
            panelRoguZwarcieGracz1.AllowDrop = false;

            panelSpecjalnaGracz1.AllowDrop = false;

            panelWspolnePole.AllowDrop = false;

            
            if (karta is IPolaPiechoty)
            {


                panelZwarcieGracz1.AllowDrop = true;
                panelDystansGracz1.AllowDrop = false;
                panelOblezniczeGracz1.AllowDrop = false;

                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;

                panelSpecjalnaGracz1.AllowDrop = false;

                panelWspolnePole.AllowDrop = false;


             

                labelDystansGracz1.Text = "1";
       


            }

             if (karta is IPolaStrzeleckie)
            {



                panelZwarcieGracz1.AllowDrop = false;
                panelDystansGracz1.AllowDrop = true;
                panelOblezniczeGracz1.AllowDrop = false;

                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;

                panelSpecjalnaGracz1.AllowDrop = false;

                panelWspolnePole.AllowDrop = false;




        
                labelDystansGracz1.Text = "2";
              


            }

             if (karta is IPolaObl�znicze)
            {



                panelZwarcieGracz1.AllowDrop = false;
                panelDystansGracz1.AllowDrop = false;
                panelOblezniczeGracz1.AllowDrop = true;

                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;

                panelSpecjalnaGracz1.AllowDrop = false;

                panelWspolnePole.AllowDrop = false;




                labelDystansGracz1.Text = "3";
               

            }

          

            if (karta is IPolaObl�znicze&& karta is IPolaStrzeleckie && karta is IPolaObl�znicze)
            {



                panelZwarcieGracz1.AllowDrop = true;
                panelDystansGracz1.AllowDrop = true;
                panelOblezniczeGracz1.AllowDrop = true;

                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;

                panelSpecjalnaGracz1.AllowDrop = false;

                panelWspolnePole.AllowDrop = false;





            }
            if (karta is RogDowodcy)
            {


                panelZwarcieGracz1.AllowDrop = false;
                panelDystansGracz1.AllowDrop = false;
                panelOblezniczeGracz1.AllowDrop = false;

                panelRoguDystansGracz1.AllowDrop = true;
                panelRoguOblezniczeGracz1.AllowDrop = true;
                panelRoguZwarcieGracz1.AllowDrop = true;

                panelSpecjalnaGracz1.AllowDrop = false;

                panelWspolnePole.AllowDrop = false;
             

       
         
            }
            if(karta is KartaDowodcy)
            {

                panelZwarcieGracz1.AllowDrop = false;
                panelDystansGracz1.AllowDrop = false;
                panelOblezniczeGracz1.AllowDrop = false;

                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;

                panelSpecjalnaGracz1.AllowDrop = true;

                panelWspolnePole.AllowDrop = false;
           
         

            }

            if (karta is Pozoga || karta is CzysteNiebo)
            {

                panelZwarcieGracz1.AllowDrop = false;
                panelDystansGracz1.AllowDrop = false;
                panelOblezniczeGracz1.AllowDrop = false;

                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;

                panelSpecjalnaGracz1.AllowDrop = false;

                panelWspolnePole.AllowDrop = true;
              
           
            }

            selectedPictureBox.DoDragDrop(selectedPictureBox, DragDropEffects.Move);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
         
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectedPictureBox = null;
        }

        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pictureBox = e.Data.GetData(typeof(PictureBox)) as PictureBox;
            Panel sourcePanel = pictureBox.Parent as Panel;
            Panel targetPanel = sender as Panel;

            if (pictureBox != null && targetPanel != null && targetPanel != sourcePanel)
            {
                // Usu� PictureBox z obecnego panelu
                sourcePanel.Controls.Remove(pictureBox);
                // Dodaj PictureBox do panelu, do kt�rego zosta� przeci�gni�ty
                targetPanel.Controls.Add(pictureBox);

                gra.WykonajRuch(gracz1, pictureBox.Tag as Karta);


                OdswiezPanel(targetPanel);
                RefreshCardPositions(sourcePanel);
                RefreshCardPositions(targetPanel);
            }

    
        }
        private void OdswiezPanel(Panel panel)
        {
            OdswiezPunktacje();
        }
        private void OdswiezPunktacje()
        {
            labelDystansGracz1.Text = gracz1.PunktyStrzeleckie+"";
            labelZwarcieGracz1.Text = gracz1.PunktyPiechoty + "";
            labelOblezniczeGracz1.Text = gracz1.PunktyObleznicze + "";
            labelPunktySumaGracz1.Text = gracz1.PunktySuma+ "";


            labelDystansGracz2.Text = gracz2.PunktyStrzeleckie + "";
            labelZwarcieGracz2.Text = gracz2.PunktyPiechoty + "";
            labelOblezniczeGracz2.Text = gracz2.PunktyObleznicze + "";
            labelPunktySumaGracz2.Text = gracz2.PunktySuma+ "";


        }

        private void GeneratePictureBoxCard(Panel panel)
        {
            // Liczba PictureBox do wygenerowania
            int pictureBoxCount = 10;

            // Rozmiar i odst�p pomi�dzy PictureBox
            int pictureBoxWidth = 60;
            int pictureBoxHeight = 90;
            int pictureBoxSpacing = 10;

            // Obliczanie szeroko�ci panelu
            int panelWidth = panel.ClientSize.Width;

            // Obliczanie szeroko�ci zajmowanej przez wszystkie karty oraz odst�p�w
            int totalWidth = pictureBoxCount * pictureBoxWidth + (pictureBoxCount - 1) * pictureBoxSpacing;

            // Obliczanie pozycji pocz�tkowej X, aby wy�rodkowa� karty
            int startX = (panelWidth - totalWidth) / 2;

            // Pozycja pocz�tkowa generowania PictureBox
            int startY = pictureBoxSpacing;
            Karta[] cards = gracz1.KartyGracza.ToArray();

            for (int i = 0; i < pictureBoxCount; i++)
            {
                // Tworzenie nowego PictureBox
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = "Karta" + (i + 1);
                pictureBox.Size = new Size(pictureBoxWidth, pictureBoxHeight);
                pictureBox.Location = new Point(startX, startY);
                pictureBox.BackColor = Color.LightGray;

                Random random = new Random();
                pictureBox.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.MouseDown += PictureBox_MouseDown;
                pictureBox.MouseMove += PictureBox_MouseMove;
                pictureBox.MouseUp += PictureBox_MouseUp;

                // Dodaj etykiet� nad PictureBox
                Label label = new Label();
                label.Text = cards[i].Nazwa;
               pictureBox.Tag = cards[i];
                label.AutoSize = true;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.BackColor = Color.Transparent;
                label.Dock = DockStyle.Top;
                pictureBox.Controls.Add(label);
             
                panel.Controls.Add(pictureBox);

                // Aktualizacja pozycji X dla nast�pnego PictureBox
                startX += pictureBoxWidth + pictureBoxSpacing;
            }

            // Od�wie�enie pozycji kart w panelu
            RefreshCardPositions(panel, pictureBoxSpacing);
        }

        private void RefreshCardPositions(Panel panel, int pictureBoxSpacing = 10)
        {
            // Obliczanie szeroko�ci zajmowanej przez wszystkie karty oraz odst�p�w
            int totalWidth = panel.Controls.Count * panel.Controls[0].Width + (panel.Controls.Count - 1) * pictureBoxSpacing;

            // Obliczanie szeroko�ci panelu
            int panelWidth = panel.ClientSize.Width;

            // Obliczanie pozycji pocz�tkowej X, aby wy�rodkowa� karty
            int startX = (panelWidth - totalWidth) / 2;

            // Pozycja pocz�tkowa Y
            int startY = pictureBoxSpacing;

            // Sprawdzenie, czy karty nie zmieszcz� si� w jednym rz�dzie
            if (totalWidth > panelWidth)
            {
                // Obliczanie nowego odst�pu pomi�dzy kartami
                pictureBoxSpacing = (panelWidth - panel.Controls.Count * panel.Controls[0].Width) / (panel.Controls.Count - 1);

                // Resetowanie pozycji pocz�tkowej X
                startX = pictureBoxSpacing;
            }

            // Ustawienie nowych pozycji dla ka�dej karty
            foreach (Control control in panel.Controls)
            {
                PictureBox pictureBox = control as PictureBox;
                if (pictureBox != null)
                {
                    pictureBox.Location = new Point(startX, startY);
                    startX += pictureBox.Width + pictureBoxSpacing;
                }
            }
        }





        private void UpdateImgPoints(Gracz gracz1, Gracz gracz2)
        {
            if(gra.gracz1.Punkty == 1)
            {
                pictureBoxDrugiPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxDrugiPunktGracz1.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragdpusty.PNG");
            }
            if (gra.gracz1.Punkty == 1)
            {
                pictureBoxDrugiPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxDrugiPunktGracz2.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragdpusty.PNG");
               
            }
        }

            private void InitImgPlayer(Gracz gracz1, Gracz gracz2)
        {
            pictureBoxZdjecieGracz1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxZdjecieGracz1.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\Geralt.PNG");
            pictureBoxZdjecieGracz2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxZdjecieGracz2.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\Geralt.PNG");
        }
           private void InitDefaultInfoComponent(Gracz gracz1, Gracz gracz2)
        {
            labelImieGracz1.Text = gracz1.Imie;
            labelImieGracz2.Text = gracz2.Imie;

            pictureBoxDrugiPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPierwszyPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxDrugiPunktGracz1.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragd.PNG");
            pictureBoxPierwszyPunktGracz1.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragd.PNG");
            pictureBoxDrugiPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPierwszyPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxDrugiPunktGracz2.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragd.PNG");
            pictureBoxPierwszyPunktGracz2.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragd.PNG");

        }

        ///nie ruszaj xD
    }
}
