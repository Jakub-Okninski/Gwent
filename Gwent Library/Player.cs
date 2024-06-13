using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.Karty;
using Gwent_Library.TypyKart;

namespace Gwent_Library
{
    public class Player 
    {
        public string Name { get; set; }
        public Deck<Card> AllCardsPlayer{ get; set; }
        public PlayerBoard playerBoard { get; set; }
        public int PlayerPoints { get; set; }
        public bool PlayerActiveInRound { get; set; } 

        public Player(string name, Deck<Card> allCardsPlayer)
        {
            Name = name;
            AllCardsPlayer = allCardsPlayer;
            PlayerPoints = 2;
            PlayerActiveInRound = true;
        }
        public void SetBoard(Deck<Card> playerRestOfCards, Deck<Card> playerCardsInGame)
        {
            playerBoard = new PlayerBoard(playerRestOfCards, playerCardsInGame);
        }
        public void DecrementPlayerPonts()
        {
            PlayerPoints--;
            if (PlayerPoints <= 0)
            {
                throw new EndGameException("Gracz "+ Name + " stracił wszystkie punkty! ");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Player))
            {
                return false;
            }

            Player other = (Player)obj;
            return Name.CompareTo(other.Name) == 0;
        }
        public override string ToString()
        {
            return $"Player: {Name}, Player Points: {PlayerPoints}, Points Sum: {playerBoard.PointsSum}, Points Piechoty: {playerBoard.PointsPiechoty}, Points Strzeleckie: {playerBoard.PointsStrzeleckie}, Points Obleznicze: {playerBoard.PointsObleznicze}, " +
                   $"Number of cards: {AllCardsPlayer.Count}";
        }
    }
}
