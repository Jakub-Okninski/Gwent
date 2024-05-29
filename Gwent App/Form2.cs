using Gwent_Library;
using Gwent_Library.Karty;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;
using System.Media;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Gwent_App
{
    public partial class Form2 : Form
    {
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
            gracz1 = g.gracz2;
            gracz2 = g.gracz1;
            gra = g;
            form1 = f;
            InitializeComponent();
            InitDefaultInfoComponent(gracz1, gracz2);
            InitImgPlayer(gracz1, gracz2);
            Enabled = true;

            foreach (var cardToAdd in gracz1.Plansza.KartyGraczaWRozgrywce)
            {
                AddCardToPanel(cardToAdd, panelGracza);
            }


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
        public void Drop()
        {
            PlaySound("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\drop.wav");

        }
        static void PlaySound(string filePath)
        {
            Task.Run(() =>
            {
                using (var audioFile = new AudioFileReader(filePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            });
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
                    panelZwarcieGracz1.AllowDrop = false;
                    panelDystansGracz1.AllowDrop = false;
                    panelOblezniczeGracz1.AllowDrop = false;

                    foreach (Control control in panelZwarcieGracz1.Controls)
                    {
                        if (control is PictureBox pictureBox && pictureBox.Tag is KartaJednostki)
                            panelZwarcieGracz1.AllowDrop = true;
                    }
                    foreach (Control control in panelDystansGracz1.Controls)
                    {
                        if (control is PictureBox pictureBox && pictureBox.Tag is KartaJednostki)
                            panelDystansGracz1.AllowDrop = true;
                    }
                    foreach (Control control in panelOblezniczeGracz1.Controls)
                    {
                        if (control is PictureBox pictureBox && pictureBox.Tag is KartaJednostki)
                            panelOblezniczeGracz1.AllowDrop = true;
                    }
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
                Drop();
                selectedPictureBox.DoDragDrop(selectedPictureBox, DragDropEffects.Move);
            }
        }

        private void WykonanoRuch()
        {
            SkopiujPictureBoxy(panelWspolnePole, form1.panelWspolnePole);
            SkopiujPictureBoxy(panelSpecjalnaGracz1, form1.panelSpecjalnaGracz2);

            SkopiujPictureBoxy(panelRoguZwarcieGracz1, form1.panelRoguZwarcieGracz2);
            SkopiujPictureBoxy(panelRoguDystansGracz1, form1.panelRoguDystansGracz2);
            SkopiujPictureBoxy(panelRoguOblezniczeGracz1, form1.panelRoguOblezniczeGracz2);

            SkopiujPictureBoxy(panelOblezniczeGracz1, form1.panelOblezniczeGracz2);
            SkopiujPictureBoxy(panelDystansGracz1, form1.panelDystansGracz2);
            SkopiujPictureBoxy(panelZwarcieGracz1, form1.panelZwarcieGracz2);

            SkopiujPictureBoxy(form1.panelOblezniczeGracz1, panelOblezniczeGracz2);
            SkopiujPictureBoxy(form1.panelDystansGracz1, panelDystansGracz2);
            SkopiujPictureBoxy(form1.panelZwarcieGracz1, panelZwarcieGracz2);


            if (gracz2.Play)
            {
                labelInfo.Text = "Ruch Gracza ...";
                form1.labelInfo.Text = "Twój Ruch !!!";

                this.Enabled = false;
                form1.Enabled = true;
            }
            OdswiezPunktacje();
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
            //git
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
                Drop();
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


                    FiltrPanelPozoga(form1.panelZwarcieGracz1, gracz2.Plansza.KartyPiechotyGracza);
                    FiltrPanelPozoga(form1.panelZwarcieGracz2, gracz1.Plansza.KartyPiechotyGracza);
                    FiltrPanelPozogaManekin(form1.panelZwarcieGracz1, gracz2.Plansza, Umiejscowienie.Piechoty);
                    FiltrPanelPozogaManekin(form1.panelZwarcieGracz2, gracz1.Plansza, Umiejscowienie.Piechoty);

                    FiltrPanelPozoga(form1.panelDystansGracz2, gracz1.Plansza.KartyStrzeleckieGracza);
                    FiltrPanelPozoga(form1.panelDystansGracz1, gracz2.Plansza.KartyStrzeleckieGracza);
                    FiltrPanelPozogaManekin(form1.panelDystansGracz1, gracz2.Plansza, Umiejscowienie.Lucznika);
                    FiltrPanelPozogaManekin(form1.panelDystansGracz2, gracz1.Plansza, Umiejscowienie.Lucznika);

                    FiltrPanelPozoga(form1.panelOblezniczeGracz1, gracz2.Plansza.KartyOblezniczeGracza);
                    FiltrPanelPozoga(form1.panelOblezniczeGracz2, gracz1.Plansza.KartyOblezniczeGracza);
                    FiltrPanelPozogaManekin(form1.panelOblezniczeGracz1, gracz2.Plansza, Umiejscowienie.Obleznicza);
                    FiltrPanelPozogaManekin(form1.panelOblezniczeGracz2, gracz1.Plansza, Umiejscowienie.Obleznicza);


                }
                else if (k is KartaJednostki kj && kj.Effect == CardEffects.Bractwo)
                {
                    if (kj is KartaPiechoty)
                    {
                        FiltrPanelBractwo(gracz1.Plansza.KartyPiechotyGracza, kj, panelZwarcieGracz1);
                    }
                    else if (kj is KartaLucznika)
                    {
                        FiltrPanelBractwo(gracz1.Plansza.KartyStrzeleckieGracza, kj, panelDystansGracz1);
                    }
                    else if (kj is KartaObleznika)
                    {
                        FiltrPanelBractwo(gracz1.Plansza.KartyOblezniczeGracza, kj, panelOblezniczeGracz1);
                    }

                }
                else if (k is KartaDowodcy KD)
                {

                    FiltrPanelRogu(panelRoguOblezniczeGracz1, gracz1.Plansza, Umiejscowienie.Obleznicza);
                    FiltrPanelRogu(panelRoguOblezniczeGracz2, gracz2.Plansza, Umiejscowienie.Obleznicza);

                    FiltrPanelSpecjalne(panelWspolnePole, KD);
                    RefreshPanelSpecial(panelWspolnePole);
                }


                OdswiezPunktacje();
                RefreshCardPositions(sourcePanel);
                RefreshCardPositions(targetPanel);
                if (k is not Manekin)
                {
                    WykonanoRuch();
                }
            }
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
            OdswiezPunktacje();

            panelGracza.DragEnter += Panel_DragEnter;
            panelGracza.DragDrop += Panel_DragDrop;
            WykonanoRuch();
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


            UpdateCardStrengths(panelZwarcieGracz1);
            UpdateCardStrengths(panelDystansGracz1);
            UpdateCardStrengths(panelOblezniczeGracz1);
            UpdateCardStrengths(panelZwarcieGracz2);
            UpdateCardStrengths(panelDystansGracz2);
            UpdateCardStrengths(panelOblezniczeGracz2);


            UpdateCardStrengths(form1.panelZwarcieGracz1);
            UpdateCardStrengths(form1.panelDystansGracz1);
            UpdateCardStrengths(form1.panelOblezniczeGracz1);
            UpdateCardStrengths(form1.panelZwarcieGracz2);
            UpdateCardStrengths(form1.panelDystansGracz2);
            UpdateCardStrengths(form1.panelOblezniczeGracz2);

        }

        private void UpdateCardStrengths(Panel panel)
        {
            foreach (Control innerControl in panel.Controls)
            {
                if (innerControl is PictureBox pictureBox)
                {
                    foreach (Control pictureBoxControl in pictureBox.Controls)
                    {
                        if (pictureBoxControl is Label label && label.Text.Contains("["))
                        {
                            if (pictureBox.Tag is KartaJednostki kartaJednostki)
                            {
                                if (kartaJednostki.Sila < 10)
                                {
                                    label.Text = $"  [{kartaJednostki.Sila}]";
                                }
                                else
                                {
                                    label.Text = $" [{kartaJednostki.Sila}]";
                                }
                            }
                            break;
                        }
                    }
                }
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







        public void UpdateImgPoints()
        {
            if (gracz1.Punkty == 1)
            {
                pictureBoxDrugiPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxDrugiPunktGracz1.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragdpusty.PNG");
            }
            if (gracz2.Punkty == 1)
            {
                pictureBoxDrugiPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxDrugiPunktGracz2.Image = Image.FromFile("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\szmaragdpusty.PNG");

            }
            OdswiezPunktacje();

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
            labelInfo.Text = " Trwa Rozgrywka ...";

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
            if (panel.Controls.Contains(pictureBox))
            {
                panel.Controls.Remove(pictureBox);
                RefreshCardPositions(panel);
            }
        }


        private void RefreshCardPositions(Panel panel, int pictureBoxSpacing = 10)
        {
            if (panel.Controls.Count > 0)
            {
                int totalWidth = panel.Controls.Count * panel.Controls[0].Width + (panel.Controls.Count - 1) * pictureBoxSpacing;
                int panelWidth = panel.ClientSize.Width;
                int startX = (panelWidth - totalWidth) / 2;
                int startY = pictureBoxSpacing;
                if (totalWidth > panelWidth)
                {
                    pictureBoxSpacing = (panelWidth - panel.Controls.Count * panel.Controls[0].Width) / (panel.Controls.Count - 1);

                    startX = pictureBoxSpacing;
                }
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

        private void AddCardToPanel(Karta karta, Panel panel)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(60, 90);
            pictureBox.BackColor = Color.LightGray;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;




            Label nameLabel = new Label();
            nameLabel.Text = karta.Nazwa;
            nameLabel.AutoSize = false;
            nameLabel.Size = new Size(pictureBox.Width - 8, 25); // Dostosowanie rozmiaru etykiety
            nameLabel.Font = new Font("Arial", 8); // Dostosowanie rozmiaru czcionki
            nameLabel.TextAlign = ContentAlignment.BottomCenter; // Umiejscowienie tekstu na dole etykiety
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Dock = DockStyle.Bottom;

            Label rowLabel = new Label();
            rowLabel.Text = GetCardTypeLabel(karta);
            rowLabel.AutoSize = false;
            rowLabel.Size = new Size(pictureBox.Width - 8, 25); // Dostosowanie rozmiaru etykiety
            rowLabel.Font = new Font("Arial", 8, FontStyle.Bold); // Dostosowanie rozmiaru czcionki
            rowLabel.TextAlign = ContentAlignment.TopCenter; // Umiejscowienie tekstu na górze etykiety
            rowLabel.BackColor = Color.Transparent;
            rowLabel.Dock = DockStyle.Top;

            Label label = new Label();
            label.AutoSize = true;
            label.Size = new Size(25, 20);
            label.Font = new Font("Arial", 8);
            label.TextAlign = ContentAlignment.TopCenter;

            label.Location = new Point((pictureBox.Width - label.Width) / 2, (pictureBox.Height - label.Height) / 2);



            if (karta is KartaJednostki k)
            {
                if (k.Sila < 10)
                {
                    label.Text = $" [{k.Sila}]";

                }
                else
                {
                    label.Text = $"[{k.Sila}]";

                }

                if (k.KartaBohatera)
                {
                    pictureBox.BackColor = Color.LightYellow;
                }
                if (k.Effect == CardEffects.Bractwo)
                {
                    pictureBox.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox.ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);
                    };
                }
                else if (k.Effect == CardEffects.Wiez)
                {
                    pictureBox.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox.ClientRectangle, Color.Green, ButtonBorderStyle.Solid);
                    };
                }
                else if (k.Effect == CardEffects.WyskoieMorale)
                {
                    pictureBox.Paint += (sender, e) =>
                    {
                        ControlPaint.DrawBorder(e.Graphics, pictureBox.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
                    };
                }

            }

            pictureBox.Tag = karta;

            pictureBox.Controls.Add(label);
            pictureBox.Controls.Add(nameLabel);
            pictureBox.Controls.Add(rowLabel);

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.DoubleClick += null;


            panel.Controls.Add(pictureBox);

            RefreshCardPositions(panel);
        }

        private string GetCardTypeLabel(Karta karta)
        {
            if (karta is KartaLucznika)
            {
                return "Łucznik";
            }
            else if (karta is KartaPiechoty)
            {
                return "Piechota";
            }
            else if (karta is KartaObleznika)
            {
                return "Obleżnik";
            }
            else if (karta is KartaPogody)
            {
                return "Pogoda";
            }
            else if (karta is RogDowodcy)
            {
                return "Róg";
            }
            else if (karta is Manekin)
            {
                return "Manekin";
            }
            else if (karta is Pozoga)
            {
                return "Pozoga";
            }
            else if (karta is KartaDowodcy)
            {
                return "Dowódca";
            }
            else if (karta is KartaSpecjalna)
            {
                return "Specjalna";
            }

            return string.Empty;
        }





        private void button1_Click(object sender, EventArgs e)
        {
            gracz1.Play = false;

            this.Enabled = false;
            form1.Enabled = true;

            System.Diagnostics.Debug.WriteLine(gracz1.Imie + " gracz1 :  " + gracz1.Play);
            System.Diagnostics.Debug.WriteLine(gracz2.Imie + " gracz2 :  " + gracz2.Play);
            System.Diagnostics.Debug.WriteLine(gra.gracz1.Imie + "gra.gracz1 :  " + gra.gracz1.Play);
            System.Diagnostics.Debug.WriteLine(gra.gracz2.Imie + "gra.gracz2 :  " + gra.gracz2.Play);
            if (gracz2.Play == false)
            {
                gra.KonczRunde(gracz1);

                panelWspolnePole.Controls.Clear();
                panelRoguZwarcieGracz1.Controls.Clear();
                panelRoguDystansGracz1.Controls.Clear();
                panelRoguOblezniczeGracz1.Controls.Clear();
                panelOblezniczeGracz1.Controls.Clear();
                panelDystansGracz1.Controls.Clear();
                panelZwarcieGracz1.Controls.Clear();

                form1.panelWspolnePole.Controls.Clear();
                form1.panelRoguZwarcieGracz2.Controls.Clear();
                form1.panelRoguDystansGracz2.Controls.Clear();
                form1.panelRoguOblezniczeGracz2.Controls.Clear();
                form1.panelOblezniczeGracz2.Controls.Clear();
                form1.panelDystansGracz2.Controls.Clear();
                form1.panelZwarcieGracz2.Controls.Clear();

                form1.panelWspolnePole.Controls.Clear();
                form1.panelRoguZwarcieGracz1.Controls.Clear();
                form1.panelRoguDystansGracz1.Controls.Clear();
                form1.panelRoguOblezniczeGracz1.Controls.Clear();
                form1.panelOblezniczeGracz1.Controls.Clear();
                form1.panelDystansGracz1.Controls.Clear();
                form1.panelZwarcieGracz1.Controls.Clear();

                panelWspolnePole.Controls.Clear();
                panelRoguZwarcieGracz2.Controls.Clear();
                panelRoguDystansGracz2.Controls.Clear();
                panelRoguOblezniczeGracz2.Controls.Clear();
                panelOblezniczeGracz2.Controls.Clear();
                panelDystansGracz2.Controls.Clear();
                panelZwarcieGracz2.Controls.Clear();

                UpdateImgPoints();
                form1.UpdateImgPoints();

                gracz1.Play = true;
                gracz2.Play = true;


                if (gra.ostatniGracz == gracz1)
                {
                    gra.ostatniGracz = gracz2;
                }
                if (gra.ostatniGracz == gracz2)
                {
                    gra.ostatniGracz = gracz1;
                }

                panelRoguDystansGracz1.DragEnter += Panel_DragEnter;
                panelRoguDystansGracz1.DragDrop += Panel_DragDrop;
                panelRoguOblezniczeGracz1.DragEnter += Panel_DragEnter;
                panelRoguOblezniczeGracz1.DragDrop += Panel_DragDrop;
                panelRoguZwarcieGracz1.DragEnter += Panel_DragEnter;
                panelRoguZwarcieGracz1.DragDrop += Panel_DragDrop;

              
              
                PlaySound("D:\\Visual Studio Project My\\Gwent\\Gwent App\\img\\runda.wav");

           


                try
                {
                    gra.KonczRozgrywke();
                }
                catch (EndGameException ex)
                {
                    Thread.Sleep(2000);

                    MessageBox.Show(ex.Message, "Koniec Rozgrywki", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form1.Close();
                    this.Close();
                }
            }
        }
    }
}