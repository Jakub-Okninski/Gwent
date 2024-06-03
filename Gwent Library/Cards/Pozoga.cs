using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class Pozoga : CardGlobal, ICloneable
    {
        public Pozoga(string name) : base(name)
        {
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
            if (player1.playerBoard.CardsSpecial.Any(card => card is Pozoga) 
                || player2.playerBoard.CardsSpecial.Any(card => card is Pozoga))
            {
                var allCardWarrior = new List<CardWarrior>();

                allCardWarrior.AddRange(player1.playerBoard.AllWarriorDeck());
                allCardWarrior.AddRange(player2.playerBoard.AllWarriorDeck());
                int maxForce = allCardWarrior.Any() ? allCardWarrior.Max(karta => karta.Force) : 0;

                RemoveCardOfGivenForce(player1.playerBoard.CardsPiechotyPlayer, maxForce);
                RemoveCardOfGivenForce(player2.playerBoard.CardsPiechotyPlayer, maxForce);

                RemoveCardOfGivenForce(player1.playerBoard.CardsStrzeleckiePlayer, maxForce);
                RemoveCardOfGivenForce(player2.playerBoard.CardsStrzeleckiePlayer, maxForce);

                RemoveCardOfGivenForce(player1.playerBoard.CardsOblezniczePlayer, maxForce);
                RemoveCardOfGivenForce(player2.playerBoard.CardsOblezniczePlayer, maxForce);

                player2.playerBoard.CardsSpecial.RemoveAll(card => card is Pozoga);
                player1.playerBoard.CardsSpecial.RemoveAll(card => card is Pozoga);
            }
        }
        private void RemoveCardOfGivenForce<T>(Deck<T> deck, int force) where T : CardWarrior
        {
            deck.RemoveAll(card => card.Force == force);
        }
    }
}
