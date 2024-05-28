using Gwent_Library;
using Gwent_Library.Karty;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Gwent_App
{
    public partial class Form2 : Form
    {
        List<PictureBox> listaPictureBoxow = new List<PictureBox>();

        public Gracz gracz1;
        public Gracz gracz2;
        public Form1 form1;
        public Gra gra;
        private PictureBox selectedPictureBox = null;
        private Karta lastCard = null;


        public Form2()
        {          
        }


        public void setForm(Gra g, Form1 f)
        {
            gracz1 = g.gracz1;
            gracz2 = g.gracz2;
            gra = g;
            form1 = f;
            InitializeComponent();
            InitDefaultInfoComponent(gracz1, gracz2);
            InitImgPlayer(gracz1, gracz2);
            this.Enabled = false;


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
            //git
            selectedPictureBox = sender as PictureBox;
            Panel sourcePanel = selectedPictureBox.Parent as Panel;
            var karta = selectedPictureBox.Tag;

            if (sourcePanel.Name.Contains("panelGracza"))
            {
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
                }
                else if (karta is KartaLucznika)
                {
                    panelDystansGracz1.AllowDrop = true;
                }
                else if (karta is KartaObleznika)
                {
                    panelOblezniczeGracz1.AllowDrop = true;
                }
                else if (karta is Manekin)
                {
                    panelZwarcieGracz1.AllowDrop = true;
                    panelDystansGracz1.AllowDrop = true;
                    panelOblezniczeGracz1.AllowDrop = true;
                }

                else if (karta is RogDowodcy)
                {
                    panelRoguDystansGracz1.AllowDrop = true;
                    panelRoguOblezniczeGracz1.AllowDrop = true;
                    panelRoguZwarcieGracz1.AllowDrop = true;
                }
                else if (karta is KartaDowodcy)
                {
                    panelSpecjalnaGracz1.AllowDrop = true;
                }

                else if (karta is KartaSpecjalna)
                {
                    panelWspolnePole.AllowDrop = true;
                }
                selectedPictureBox.DoDragDrop(selectedPictureBox, DragDropEffects.Move);
            }
        }



        private void Czekaj()
        {
            SkopiujPictureBoxy(panelWspolnePole, form1.panelWspolnePole);
            SkopiujPictureBoxy(panelSpecjalnaGracz1, form1.panelSpecjalnaGracz2);
            SkopiujPictureBoxy(panelRoguZwarcieGracz1, form1.panelRoguZwarcieGracz2);
            SkopiujPictureBoxy(panelRoguDystansGracz1, form1.panelRoguDystansGracz2);
            SkopiujPictureBoxy(panelRoguOblezniczeGracz1, form1.panelRoguOblezniczeGracz2);

            SkopiujPictureBoxy(panelOblezniczeGracz1, form1.panelOblezniczeGracz2);
            SkopiujPictureBoxy(panelDystansGracz1, form1.panelDystansGracz2);
            SkopiujPictureBoxy(panelZwarcieGracz1, form1.panelZwarcieGracz2);


            if (gracz1.Play)
            {
                this.Enabled = false;
                form1.Enabled = true;
            }
          

            //  MessageBox.Show("Ruch Przeciwnika", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // this.Enabled = false;
        }





        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectedPictureBox = null;
        }
        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }



        public void SkopiujPictureBoxy(Panel panelZrodlowy, Panel panelDocelowy)
        {
            panelDocelowy.Controls.Clear();

            foreach (PictureBox originalPictureBox in panelZrodlowy.Controls.OfType<PictureBox>())
            {
                Karta k = originalPictureBox.Tag as Karta;
                AddCardToPanel(k, panelDocelowy);
            }

            RefreshCardPositions(panelDocelowy);

        }



        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pictureBox = e.Data.GetData(typeof(PictureBox)) as PictureBox;


            Panel sourcePanel = pictureBox.Parent as Panel;
            Panel targetPanel = sender as Panel;

            if (pictureBox != null && targetPanel != null && targetPanel != sourcePanel)
            {

                Karta k = pictureBox.Tag as Karta;

                sourcePanel.Controls.Remove(pictureBox);
                targetPanel.Controls.Add(pictureBox);

                if (k is RogDowodcy k1)
                {
                    if (targetPanel.Name.Contains("RoguDystans"))
                    {
                        k1.Umiejscowienie = Umiejscowienie.Lucznika;
                    }
                    else if (targetPanel.Name.Contains("RoguZwarcie"))
                    {
                        k1.Umiejscowienie = Umiejscowienie.Piechoty;
                    }
                    else if (targetPanel.Name.Contains("RoguObleznicze"))
                    {
                        k1.Umiejscowienie = Umiejscowienie.Obleznicza;
                    }
                    targetPanel.DragEnter -= Panel_DragEnter;
                    targetPanel.DragDrop -= Panel_DragDrop;
                }
                else if (k is Manekin M)
                {
                    if (targetPanel.Name.Contains("panelZwarcie"))
                    {
                        M.Umiejscowienie = Umiejscowienie.Piechoty;
                        lastCard = M;
                        foreach (Control control in panelZwarcieGracz1.Controls)
                        {
                            if (control is PictureBox pictureBoxa)
                            {
                                if (pictureBoxa.Tag is KartaJednostki)
                                    pictureBoxa.DoubleClick += PictureBox_DoubleClick;
                            }
                        }
                    }
                    else if (targetPanel.Name.Contains("panelDystans"))
                    {
                        M.Umiejscowienie = Umiejscowienie.Lucznika;
                        lastCard = M;
                        foreach (Control control in panelDystansGracz1.Controls)
                        {
                            if (control is PictureBox pictureBoxa)
                            {
                                if (pictureBoxa.Tag is KartaJednostki)
                                    pictureBoxa.DoubleClick += PictureBox_DoubleClick;
                            }
                        }
                    }
                    else if (targetPanel.Name.Contains("panelObleznicze"))
                    {
                        M.Umiejscowienie = Umiejscowienie.Obleznicza;
                        lastCard = M;
                        foreach (Control control in panelOblezniczeGracz1.Controls)
                        {
                            if (control is PictureBox pictureBoxa)
                            {
                                if (pictureBoxa.Tag is KartaJednostki)
                                    pictureBoxa.DoubleClick += PictureBox_DoubleClick;
                            }
                        }
                    }
                }



                if (k is not Manekin)
                {
                    try
                    {
                        gra.WykonajRuch(gracz1, k);
                    }
                    catch (ExceptionDowodca ED)
                    {
                        MessageBox.Show(ED.Message, "Niezły z ciebie taktyk :) ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    catch (ExceptionRog ER)
                    {
                        MessageBox.Show(ER.Message, "Róg zajęty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message, "No to mamy błąd:) ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                if (k is Pozoga)
                {

                    FiltrPanelPozoga(panelZwarcieGracz1, gracz1.Plansza.KartyPiechotyGracza);
                    FiltrPanelPozoga(panelZwarcieGracz2, gracz2.Plansza.KartyPiechotyGracza);
                    FiltrPanelPozogaManekin(panelZwarcieGracz1, gracz1.Plansza, Umiejscowienie.Piechoty);
                    FiltrPanelPozogaManekin(panelZwarcieGracz2, gracz2.Plansza, Umiejscowienie.Piechoty);

                    FiltrPanelPozoga(panelDystansGracz2, gracz2.Plansza.KartyStrzeleckieGracza);
                    FiltrPanelPozoga(panelDystansGracz1, gracz1.Plansza.KartyStrzeleckieGracza);
                    FiltrPanelPozogaManekin(panelDystansGracz1, gracz1.Plansza, Umiejscowienie.Lucznika);
                    FiltrPanelPozogaManekin(panelDystansGracz2, gracz2.Plansza, Umiejscowienie.Lucznika);

                    FiltrPanelPozoga(panelOblezniczeGracz1, gracz1.Plansza.KartyOblezniczeGracza);
                    FiltrPanelPozoga(panelOblezniczeGracz2, gracz2.Plansza.KartyOblezniczeGracza);
                    FiltrPanelPozogaManekin(panelOblezniczeGracz1, gracz1.Plansza, Umiejscowienie.Obleznicza);
                    FiltrPanelPozogaManekin(panelOblezniczeGracz2, gracz2.Plansza, Umiejscowienie.Obleznicza);
                }
                else if (k is KartaJednostki kj && kj.Effect == CardEffects.Bractwo)
                {
                    if (kj is KartaPiechoty)
                    {
                        FiltrPanelBractwo(gracz1.Plansza.KartyPiechotyGracza, kj, panelZwarcieGracz1);

                    }
                    else if (kj is KartaObleznika)
                    {
                        FiltrPanelBractwo(gracz1.Plansza.KartyOblezniczeGracza, kj, panelOblezniczeGracz1);

                    }
                    else if (kj is KartaLucznika)
                    {
                        FiltrPanelBractwo(gracz1.Plansza.KartyStrzeleckieGracza, kj, panelDystansGracz1);



                    }
                }
                else if (k is KartaDowodcy KD)
                {

                    FiltrPanelRogu(panelRoguOblezniczeGracz1, gracz1.Plansza, Umiejscowienie.Obleznicza);
                    FiltrPanelRogu(panelRoguOblezniczeGracz2, gracz2.Plansza, Umiejscowienie.Obleznicza);


                    FiltrPanelPozoga(panelOblezniczeGracz1, gracz1.Plansza.KartyOblezniczeGracza);
                    FiltrPanelPozoga(panelOblezniczeGracz2, gracz2.Plansza.KartyOblezniczeGracza);
                    FiltrPanelPozogaManekin(panelOblezniczeGracz1, gracz1.Plansza, Umiejscowienie.Obleznicza);
                    FiltrPanelPozogaManekin(panelOblezniczeGracz2, gracz2.Plansza, Umiejscowienie.Obleznicza);


                    FiltrPanelSpecjalne(panelWspolnePole, KD);





                }
                RefreshPanelSpecial(panelWspolnePole);
                OdswiezPanel(targetPanel);
                RefreshCardPositions(sourcePanel);
                RefreshCardPositions(targetPanel);
                Czekaj();
            }
        }

        private void FiltrPanelBractwo<T>(Talia<T> talia, KartaJednostki kj, Panel panel) where T : KartaJednostki
        {

            var karty = talia.Where(karta => karta is KartaJednostki && karta.Nazwa == kj.Nazwa && kj.Effect == CardEffects.Bractwo);
            foreach (KartaJednostki kk in karty)
            {
                if (FindPictureBoxByCard(panel, kk) == null)
                {
                    AddCardToPanel(kk, panel);
                    RemoveCardFromPanel(FindPictureBoxByCard(panelGracza, kk), panelGracza);
                }
            }


        }
        private void RefreshPanelSpecial(Panel panel)
        {
            var pictureBoxes = panel.Controls.OfType<PictureBox>().ToList();

            if (pictureBoxes.Count > 4)
            {
                for (int i = 0; i < pictureBoxes.Count - 4; i++)
                {
                    panel.Controls.Remove(pictureBoxes[i]);
                }
            }
            RefreshCardPositions(panel);
        }
        private void FiltrPanelSpecjalne(Panel panel, KartaDowodcy kartaDowodcy)
        {
            if (kartaDowodcy is FoltestDowódcaPółnocy)
            {
                AddCardToPanel(new CzysteNiebo("Czyste Niebo Krola Temerii", "CzysteNiebo"), panel);
            }
            if (kartaDowodcy is FoltestKrólTemerii)
            {
                AddCardToPanel(new GestaMgla("Gesta Mgla Krola Temerii", "GestaMgla"), panel);
            }
            RefreshPanelSpecial(panel);

        }
        private void FiltrPanelRogu(Panel panel, Plansza plansza, Umiejscowienie umiejscowienie)
        {
            panel.Controls.Clear();
            var ka = plansza.KartySpecjalne.Where(kartaa => (kartaa is RogDowodcy ks && ks.Umiejscowienie == umiejscowienie));

            if (ka.Any())
            {
                foreach (var karty in ka)
                {
                    AddCardToPanel(karty, panel);


                }
                panel.DragEnter -= Panel_DragEnter;
                panel.DragDrop -= Panel_DragDrop;
            }
        }
        private void FiltrPanelPozogaManekin(Panel panel, Plansza plansza, Umiejscowienie umiejscowienie)
        {

            var ka = plansza.KartySpecjalne.Where(kartaa => (kartaa is Manekin ks && ks.Umiejscowienie == umiejscowienie));

            if (ka.Any())
            {
                foreach (var karty in ka)
                {
                    AddCardToPanel(karty, panel);
                }
            }
        }
        private void FiltrPanelPozoga<T>(Panel panel, Talia<T> talia) where T : KartaJednostki
        {
            panel.Controls.Clear();

            foreach (Karta karty in talia)
            {
                AddCardToPanel(karty, panel);
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


            form1.labelDystansGracz2.Text = gracz1.Plansza.PunktyStrzeleckie + "";
            form1.labelZwarcieGracz2.Text = gracz1.Plansza.PunktyPiechoty + "";
            form1.labelOblezniczeGracz2.Text = gracz1.Plansza.PunktyObleznicze + "";
            form1.labelPunktySumaGracz2.Text = gracz1.Plansza.PunktySuma + "";


            form1.labelDystansGracz1.Text = gracz2.Plansza.PunktyStrzeleckie + "";
            form1.labelZwarcieGracz1.Text = gracz2.Plansza.PunktyPiechoty + "";
            form1.labelOblezniczeGracz1.Text = gracz2.Plansza.PunktyObleznicze + "";
            form1.labelPunktySumaGracz1.Text = gracz2.Plansza.PunktySuma + "";

        }


        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            selectedPictureBox = sender as PictureBox;

            Panel sourcePanel = selectedPictureBox.Parent as Panel;

            if (lastCard != null)
            {
                if (sourcePanel.Name.Contains("panelZwarcie"))
                {
                    KartaPiechoty karta = selectedPictureBox.Tag as KartaPiechoty;
                    gra.WykonajRuch<KartaPiechoty>(gracz1, lastCard, karta);
                    RemoveCardFromPanel(selectedPictureBox, sourcePanel);
                    AddCardToPanel(karta, panelGracza);
                }
                else if (sourcePanel.Name.Contains("panelDystans"))
                {
                    KartaLucznika karta = selectedPictureBox.Tag as KartaLucznika;
                    gra.WykonajRuch<KartaLucznika>(gracz1, lastCard, karta);
                    RemoveCardFromPanel(selectedPictureBox, sourcePanel);
                    AddCardToPanel(karta, panelGracza);
                }
                else if (sourcePanel.Name.Contains("panelObleznicze"))
                {
                    KartaObleznika karta = selectedPictureBox.Tag as KartaObleznika;
                    gra.WykonajRuch<KartaObleznika>(gracz1, lastCard, karta);

                    RemoveCardFromPanel(selectedPictureBox, sourcePanel);
                    AddCardToPanel(karta, panelGracza);

                }
            }

            foreach (Control control in sourcePanel.Controls)
            {
                if (control is PictureBox pictureBoxa)
                {
                    pictureBoxa.DoubleClick -= PictureBox_DoubleClick;
                }
            }
            lastCard = null;
            RefreshCardPositions(sourcePanel);
            RefreshCardPositions(panelGracza);
            OdswiezPanel(sourcePanel);
            System.Diagnostics.Debug.WriteLine("Log: Stadsadadad23424242rt xD");

            Czekaj();


        }
        public void PoddajeSie()
        {
            MessageBox.Show("Poddał sie", "Gracz sie poddał", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void GeneratePictureBoxCard(Panel panel, Gracz gracz)
        {
            foreach (var cardToAdd in gracz.Plansza.KartyGraczaWRozgrywce)
            {
                AddCardToPanel(cardToAdd, panel);
            }
        }


        private PictureBox FindPictureBoxByCard<T>(Panel panel, T karta) where T : Karta
        {
            foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
            {
                T pictureBoxKarta = pictureBox.Tag as T;
                if (pictureBoxKarta != null && pictureBoxKarta.Equals(karta))
                {
                    return pictureBox;
                }
            }
            return null;
        }


        private void RefreshCardPositions(Panel panel, int pictureBoxSpacing = 10)
        {
            // Sprawdzenie, czy panel zawiera jakiekolwiek kontrolki
            if (panel.Controls.Count > 0)
            {
                // Obliczanie szerokości zajmowanej przez wszystkie karty oraz odstępów
                int totalWidth = panel.Controls.Count * panel.Controls[0].Width + (panel.Controls.Count - 1) * pictureBoxSpacing;

                // Obliczanie szerokości panelu
                int panelWidth = panel.ClientSize.Width;

                // Obliczanie pozycji początkowej X, aby wyśrodkować karty
                int startX = (panelWidth - totalWidth) / 2;

                // Pozycja początkowa Y
                int startY = pictureBoxSpacing;

                // Sprawdzenie, czy karty nie zmieszczą się w jednym rzędzie
                if (totalWidth > panelWidth)
                {
                    // Obliczanie nowego odstępu pomiędzy kartami
                    pictureBoxSpacing = (panelWidth - panel.Controls.Count * panel.Controls[0].Width) / (panel.Controls.Count - 1);

                    // Resetowanie pozycji początkowej X
                    startX = pictureBoxSpacing;
                }

                // Ustawienie nowych pozycji dla każdej karty
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
            // Sprawdź, czy PictureBox istnieje w panelu
            if (panel.Controls.Contains(pictureBox))
            {
                // Usuń PictureBox z panelu
                panel.Controls.Remove(pictureBox);
                // Odśwież pozycje kart w panelu
                // RefreshCardPositions(panel);
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
            pictureBox.DoubleClick += null;

            // Dodaj etykietę nad PictureBox
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

            // Odświeżenie pozycji kart w panelu
            //             RefreshCardPositions(panel);
            RefreshCardPositions(panel);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gracz2.Play = false;
            form1.PoddajeSie();

            this.Enabled = false;
            form1.Enabled = true;

        }
    }
}