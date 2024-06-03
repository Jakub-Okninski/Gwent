using Gwent_Library.Karty;
using Gwent_Library.Karty.KartyDowodcow;
using Gwent_Library.TypyKart;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Gwent_Library
{
    public class Game
    {
        public Player player1 { get; set; }
        public Player player2 { get; set; }
        public Player lastplayer { get; set; }
        public Game(Player p1, Player p2)
        {
            System.Diagnostics.Debug.WriteLine("Start Game");

            if (p1.AllCardsPlayer.Count < 20)
            {
                throw new CardException("Zbyt mało kart, gracz: "+p1.Name);
            }
            if (p2.AllCardsPlayer.Count < 20)
            {
                throw new CardException("Zbyt mało kart, gracz: " + p2.Name);

            }
            player1 = p1;
            player2 = p2;
            lastplayer = player2;
        }

        public void CheckMove (Player player)
        {
            if(player1.PlayerActiveInRound && player2.PlayerActiveInRound)
            {
                if (player != lastplayer)
                {
                    lastplayer = player;
                }else
                {
                    throw new MoveException("Nie twój ruch!");
                }
            }else if (player.PlayerActiveInRound)
            {
                if (player == player1)
                {
                    lastplayer = player2;
                }
                else if (player == player2) 
                {
                    lastplayer = player1;
                }
            }     
        }

        public void RundEnd(Player player)
        {
            if (!player1.PlayerActiveInRound && !player1.PlayerActiveInRound)
            {
                CountPoints(player1);
                CountPoints(player2);
                if (player1.playerBoard.PointsSum > player2.playerBoard.PointsSum)
                {
                    player2.PlayerPoints--;
                }
                else if (player1.playerBoard.PointsSum < player2.playerBoard.PointsSum)
                {
                    player1.PlayerPoints--;
                }
                else if (player1.playerBoard.PointsSum == player2.playerBoard.PointsSum)
                {
                    if (player1 == player)
                    {
                        player2.PlayerPoints--;
                    }
                    else
                    {
                        player1.PlayerPoints--;
                    }
                }
                player1.playerBoard.ResetBoard();
                player2.playerBoard.ResetBoard();
            }
        }


        public Player ReturnWinner()
        {
           
            if (player1.PlayerPoints > 0 && player2.PlayerPoints > 0)
            {
                throw new EndGameException("Rozgrywka się nie zakończyła !");
            }
            if (player1.PlayerPoints == 0)
            {
                return player2;
            }
            else if (player2.PlayerPoints == 0)
            {
                return player1;
            }
            else
            {
                return null;
            }
        }

        public void GameEnd()
        {
            if (player1.PlayerPoints == 0)
            {
                throw new EndGameException(player2.Name + " Wygrał !!!");
            }
            else if (player2.PlayerPoints == 0)
            {
                throw new EndGameException(player1.Name + " Wygrał !!!");
            }  
        }

        public void MakeMove<T>(Player player, Card card, T replaceCard) where T : CardWarrior
        {
            CheckMove(player);
            card.PutCard(player.playerBoard);

            if (card is Manekin M)
            {
                 M.ManekinAction<T>(player, replaceCard);
            }

            CountPoints(player);
    
        }

        public void MakeMove(Player player, Card card)
        {
            CheckMove(player);
            card.PutCard(player.playerBoard);

            if (card is Pozoga P)
            {
                P.GlobalActionBoard(player1, player2);
            }
            else if (card is CardCommander D)
            {
                if(D is FoltestZdobywca)
                {
                    D.GlobalActionBoard(player, player2);
                }
                else
                {
                    D.GlobalActionBoard(player1, player2);
                }
            }
            CountPoints(player);
        }
      
        public void ActionWeather()
        {
            var cards = player1.playerBoard.CardsSpecial.Where(card => card is CardWeather);        
            if (cards.Any())
            {
                foreach (CardWeather card in cards)
                {
                    card.GlobalActionBoard(player1, player2);
                    if (card is CzysteNiebo)
                        break;
                }
                
            }
            var cards2 = player2.playerBoard.CardsSpecial.Where(card => card is CardWeather);
            if (cards2.Any())
            {
                foreach (CardWeather card in cards2)
                {
                    card.GlobalActionBoard(player1, player2);
                    if (card is CzysteNiebo)
                        break;
                }

            }
        }
        public void ActionRog(Player player)
        {
            var cards = player.playerBoard.CardsSpecial.Where(karta => karta is RogDowodcy);
            if (cards.Any())
            {
                foreach (RogDowodcy card in cards)
                {
                    card.ActionRog(player, card.LocationCard);
                }
            }
        }

        private void CountPoints(Player player) {
            ResetPoints(player);
            ActionWeather();
            ActionRog(player);
            CountPoints();
        }
        public void CountPoints()
        {
            player1.playerBoard.PointsPiechoty = CountTotalPoints(player1.playerBoard.CardsPiechotyPlayer);
            player2.playerBoard.PointsPiechoty = CountTotalPoints(player2.playerBoard.CardsPiechotyPlayer);
            player1.playerBoard.PointsStrzeleckie = CountTotalPoints(player1.playerBoard.CardsStrzeleckiePlayer);
            player2.playerBoard.PointsStrzeleckie = CountTotalPoints(player2.playerBoard.CardsStrzeleckiePlayer);
            player1.playerBoard.PointsObleznicze = CountTotalPoints(player1.playerBoard.CardsOblezniczePlayer);
            player2.playerBoard.PointsObleznicze = CountTotalPoints(player2.playerBoard.CardsOblezniczePlayer);
            RefreshSumPoints();
        }

        private int CountTotalPoints<T>(Deck<T> deck) where T : CardWarrior
        {
            return deck.Any() ? deck.Sum(card => card.Force) : 0;
        }

        private void ResetPoints(Player player)
        {
            SetDefaultCardValue(player.playerBoard.CardsPiechotyPlayer);
            SetDefaultCardValue(player.playerBoard.CardsStrzeleckiePlayer);
            SetDefaultCardValue(player.playerBoard.CardsOblezniczePlayer);
        }

        private void SetDefaultCardValue<T>(Deck<T> deck) where T : CardWarrior
        {
            deck.ForEach(card => card.setDefaultForce());
        }
      
        private void RefreshSumPoints()
        {
            player1.playerBoard.PointsSum = player1.playerBoard.PointsPiechoty + player1.playerBoard.PointsStrzeleckie + player1.playerBoard.PointsObleznicze;
            player2.playerBoard.PointsSum = player2.playerBoard.PointsPiechoty + player2.playerBoard.PointsStrzeleckie + player2.playerBoard.PointsObleznicze;
        }
        public static Deck<Card> GenerateCommanderCard()
        {
            Deck<Card> deck = new Deck<Card>();
            Card FoltestDowódcaPółnocy = new FoltestDowódcaPółnocy("Foltest Dowódca Północy");
            Card FoltestKrólTemerii = new FoltestKrólTemerii("Foltest Król Temerii");
            Card FoltestZdobywca = new FoltestZdobywca("Foltest Zdobywca");
            deck.Add(FoltestDowódcaPółnocy);
            deck.Add(FoltestKrólTemerii);
            deck.Add(FoltestZdobywca);
            return deck;
        }
            public static Deck<Card> GenerateCard()
        {
            Deck<Card> deck = new Deck<Card>();
            Card Balista1 = new KartaObleznika("Balista", 6, false, CardEffects.Braterstwo);
            Card Balista2 = new KartaObleznika("Balista", 6, false, CardEffects.Braterstwo);
            Card BiednaPiechota = new KartaPiechoty("Biedna Piechota", 1, false, CardEffects.Wiez);
            Card BiednaPiechota2 = new KartaPiechoty("Biedna Piechota", 1, false, CardEffects.Wiez);
            Card BiednaPiechota3 = new KartaPiechoty("Biedna Piechota", 1, false, CardEffects.Wiez);
            Card Detmold = new KartaLucznika("Detmold", 6, false, CardEffects.Default);
            Card Esterad = new KartaPiechoty("Esterad Thyssen", 10, false, CardEffects.Default);
            Card Filippa = new KartaObleznika("Filippa Eilhart", 10, false, CardEffects.Default);
            Card JanNatalis = new KartaPiechoty("Jan Natalis", 10, false, CardEffects.Default);
            Card Katapulta1 = new KartaObleznika("Katapulta", 8, false, CardEffects.Wiez);
            Card Katapulta2 = new KartaObleznika("Katapulta", 8, false, CardEffects.Wiez);
            Card KeiraMetz = new KartaLucznika("Keira Metz", 5, false, CardEffects.Default);
            Card Komandos1 = new KartaPiechoty("Komandos Niebieskich Pasów", 4, false, CardEffects.Wiez);
            Card Komandos2 = new KartaPiechoty("Komandos Niebieskich Pasów", 4, false, CardEffects.Wiez);
            Card Komandos3 = new KartaPiechoty("Komandos Niebieskich Pasów", 4, false, CardEffects.Wiez);
            Card KsiążęStennis = new KartaPiechoty("Książę Stennis", 5, false, CardEffects.Default);
            Card MistrzOblezeń1 = new KartaPiechoty("Mistrz oblężeń z Kaedwen", 1, false, CardEffects.WysokieMorale);
            Card MistrzOblezeń2 = new KartaPiechoty("Mistrz oblężeń z Kaedwen", 1, false, CardEffects.WysokieMorale);
            Card MistrzOblezeń3 = new KartaPiechoty("Mistrz oblężeń z Kaedwen", 1, false, CardEffects.WysokieMorale);
            Card RedańskiPiechur1 = new KartaPiechoty("Redański piechur", 1, false, CardEffects.Default);
            Card RedańskiPiechur2 = new KartaPiechoty("Redański piechur", 1, false, CardEffects.Default);
            Card Rębacze1 = new KartaLucznika("Rębacze z Crinfrid", 5, false, CardEffects.Wiez);
            Card Rębacze2 = new KartaLucznika("Rębacze z Crinfrid", 5, false, CardEffects.Wiez);
            Card Rębacze3 = new KartaLucznika("Rębacze z Crinfrid", 5, false, CardEffects.Wiez);
            Card SabrinaGlevissig = new KartaPiechoty("Sabrina Glevissig", 4, false, CardEffects.Default);
            Card ShealaDeTancarville = new KartaPiechoty("Sheala de Tancarville", 5, false, CardEffects.Default);
            Card SheldonSkaggs = new KartaPiechoty("Sheldon Skaggs", 4, false, CardEffects.Default);
            Card SigismundDijkstra = new KartaLucznika("Sigismund Dijkstra", 4, false, CardEffects.Default);
            Card Talar = new KartaObleznika("Talar", 1, false,  CardEffects.Default);
            Card Trebusz1 = new KartaObleznika("Trebusz", 6, false, CardEffects.Braterstwo);
            Card Trebusz2 = new KartaObleznika("Trebusz", 6, false, CardEffects.Braterstwo);
            Card WieżaObleżnicza = new KartaObleznika("Wieża oblężnicza", 6, false, CardEffects.Default);
            Card VernonRoche = new KartaPiechoty("Vernon Roche", 10, false, CardEffects.Default);
            Card Ves = new KartaPiechoty("Ves", 5, false, CardEffects.Default);
            Card YarpenZigrin = new KartaPiechoty("Yarpen Zigrin", 2, false, CardEffects.Default);
            Card ZygfrydZDenesle = new KartaPiechoty("Zygfryd z Denesle", 5, false, CardEffects.Default);

            Card Manekin1 = new Manekin("Manekin", LocationCard.Piechoty);
            Card Rog1 = new RogDowodcy("Rog Dowodcy", LocationCard.Piechoty);
            Card Pozoga1 = new Pozoga("Pozoga");
            Card TrzaskającyMroz1 = new TrzaskającyMroz("Trzaskający Mroz");
            Card GestaMgla1 = new GestaMgla("Gesta Mgla");
            Card UlewnyDeszcz1 = new UlewnyDeszcz("Ulewny Deszcz");
            Card CzysteNiebo1 = new CzysteNiebo("Czyste Niebo");

 
            deck.Add(Balista1);
            deck.Add(Balista2);
            deck.Add(BiednaPiechota);
            deck.Add(BiednaPiechota2);
            deck.Add(BiednaPiechota3);
            deck.Add(Detmold);
            deck.Add(Esterad);
            deck.Add(Filippa);
            deck.Add(JanNatalis);
            deck.Add(Katapulta1);
            deck.Add(Katapulta2);
            deck.Add(KeiraMetz);
            deck.Add(Komandos1);
            deck.Add(Komandos2);
            deck.Add(Komandos3);
            deck.Add(KsiążęStennis);
            deck.Add(MistrzOblezeń1);
            deck.Add(MistrzOblezeń2);
            deck.Add(MistrzOblezeń3);
            deck.Add(RedańskiPiechur1);
            deck.Add(RedańskiPiechur2);
            deck.Add(Rębacze1);
            deck.Add(Rębacze2);
            deck.Add(Rębacze3);
            deck.Add(SabrinaGlevissig);
            deck.Add(ShealaDeTancarville);
            deck.Add(SheldonSkaggs);
            deck.Add(SigismundDijkstra);
            deck.Add(Talar);
            deck.Add(Trebusz1);
            deck.Add(Trebusz2);
            deck.Add(WieżaObleżnicza);
            deck.Add(VernonRoche);
            deck.Add(Ves);
            deck.Add(YarpenZigrin);
            deck.Add(ZygfrydZDenesle);
            deck.Add(Manekin1);
            deck.Add(Rog1);
            deck.Add(Pozoga1);
            deck.Add(TrzaskającyMroz1);
            deck.Add(GestaMgla1);
            deck.Add(UlewnyDeszcz1);
            deck.Add(CzysteNiebo1);


            Card Cirilla = new KartaPiechoty("Cirilla", 15, true, CardEffects.Default);
            Card Emiel = new KartaPiechoty("Emiel Regis", 5, true, CardEffects.Default);
            Card Geralt = new KartaPiechoty("Geralt", 15, true, CardEffects.Default);
            Card Jaskier = new KartaPiechoty("Jaskier", 2, true, CardEffects.WysokieMorale);
            Card Triss = new KartaPiechoty("Triss", 7, true, CardEffects.Default);
            Card Vesemir = new KartaPiechoty("Vesemir", 6, true, CardEffects.Default);
            Card Villentretenmerth = new KartaPiechoty("Villentretenmerth", 7, true, CardEffects.Default);
            Card Yennefer = new KartaLucznika("Yennefer", 7, true, CardEffects.Default);
            Card Zoltan = new KartaPiechoty("Zoltan", 5, true, CardEffects.Default);

            Card Manekin2 = new Manekin("Manekin", LocationCard.Piechoty);
            Card Manekin3 = new Manekin("Manekin", LocationCard.Piechoty);
            Card Rog2 = new RogDowodcy("Rog Dowodcy", LocationCard.Piechoty);
            Card Rog3 = new RogDowodcy("Rog Dowodcy", LocationCard.Piechoty);
            Card Pozoga2 = new Pozoga("Pozoga");
            Card Pozoga3 = new Pozoga("Pozoga");

            Card TrzaskającyMroz2 = new TrzaskającyMroz("Trzaskający Mroz");
            Card TrzaskającyMroz3 = new TrzaskającyMroz("Trzaskający Mroz");
            Card GestaMgla2 = new GestaMgla("Gesta Mgla");
            Card GestaMgla3 = new GestaMgla("Gesta Mgla");
            Card UlewnyDeszcz2 = new UlewnyDeszcz("Ulewny Deszcz");
            Card UlewnyDeszcz3 = new UlewnyDeszcz("Ulewny Deszcz");
            Card CzysteNiebo2 = new CzysteNiebo("Czyste Niebo");

            deck.Add(Cirilla);
            deck.Add(Pozoga2);
            deck.Add(TrzaskającyMroz2);
            deck.Add(GestaMgla2);
            deck.Add(UlewnyDeszcz3);
            deck.Add(Emiel);
            deck.Add(Geralt);
            deck.Add(Jaskier);
            deck.Add(Triss);
            deck.Add(Vesemir);
            deck.Add(Villentretenmerth);
            deck.Add(Yennefer);
            deck.Add(Zoltan);
            deck.Add(Manekin2);
            deck.Add(Manekin3);
            deck.Add(Rog2);
            deck.Add(Rog3);
            deck.Add(Pozoga3);
            deck.Add(TrzaskającyMroz3);
            deck.Add(GestaMgla3);
            deck.Add(UlewnyDeszcz2);
            deck.Add(CzysteNiebo2);

            return deck;
        }
        public static Deck<Card> GenerateRandomCard(Deck<Card> deck, int n)
        {
            if (n > deck.Count)
            {
                throw new ArgumentException("Ilość elementów do zwrócenia nie może być większa niż długość talii");
            }

            Random rand = new Random();
            Deck<Card> selectedCards = new Deck<Card>();
            HashSet<int> selectedIndices = new HashSet<int>();

            while (selectedCards.Count < n)
            {
                int index = rand.Next(deck.Count);
                if (selectedIndices.Add(index))
                {
                    selectedCards.Add(deck[index]);
                    deck.Remove(deck[index]);
                }
            }

            return selectedCards;
        }
  
    }
}
