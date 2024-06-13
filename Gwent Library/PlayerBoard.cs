using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class PlayerBoard
    {
        public Deck<Card> PlayerRestOfCards { get; set; }
        public Deck<Card> PlayerCardsInGame { get; set; }
        public Deck<KartaPiechoty> CardsPiechotyPlayer { get; set; }
        public Deck<KartaLucznika> CardsStrzeleckiePlayer { get; set; }
        public Deck<KartaObleznika> CardsOblezniczePlayer { get; set; }
        public Deck<CardSpecial> CardsSpecial { get; set; }
        public int PointsSum { get; set; }
        public int PointsPiechoty { get; set; }
        public int PointsStrzeleckie { get; set; }
        public int PointsObleznicze { get; set; }

        public PlayerBoard(Deck<Card> playerRestOfCards, Deck<Card> playerCardsInGame)
        {
            PlayerRestOfCards = playerRestOfCards;
            PlayerCardsInGame = playerCardsInGame;

            CardsPiechotyPlayer = new Deck<KartaPiechoty>();
            CardsStrzeleckiePlayer = new Deck<KartaLucznika>();
            CardsOblezniczePlayer = new Deck<KartaObleznika>();

            CardsSpecial = new Deck<CardSpecial>();

            PointsSum = 0;
            PointsPiechoty = 0;
            PointsStrzeleckie = 0;
            PointsObleznicze = 0;
        }   
        public Deck<CardWarrior> AllWarriorDeck()
        {
            Deck<CardWarrior> deck = new Deck<CardWarrior>();
            deck.AddRange(CardsPiechotyPlayer);
            deck.AddRange(CardsStrzeleckiePlayer);
            deck.AddRange(CardsOblezniczePlayer);
            return deck;
        }
        public void ResetBoard(){
            PointsSum = 0;
            PointsPiechoty = 0;
            PointsStrzeleckie = 0;
            PointsObleznicze = 0;
            CardsPiechotyPlayer.Clear();
            CardsStrzeleckiePlayer.Clear();
            CardsOblezniczePlayer.Clear();
            CardsSpecial.Clear();
        }   
    }
}