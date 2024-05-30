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
    public partial class Form1 : Form
    {
        public WaveOutEvent outputDevice;
        public Player player1;
        public Player player2;
        public Form2 form2;
        public Game game;
        private PictureBox selectedPictureBox = null;
        private Card lastCard = null;
        string localPath;
        public Form1(Game g, Form2 f)
        {       
            player1 = g.player1;
            player2 = g.player2;
            game = g;
            form2 = f;
            
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] baseDirectoryTab = baseDirectory.Split("Gwent App");
            System.Diagnostics.Debug.WriteLine(baseDirectoryTab[0]);
            localPath = Path.Combine(baseDirectoryTab[0], "Gwent App", "LocalSources");
           
            InitializeComponent();
            InitDefaultInfoComponent(player1, player2);
            InitImgPlayer(player1, player2);
            PlayBackgroundMusic();
            Enabled = true;


            foreach (var cardToAdd in player1.playerBoard.PlayerCardsInGame)
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
            form2.Show();
        }

       public void PlayBackgroundMusic()
        {
            outputDevice = new WaveOutEvent();       
            AudioFileReader audioFile = new AudioFileReader(localPath + "\\gamemusic.wav");
            VolumeSampleProvider volumeProvider = new VolumeSampleProvider(audioFile.ToSampleProvider());
            volumeProvider.Volume = 0.08f;
            outputDevice.Init(volumeProvider);
            outputDevice.Play();
        }
        public void Drop()
        {
            PlaySound(localPath + "\\drop.wav");
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
            var card = selectedPictureBox.Tag;

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

                if (card is KartaPiechoty)
                {
                    panelZwarcieGracz1.AllowDrop = true;
                }
                else if (card is KartaLucznika)
                {
                    panelDystansGracz1.AllowDrop = true;
                }
                else if (card is KartaObleznika)
                {
                    panelOblezniczeGracz1.AllowDrop = true;
                }
                else if (card is Manekin)
                {
                    panelZwarcieGracz1.AllowDrop = false;
                    panelDystansGracz1.AllowDrop = false;
                    panelOblezniczeGracz1.AllowDrop = false;

                    foreach (Control control in panelZwarcieGracz1.Controls){
                        if (control is PictureBox pictureBox && pictureBox.Tag is CardWarrior)
                            panelZwarcieGracz1.AllowDrop = true;          
                    }
                    foreach (Control control in panelDystansGracz1.Controls){
                        if (control is PictureBox pictureBox && pictureBox.Tag is CardWarrior)
                            panelDystansGracz1.AllowDrop = true;           
                    }
                    foreach (Control control in panelOblezniczeGracz1.Controls) {
                        if (control is PictureBox pictureBox && pictureBox.Tag is CardWarrior)
                            panelOblezniczeGracz1.AllowDrop = true; 
                    }   
                }
                else if (card is RogDowodcy)
                { 
                    panelRoguDystansGracz1.AllowDrop = true;
                    panelRoguOblezniczeGracz1.AllowDrop = true;
                    panelRoguZwarcieGracz1.AllowDrop = true;
                }
                else if (card is CardCommander)
                {
                    panelSpecjalnaGracz1.AllowDrop = true;
                }
                else if (card is CardSpecial)
                {
                    panelWspolnePole.AllowDrop = true;
                }
                selectedPictureBox.DoDragDrop(selectedPictureBox, DragDropEffects.Move);
            }
        }

        private void NewMove()
        {
            SkopiujPictureBoxy(panelWspolnePole, form2.panelWspolnePole);
            SkopiujPictureBoxy(panelSpecjalnaGracz1, form2.panelSpecjalnaGracz2);
           
            SkopiujPictureBoxy(panelRoguZwarcieGracz1, form2.panelRoguZwarcieGracz2);
            SkopiujPictureBoxy(panelRoguDystansGracz1, form2.panelRoguDystansGracz2);
            SkopiujPictureBoxy(panelRoguOblezniczeGracz1, form2.panelRoguOblezniczeGracz2);

            SkopiujPictureBoxy(panelOblezniczeGracz1, form2.panelOblezniczeGracz2);
            SkopiujPictureBoxy(panelDystansGracz1, form2.panelDystansGracz2);
            SkopiujPictureBoxy(panelZwarcieGracz1, form2.panelZwarcieGracz2);

            SkopiujPictureBoxy(form2.panelOblezniczeGracz1,panelOblezniczeGracz2);
            SkopiujPictureBoxy(form2.panelDystansGracz1, panelDystansGracz2);
            SkopiujPictureBoxy(form2.panelZwarcieGracz1, panelZwarcieGracz2);


            if (player2.PlayerActiveInRound)
            {
                labelInfo.Text = "Ruch Gracza ...";
                form2.labelInfo.Text = "Twój Ruch !!!";

                this.Enabled = false;
                form2.Enabled = true;
            }
            RefreshScores();
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
                Card c = originalPictureBox.Tag as Card;
                AddCardToPanel(c, panelDocelowy);
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
                Card c = pictureBox.Tag as Card;

                sourcePanel.Controls.Remove(pictureBox);
                targetPanel.Controls.Add(pictureBox);

                if (c is RogDowodcy rg)
                {
                    if (targetPanel.Name.Contains("RoguDystans"))
                    {
                        rg.LocationCard = LocationCard.Lucznika;
                    }
                    else if (targetPanel.Name.Contains("RoguZwarcie"))
                    {
                        rg.LocationCard = LocationCard.Piechoty;
                    }
                    else if (targetPanel.Name.Contains("RoguObleznicze"))
                    {
                        rg.LocationCard = LocationCard.Obleznika;
                    }
                    targetPanel.DragEnter -= Panel_DragEnter;
                    targetPanel.DragDrop -= Panel_DragDrop;
                }
                else if (c is Manekin m)
                {           
                    if (targetPanel.Name.Contains("panelZwarcie"))
                    {
                        m.LocationCard = LocationCard.Piechoty;
                        lastCard = m;
                        foreach (Control control in panelZwarcieGracz1.Controls)
                        {
                            if (control is PictureBox pictureBoxa)
                            {
                                if (pictureBoxa.Tag is CardWarrior)
                                    pictureBoxa.DoubleClick += PictureBox_DoubleClick;
                            }
                        }
                    }
                    else if (targetPanel.Name.Contains("panelDystans"))
                    {
                        m.LocationCard = LocationCard.Lucznika;
                        lastCard = m;
                        foreach (Control control in panelDystansGracz1.Controls)
                        {
                            if (control is PictureBox pictureBoxa)
                            {
                                if (pictureBoxa.Tag is CardWarrior)
                                    pictureBoxa.DoubleClick += PictureBox_DoubleClick;
                            }
                        }
                    }
                    else if (targetPanel.Name.Contains("panelObleznicze"))
                    {
                        m.LocationCard = LocationCard.Obleznika;
                        lastCard = m;
                        foreach (Control control in panelOblezniczeGracz1.Controls)
                        {
                            if (control is PictureBox pictureBoxa)
                            {
                                if (pictureBoxa.Tag is CardWarrior)
                                    pictureBoxa.DoubleClick += PictureBox_DoubleClick;
                            }
                        }
                    }
                }

                if (c is not Manekin)
                {
                    try
                    {
                        game.MakeMove(player1, c);
                    }
                    catch (ExceptionDowodca ED)
                    {
                        MessageBox.Show(ED.Message, "Niezły z ciebie taktyk :) ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (RogException ER)
                    {
                        MessageBox.Show(ER.Message, "Róg zajęty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }      
                }
                if (c is Pozoga)
                {
                    FilterPanelPozoga(panelZwarcieGracz1, player1.playerBoard.CardsPiechotyPlayer);
                    FilterPanelPozoga(panelZwarcieGracz2, player2.playerBoard.CardsPiechotyPlayer);
                    FilterPanelManekin(panelZwarcieGracz1, player1.playerBoard, LocationCard.Piechoty);
                    FilterPanelManekin(panelZwarcieGracz2, player2.playerBoard, LocationCard.Piechoty);

                    FilterPanelPozoga(panelDystansGracz2, player2.playerBoard.CardsStrzeleckiePlayer);
                    FilterPanelPozoga(panelDystansGracz1, player1.playerBoard.CardsStrzeleckiePlayer);
                    FilterPanelManekin(panelDystansGracz1, player1.playerBoard, LocationCard.Lucznika);
                    FilterPanelManekin(panelDystansGracz2, player2.playerBoard, LocationCard.Lucznika);

                    FilterPanelPozoga(panelOblezniczeGracz1, player1.playerBoard.CardsOblezniczePlayer);
                    FilterPanelPozoga(panelOblezniczeGracz2, player2.playerBoard.CardsOblezniczePlayer);
                    FilterPanelManekin(panelOblezniczeGracz1, player1.playerBoard, LocationCard.Obleznika);
                    FilterPanelManekin(panelOblezniczeGracz2, player2.playerBoard, LocationCard.Obleznika);


                    FilterPanelPozoga(form2.panelZwarcieGracz1, player2.playerBoard.CardsPiechotyPlayer);
                    FilterPanelPozoga(form2.panelZwarcieGracz2, player1.playerBoard.CardsPiechotyPlayer);
                    FilterPanelManekin(form2.panelZwarcieGracz1, player2.playerBoard, LocationCard.Piechoty);
                    FilterPanelManekin(form2.panelZwarcieGracz2, player1.playerBoard, LocationCard.Piechoty);

                    FilterPanelPozoga(form2.panelDystansGracz2, player1.playerBoard.CardsStrzeleckiePlayer);
                    FilterPanelPozoga(form2.panelDystansGracz1, player2.playerBoard.CardsStrzeleckiePlayer);
                    FilterPanelManekin(form2.panelDystansGracz1, player2.playerBoard, LocationCard.Lucznika);
                    FilterPanelManekin(form2.panelDystansGracz2, player1.playerBoard, LocationCard.Lucznika);

                    FilterPanelPozoga(form2.panelOblezniczeGracz1, player2.playerBoard.CardsOblezniczePlayer);
                    FilterPanelPozoga(form2.panelOblezniczeGracz2, player1.playerBoard.CardsOblezniczePlayer);
                    FilterPanelManekin(form2.panelOblezniczeGracz1, player2.playerBoard, LocationCard.Obleznika);
                    FilterPanelManekin(form2.panelOblezniczeGracz2, player1.playerBoard, LocationCard.Obleznika);
                }
                else if (c is CardWarrior cw && cw.Effect == CardEffects.Braterstwo)
                {
                    if (cw is KartaPiechoty)
                    {
                        FilterPanelBractwo(player1.playerBoard.CardsPiechotyPlayer, cw, panelZwarcieGracz1);
                    }
                    else if (cw is KartaLucznika)
                    {
                        FilterPanelBractwo(player1.playerBoard.CardsStrzeleckiePlayer, cw, panelDystansGracz1);
                    }
                    else if (cw is KartaObleznika)
                    {
                        FilterPanelBractwo(player1.playerBoard.CardsOblezniczePlayer, cw, panelOblezniczeGracz1);
                    }    
                }
                else if (c is CardCommander cc)
                {
                    FilterPanelRogu(panelRoguOblezniczeGracz1, player1.playerBoard, LocationCard.Obleznika);
                    FilterPanelRogu(panelRoguOblezniczeGracz2, player2.playerBoard, LocationCard.Obleznika);

                    AddCardCommanderPanel(panelWspolnePole, cc);
                    RefreshPanelSpecial(panelWspolnePole);
                }

                RefreshScores();
                RefreshCardPositions(sourcePanel);
                RefreshCardPositions(targetPanel);
                if (c is not Manekin)
                {
                    NewMove();
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
                    KartaPiechoty card = selectedPictureBox.Tag as KartaPiechoty;
                    game.MakeMove<KartaPiechoty>(player1, lastCard, card);
                    RemoveCardFromPanel(selectedPictureBox, sourcePanel);
                    AddCardToPanel(card, panelGracza);
                }
                else if (sourcePanel.Name.Contains("panelDystans"))
                {
                    KartaLucznika card = selectedPictureBox.Tag as KartaLucznika;
                    game.MakeMove<KartaLucznika>(player1, lastCard, card);
                    RemoveCardFromPanel(selectedPictureBox, sourcePanel);
                    AddCardToPanel(card, panelGracza);
                }
                else if (sourcePanel.Name.Contains("panelObleznicze"))
                {
                    KartaObleznika card = selectedPictureBox.Tag as KartaObleznika;
                    game.MakeMove<KartaObleznika>(player1, lastCard, card);
                    RemoveCardFromPanel(selectedPictureBox, sourcePanel);
                    AddCardToPanel(card, panelGracza);
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
            RefreshScores();
  
            panelGracza.DragEnter += Panel_DragEnter;
            panelGracza.DragDrop += Panel_DragDrop;
            NewMove();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player1.PlayerActiveInRound = false;

            this.Enabled = false;
            form2.Enabled = true;

            if (player2.PlayerActiveInRound == false)
            {
                game.RundEnd(player1);

                panelWspolnePole.Controls.Clear();
                panelRoguZwarcieGracz1.Controls.Clear();
                panelRoguDystansGracz1.Controls.Clear();
                panelRoguOblezniczeGracz1.Controls.Clear();
                panelOblezniczeGracz1.Controls.Clear();
                panelDystansGracz1.Controls.Clear();
                panelZwarcieGracz1.Controls.Clear();

                form2.panelWspolnePole.Controls.Clear();
                form2.panelRoguZwarcieGracz2.Controls.Clear();
                form2.panelRoguDystansGracz2.Controls.Clear();
                form2.panelRoguOblezniczeGracz2.Controls.Clear();
                form2.panelOblezniczeGracz2.Controls.Clear();
                form2.panelDystansGracz2.Controls.Clear();
                form2.panelZwarcieGracz2.Controls.Clear();

                form2.panelWspolnePole.Controls.Clear();
                form2.panelRoguZwarcieGracz1.Controls.Clear();
                form2.panelRoguDystansGracz1.Controls.Clear();
                form2.panelRoguOblezniczeGracz1.Controls.Clear();
                form2.panelOblezniczeGracz1.Controls.Clear();
                form2.panelDystansGracz1.Controls.Clear();
                form2.panelZwarcieGracz1.Controls.Clear();

                panelWspolnePole.Controls.Clear();
                panelRoguZwarcieGracz2.Controls.Clear();
                panelRoguDystansGracz2.Controls.Clear();
                panelRoguOblezniczeGracz2.Controls.Clear();
                panelOblezniczeGracz2.Controls.Clear();
                panelDystansGracz2.Controls.Clear();
                panelZwarcieGracz2.Controls.Clear();

                UpdateImgPoints();
                form2.UpdateImgPoints();

                player1.PlayerActiveInRound = true;
                player2.PlayerActiveInRound = true;


                if (game.lastplayer == player1)
                {
                    game.lastplayer = player2;
                }
                if (game.lastplayer == player2)
                {
                    game.lastplayer = player1;
                }

                panelRoguDystansGracz1.DragEnter += Panel_DragEnter;
                panelRoguDystansGracz1.DragDrop += Panel_DragDrop;
                panelRoguOblezniczeGracz1.DragEnter += Panel_DragEnter;
                panelRoguOblezniczeGracz1.DragDrop += Panel_DragDrop;
                panelRoguZwarcieGracz1.DragEnter += Panel_DragEnter;
                panelRoguZwarcieGracz1.DragDrop += Panel_DragDrop;

                PlaySound(localPath + "\\runda.wav");
                try
                {
                    game.GameEnd();
                }
                catch (EndGameException ex)
                {
                    Thread.Sleep(2000);
                    MessageBox.Show(ex.Message, "Koniec Rozgrywki !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form2.Close();
                    this.Close();

                }
            }
        }

        private void FilterPanelBractwo<T>(Deck<T> deck, CardWarrior cw, Panel panel) where T : CardWarrior
        {
            var cards = deck.Where(karta => karta is CardWarrior && karta.Name == cw.Name && cw.Effect == CardEffects.Braterstwo);
            foreach (CardWarrior c in cards)
            {
                if (FindPictureBoxByCard(panel, c) == null)
                {
                    AddCardToPanel(c, panel);
                    RemoveCardFromPanel(FindPictureBoxByCard(panelGracza, c), panelGracza);
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
        private void AddCardCommanderPanel(Panel panel, CardCommander cardCommander)
        {
            if (cardCommander is FoltestDowódcaPółnocy)
            {
                AddCardToPanel(new CzysteNiebo("Czyste Niebo Krola Temerii"), panel);
            }
            if (cardCommander is FoltestKrólTemerii)
            {
                AddCardToPanel(new GestaMgla("Gesta Mgla Krola Temerii"), panel);
            }        
        }
        private void FilterPanelRogu(Panel panel, PlayerBoard playerBoard, LocationCard locationCard)
        {
            panel.Controls.Clear();
            var cards = playerBoard.CardsSpecial.Where(c => (c is RogDowodcy rg && rg.LocationCard == locationCard));

            if (cards.Any())
            {
                foreach (var card in cards)
                {
                    AddCardToPanel(card, panel);
                }
                panel.DragEnter -= Panel_DragEnter;
                panel.DragDrop -= Panel_DragDrop;
            }
        }
        private void FilterPanelManekin(Panel panel, PlayerBoard playerBoard, LocationCard locationCard)
        {
            var cards = playerBoard.CardsSpecial.Where(c => (c is Manekin m && m.LocationCard == locationCard));
            if (cards.Any())
            {
                foreach (var card in cards)
                {
                    AddCardToPanel(card, panel);
                }
            }
        }
        private void FilterPanelPozoga<T>(Panel panel, Deck<T> deck) where T : CardWarrior
        {
            panel.Controls.Clear();
            foreach (Card card in deck)
            {
                AddCardToPanel(card, panel);
            }
        }

        private void RefreshScores()
        {
            labelDystansGracz1.Text = player1.playerBoard.PointsStrzeleckie + "";
            labelZwarcieGracz1.Text = player1.playerBoard.PointsPiechoty + "";
            labelOblezniczeGracz1.Text = player1.playerBoard.PointsObleznicze + "";
            labelPunktySumaGracz1.Text = player1.playerBoard.PointsSum + "";

            labelDystansGracz2.Text = player2.playerBoard.PointsStrzeleckie + "";
            labelZwarcieGracz2.Text = player2.playerBoard.PointsPiechoty + "";
            labelOblezniczeGracz2.Text = player2.playerBoard.PointsObleznicze + "";
            labelPunktySumaGracz2.Text = player2.playerBoard.PointsSum + "";

            form2.labelDystansGracz2.Text = player1.playerBoard.PointsStrzeleckie + "";
            form2.labelZwarcieGracz2.Text = player1.playerBoard.PointsPiechoty + "";
            form2.labelOblezniczeGracz2.Text = player1.playerBoard.PointsObleznicze + "";
            form2.labelPunktySumaGracz2.Text = player1.playerBoard.PointsSum + "";

            form2.labelDystansGracz1.Text = player2.playerBoard.PointsStrzeleckie + "";
            form2.labelZwarcieGracz1.Text = player2.playerBoard.PointsPiechoty + "";
            form2.labelOblezniczeGracz1.Text = player2.playerBoard.PointsObleznicze + "";
            form2.labelPunktySumaGracz1.Text = player2.playerBoard.PointsSum + "";

            UpdateCardForcePanel(panelZwarcieGracz1);
            UpdateCardForcePanel(panelDystansGracz1);
            UpdateCardForcePanel(panelOblezniczeGracz1);
            UpdateCardForcePanel(panelZwarcieGracz2);
            UpdateCardForcePanel(panelDystansGracz2);
            UpdateCardForcePanel(panelOblezniczeGracz2);

            UpdateCardForcePanel(form2.panelZwarcieGracz1);
            UpdateCardForcePanel(form2.panelDystansGracz1);
            UpdateCardForcePanel(form2.panelOblezniczeGracz1);
            UpdateCardForcePanel(form2.panelZwarcieGracz2);
            UpdateCardForcePanel(form2.panelDystansGracz2);
            UpdateCardForcePanel(form2.panelOblezniczeGracz2);

        }
        private void UpdateCardForcePanel(Panel panel)
        {
            foreach (Control innerControl in panel.Controls)
            {
                if (innerControl is PictureBox pictureBox)
                {
                    foreach (Control pictureBoxControl in pictureBox.Controls)
                    {
                        if (pictureBoxControl is Label label && label.Text.Contains("["))
                        {
                            if (pictureBox.Tag is CardWarrior kartaJednostki)
                            {
                                if (kartaJednostki.Force < 10)
                                {
                                    label.Text = $"  [{kartaJednostki.Force}]";
                                }
                                else
                                {
                                    label.Text = $" [{kartaJednostki.Force}]";
                                }
                            }
                            break; 
                        }
                    }
                }
            }
        }

        private PictureBox FindPictureBoxByCard<T>(Panel panel, T card) where T : Card
        {
            foreach (PictureBox pictureBox in panel.Controls.OfType<PictureBox>())
            {
                T pictureBoxCard = pictureBox.Tag as T;
                if (pictureBoxCard != null && pictureBoxCard.Equals(card))
                {
                    return pictureBox;
                }
            }
            return null;
        }
        public void UpdateImgPoints()
        {
            if (player1.PlayerPoints == 1)
            {
                pictureBoxDrugiPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxDrugiPunktGracz1.Image = Image.FromFile(localPath + "\\szmaragdpusty.PNG");
            }
            if (player2.PlayerPoints == 1)
            {
                pictureBoxDrugiPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxDrugiPunktGracz2.Image = Image.FromFile(localPath + "\\szmaragdpusty.PNG");

            }
        }

        private void InitImgPlayer(Player player1, Player player2)
        {
            pictureBoxZdjecieGracz1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxZdjecieGracz1.Image = Image.FromFile(localPath + "\\Geralt.PNG");
            pictureBoxZdjecieGracz2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxZdjecieGracz2.Image = Image.FromFile(localPath + "\\Geralt.PNG");
        }
        private void InitDefaultInfoComponent(Player player1, Player player2)
        {
            labelInfo.Text = " Trwa Rozgrywka ...";

            labelImieGracz1.Text = player1.Name;
            labelImieGracz2.Text = player2.Name;
  

            pictureBoxDrugiPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPierwszyPunktGracz1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxDrugiPunktGracz1.Image = Image.FromFile(localPath + "\\szmaragd.PNG");
            pictureBoxPierwszyPunktGracz1.Image = Image.FromFile(localPath + "\\szmaragd.PNG");
            pictureBoxDrugiPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPierwszyPunktGracz2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxDrugiPunktGracz2.Image = Image.FromFile(localPath + "\\szmaragd.PNG");
            pictureBoxPierwszyPunktGracz2.Image = Image.FromFile(localPath + "\\szmaragd.PNG");

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

        private void AddCardToPanel(Card karta, Panel panel)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(60, 90);
            pictureBox.BackColor = Color.LightGray;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
                
            Label nameLabel = new Label();
            nameLabel.Text = karta.Name;
            nameLabel.AutoSize = false;
            nameLabel.Size = new Size(pictureBox.Width - 8, 25);
            nameLabel.Font = new Font("Arial", 8); 
            nameLabel.TextAlign = ContentAlignment.BottomCenter; 
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Dock = DockStyle.Bottom;

            Label rowLabel = new Label();
            rowLabel.Text = GetCardTypeLabel(karta);
            rowLabel.AutoSize = false;
            rowLabel.Size = new Size(pictureBox.Width - 8, 25); 
            rowLabel.Font = new Font("Arial", 8, FontStyle.Bold); 
            rowLabel.TextAlign = ContentAlignment.TopCenter; 
            rowLabel.BackColor = Color.Transparent;
            rowLabel.Dock = DockStyle.Top;

            Label label = new Label();
            label.AutoSize = true;
            label.Size = new Size(25, 20);
            label.Font = new Font("Arial", 8);
            label.TextAlign = ContentAlignment.TopCenter;

            label.Location = new Point((pictureBox.Width - label.Width) / 2,(pictureBox.Height - label.Height) / 2);

            if (karta is CardWarrior k)
            {
                if (k.Force < 10)
                {
                    label.Text = $" [{k.Force}]";
                }
                else
                {
                    label.Text = $"[{k.Force}]"; 
                }

                if (k.IsHero)
                {
                    pictureBox.BackColor = Color.LightYellow;   
                }
                if(k.Effect == CardEffects.Braterstwo)
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
                else if (k.Effect == CardEffects.WysokieMorale)
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
        private string GetCardTypeLabel(Card karta)
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
            else if (karta is CardWeather)
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
            else if (karta is CardCommander)
            {
                return "Dowódca";
            }
            else if (karta is CardSpecial)
            {
                return "Specjalna";
            }

            return "";
        }
    }
}