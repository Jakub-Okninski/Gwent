using Gwent_Library;
using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Gwent_App
{
    public partial class Form1 : Form
    {

        public Gracz gracz1;
        public Gracz gracz2;
        public Gra gra;
        private PictureBox selectedPictureBox = null;
       // Form2 form2;
        public Form1() { }


        public void SetForm(Gra g)
        {
            gracz1 = g.gracz1;
            gracz2 = g.gracz2;
            gra = g;
          

            InitializeComponent();
            InitDefaultInfoComponent(gracz1, gracz2);
            InitImgPlayer(gracz1, gracz2);


            GeneratePictureBoxCard(panelGracza, gracz2);

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

        public Form1(Gra g)
        {
            gracz1 = g.gracz1;
            gracz2 = g.gracz2;
            gra = g;
          //  form2 = form;
           // form2.Show();
            InitializeComponent();
            InitDefaultInfoComponent(gracz1, gracz2);
            InitImgPlayer(gracz1, gracz2);


            GeneratePictureBoxCard(panelGracza, gracz1);

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

            Panel sourcePanel = selectedPictureBox.Parent as Panel;

            if (!sourcePanel.Name.Contains("WspolnePole") &&
                !sourcePanel.Name.Contains("panelZwarcie") &&
                !sourcePanel.Name.Contains("panelDystans") &&
                !sourcePanel.Name.Contains("panelObleznicze") &&
                !sourcePanel.Name.Contains("panelRogu") &&
                !sourcePanel.Name.Contains("panelSpecjalna"))
            {

                var karta = selectedPictureBox.Tag;

                panelZwarcieGracz1.AllowDrop = false;
                panelDystansGracz1.AllowDrop = false;
                panelOblezniczeGracz1.AllowDrop = false;
                panelRoguDystansGracz1.AllowDrop = false;
                panelRoguOblezniczeGracz1.AllowDrop = false;
                panelRoguZwarcieGracz1.AllowDrop = false;
                panelSpecjalnaGracz1.AllowDrop = false;
                panelWspolnePole.AllowDrop = false;


                if (karta is KartaPiechoty)
                {
                    panelZwarcieGracz1.AllowDrop = true;
                    panelDystansGracz1.AllowDrop = false;
                    panelOblezniczeGracz1.AllowDrop = false;
                    panelRoguDystansGracz1.AllowDrop = false;
                    panelRoguOblezniczeGracz1.AllowDrop = false;
                    panelRoguZwarcieGracz1.AllowDrop = false;
                    panelSpecjalnaGracz1.AllowDrop = false;
                    panelWspolnePole.AllowDrop = false;
                }

                if (karta is KartaLucznika)
                {
                    panelZwarcieGracz1.AllowDrop = false;
                    panelDystansGracz1.AllowDrop = true;
                    panelOblezniczeGracz1.AllowDrop = false;
                    panelRoguDystansGracz1.AllowDrop = false;
                    panelRoguOblezniczeGracz1.AllowDrop = false;
                    panelRoguZwarcieGracz1.AllowDrop = false;
                    panelSpecjalnaGracz1.AllowDrop = false;
                    panelWspolnePole.AllowDrop = false;
                }

                if (karta is KartaObleznika)
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
                if (karta is KartaDowodcy)
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

                if (karta is Pozoga || karta is KartaPogody)
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
        }



        private void PozogaPanel<T>(Panel panel, Talia<T> talia) where T : KartaJednostki
        {

            foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>().ToList())
            {
                T pictureBoxKarta = pictureBox.Tag as T;

                if (!talia.Contains(pictureBoxKarta))
                {
                    panel.Controls.Remove(pictureBox);
                }
            }

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
                // Usuñ PictureBox z obecnego panelu
                sourcePanel.Controls.Remove(pictureBox);
                // Dodaj PictureBox do panelu, do którego zosta³ przeci¹gniêty
                targetPanel.Controls.Add(pictureBox);



                Karta k = pictureBox.Tag as Karta;




                if (k is KartaDowodcy )
                {
                    targetPanel.DragEnter -= Panel_DragEnter;
                    targetPanel.DragDrop -= Panel_DragDrop;
                  //  AddCardToPanel(k, form2.panelSpecjalnaGracz2);
                }

                else if ( k is KartaPogody)
                {
                   // AddCardToPanel(k, form2.panelWspolnePole);
                }
                
                else if (k is KartaPiechoty)
                {
                   // AddCardToPanel(k, form2.panelZwarcieGracz2);
                }
                else if (k is KartaLucznika)
                {
                  //  AddCardToPanel(k, form2.panelDystansGracz2);
                }
                else if(k is KartaObleznika)
                {
                   // AddCardToPanel(k, form2.panelOblezniczeGracz2);
                }

                else if (k is RogDowodcy k1)
                {

                    if (targetPanel.Name.Contains("RoguDystans"))
                    {
                        k1.Umiejscowienie = Umiejscowienie.Lucznika;
                       // gra.WykonajRuch(gracz1, k1);
                       // AddCardToPanel(k, form2.panelRoguDystansGracz2);


                    }
                    else if (targetPanel.Name.Contains("RoguZwarcie"))
                    {
                        k1.Umiejscowienie = Umiejscowienie.Piechoty;
                       // gra.WykonajRuch(gracz1, k1);
                      //  AddCardToPanel(k, form2.panelRoguZwarcieGracz2);

                    }
                    else if (targetPanel.Name.Contains("RoguObleznicze"))
                    {
                        k1.Umiejscowienie = Umiejscowienie.Obleznicza;
                      //  gra.WykonajRuch(gracz1, k1);
                        //AddCardToPanel(k, form2.panelRoguOblezniczeGracz2);

                    }

                    targetPanel.DragEnter -= Panel_DragEnter;
                    targetPanel.DragDrop -= Panel_DragDrop;

                }


                gra.WykonajRuch(gracz1, k);

                if (k is Pozoga)
                {
                /*    AddCardToPanel(k, form2.panelWspolnePole);


                    PozogaPanel(form2.panelZwarcieGracz1, form2.gracz1.Plansza.KartyPiechotyGracza);
                    PozogaPanel(form2.panelZwarcieGracz2, form2.gracz2.Plansza.KartyPiechotyGracza);


                    PozogaPanel(form2.panelDystansGracz2, form2.gracz2.Plansza.KartyStrzeleckieGracza);
                    PozogaPanel(form2.panelDystansGracz1, form2.gracz1.Plansza.KartyStrzeleckieGracza);


                    PozogaPanel(form2.panelOblezniczeGracz1, form2.gracz1.Plansza.KartyOblezniczeGracza);
                    PozogaPanel(form2.panelOblezniczeGracz2, form2.gracz2.Plansza.KartyOblezniczeGracza);
*/



                    PozogaPanel(panelZwarcieGracz1, gracz1.Plansza.KartyPiechotyGracza);
                    PozogaPanel(panelZwarcieGracz2, gracz2.Plansza.KartyPiechotyGracza);


                    PozogaPanel(panelDystansGracz2, gracz2.Plansza.KartyStrzeleckieGracza);
                    PozogaPanel(panelDystansGracz1, gracz1.Plansza.KartyStrzeleckieGracza);


                    PozogaPanel(panelOblezniczeGracz1, gracz1.Plansza.KartyOblezniczeGracza);
                    PozogaPanel(panelOblezniczeGracz2, gracz2.Plansza.KartyOblezniczeGracza);




                }

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
            labelDystansGracz1.Text = gracz1.Plansza.PunktyStrzeleckie + "";
            labelZwarcieGracz1.Text = gracz1.Plansza.PunktyPiechoty + "";
            labelOblezniczeGracz1.Text = gracz1.Plansza.PunktyObleznicze + "";
            labelPunktySumaGracz1.Text = gracz1.Plansza.PunktySuma + "";


            labelDystansGracz2.Text = gracz2.Plansza.PunktyStrzeleckie + "";
            labelZwarcieGracz2.Text = gracz2.Plansza.PunktyPiechoty + "";
            labelOblezniczeGracz2.Text = gracz2.Plansza.PunktyObleznicze + "";
            labelPunktySumaGracz2.Text = gracz2.Plansza.PunktySuma + "";


/*
            form2.labelDystansGracz2.Text = gracz1.Plansza.PunktyStrzeleckie + "";
            form2.labelZwarcieGracz2.Text = gracz1.Plansza.PunktyPiechoty + "";
            form2.labelOblezniczeGracz2.Text = gracz1.Plansza.PunktyObleznicze + "";
            form2.labelPunktySumaGracz2.Text = gracz1.Plansza.PunktySuma + "";


            form2.labelDystansGracz1.Text = gracz2.Plansza.PunktyStrzeleckie + "";
            form2.labelZwarcieGracz1.Text = gracz2.Plansza.PunktyPiechoty + "";
            form2.labelOblezniczeGracz1.Text = gracz2.Plansza.PunktyObleznicze + "";
            form2.labelPunktySumaGracz1.Text = gracz2.Plansza.PunktySuma + "";*/


        }

        private void GeneratePictureBoxCard(Panel panel, Gracz gracz)
        {
            Karta[] cards = gracz.KartyGracza.ToArray();

            // Liczba PictureBox do wygenerowania
            int pictureBoxCount = cards.Count();

            // Rozmiar i odstêp pomiêdzy PictureBox
            int pictureBoxWidth = 60;
            int pictureBoxHeight = 90;
            int pictureBoxSpacing = 10;

            // Obliczanie szerokoœci panelu
            int panelWidth = panel.ClientSize.Width;

            // Obliczanie szerokoœci zajmowanej przez wszystkie karty oraz odstêpów
            int totalWidth = pictureBoxCount * pictureBoxWidth + (pictureBoxCount - 1) * pictureBoxSpacing;

            // Obliczanie pozycji pocz¹tkowej X, aby wyœrodkowaæ karty
            int startX = (panelWidth - totalWidth) / 2;

            // Pozycja pocz¹tkowa generowania PictureBox
            int startY = pictureBoxSpacing;

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
                pictureBox.MouseUp += PictureBox_MouseUp;

                // Dodaj etykietê nad PictureBox
                Label label = new Label();
                label.Text = cards[i].Nazwa;
                pictureBox.Tag = cards[i];
                label.AutoSize = true;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.BackColor = Color.Transparent;
                label.Dock = DockStyle.Top;
                pictureBox.Controls.Add(label);

                panel.Controls.Add(pictureBox);

                // Aktualizacja pozycji X dla nastêpnego PictureBox
                startX += pictureBoxWidth + pictureBoxSpacing;
            }

            // Odœwie¿enie pozycji kart w panelu
            RefreshCardPositions(panel, pictureBoxSpacing);
        }

        private void RefreshCardPositions(Panel panel, int pictureBoxSpacing = 10)
        {
            // Sprawdzenie, czy panel zawiera jakiekolwiek kontrolki
            if (panel.Controls.Count > 0)
            {
                // Obliczanie szerokoœci zajmowanej przez wszystkie karty oraz odstêpów
                int totalWidth = panel.Controls.Count * panel.Controls[0].Width + (panel.Controls.Count - 1) * pictureBoxSpacing;

                // Obliczanie szerokoœci panelu
                int panelWidth = panel.ClientSize.Width;

                // Obliczanie pozycji pocz¹tkowej X, aby wyœrodkowaæ karty
                int startX = (panelWidth - totalWidth) / 2;

                // Pozycja pocz¹tkowa Y
                int startY = pictureBoxSpacing;

                // Sprawdzenie, czy karty nie zmieszcz¹ siê w jednym rzêdzie
                if (totalWidth > panelWidth)
                {
                    // Obliczanie nowego odstêpu pomiêdzy kartami
                    pictureBoxSpacing = (panelWidth - panel.Controls.Count * panel.Controls[0].Width) / (panel.Controls.Count - 1);

                    // Resetowanie pozycji pocz¹tkowej X
                    startX = pictureBoxSpacing;
                }

                // Ustawienie nowych pozycji dla ka¿dej karty
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
        }






        private void UpdateImgPoints(Gracz gracz1, Gracz gracz2)
        {
            if (gra.gracz1.Punkty == 1)
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
        private void RemoveCardFromPanel(PictureBox pictureBox, Panel panel)
        {
            // SprawdŸ, czy PictureBox istnieje w panelu
            if (panel.Controls.Contains(pictureBox))
            {
                // Usuñ PictureBox z panelu
                panel.Controls.Remove(pictureBox);
                // Odœwie¿ pozycje kart w panelu
                RefreshCardPositions(panel);
            }
        }

        private void AddCardToPanel(Karta karta, Panel panel)
        {
            // Tworzenie nowego PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(60, 90);
            pictureBox.BackColor = Color.LightGray;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            Random random = new Random();
            pictureBox.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;

            // Dodaj etykietê nad PictureBox
            Label label = new Label();
            label.Text = karta.Nazwa;
            label.AutoSize = true;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.BackColor = Color.Transparent;
            label.Dock = DockStyle.Top;

            // Przypisanie karty jako Tag do PictureBox
            pictureBox.Tag = karta;

            // Dodanie etykiety do PictureBox
            pictureBox.Controls.Add(label);

            // Dodanie PictureBox do panelu
            panel.Controls.Add(pictureBox);

            // Odœwie¿enie pozycji kart w panelu
            RefreshCardPositions(panel);
        }


        private void labelPunktySumaGracz2_Click(object sender, EventArgs e)
        {

        }
    }
}
