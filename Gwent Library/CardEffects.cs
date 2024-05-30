using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class CardEffects  
    {
        public static void Braterstwo(Card card, PlayerBoard playerBoard)
        {
            if (card is KartaPiechoty)
            {
                BraterstwoHelp(card, playerBoard.CardsPiechotyPlayer, playerBoard);
            }
            else if (card is KartaLucznika)
            {
                BraterstwoHelp(card, playerBoard.CardsStrzeleckiePlayer, playerBoard);
            }
            else if (card is KartaObleznika)
            {
                BraterstwoHelp(card, playerBoard.CardsOblezniczePlayer, playerBoard);
            }
        }
        private static void BraterstwoHelp<T>(Card card, Deck<T> deck, PlayerBoard playerBoard) where T : CardWarrior
        {
            var otherCard = playerBoard.PlayerRestOfCards.Where(c => c is CardWarrior cw && cw.Name.Contains(card.Name) && cw.Effect == Braterstwo);

            foreach (T oc in otherCard)
            {
                deck.Add(oc);
            }

            playerBoard.PlayerRestOfCards.RemoveAll(c => c is CardWarrior kp && kp.Name.Contains(card.Name) && kp.Effect == Braterstwo);


            var otherCard2 = playerBoard.PlayerCardsInGame.Where(c => c is CardWarrior cw && cw.Name.Contains(card.Name) && cw.Effect == Braterstwo);

            foreach (T oc in otherCard2)
            {
                deck.Add(oc);
            }

            playerBoard.PlayerCardsInGame.RemoveAll(c => c is CardWarrior cw && cw.Name.Contains(card.Name)
            && cw.Effect == Braterstwo);
           
        }

        public static void Wiez(Card card, PlayerBoard playerBoard)
        {
            if (card is KartaPiechoty)
            {
                WizeHelp(card, playerBoard.CardsPiechotyPlayer);
            }
            else if (card is KartaLucznika)
            {
                WizeHelp(card, playerBoard.CardsStrzeleckiePlayer);
            }
            else if (card is KartaObleznika)
            {
                WizeHelp(card, playerBoard.CardsOblezniczePlayer);
            }
        }


        private static void WizeHelp<T>(Card card,  Deck<T> deck) where T : CardWarrior
        {
            var otherCard = deck.Where(c => c is CardWarrior cw && cw.Name.Contains(card.Name) && cw.Effect == Wiez);
            foreach (CardWarrior oc in otherCard)
            {
                oc.ForceTemporary = oc.ForceDefault * otherCard.Count();
            }
        }

        public static void WysokieMorale(Card card, PlayerBoard playerBoard)
        {
            if (card is KartaPiechoty)
            {
                WysokieMoraleHelp(card, playerBoard.CardsPiechotyPlayer);
            }
            else if (card is KartaLucznika)
            {            
                WysokieMoraleHelp(card, playerBoard.CardsStrzeleckiePlayer);
            }
            else if (card is KartaObleznika)
            {
                WysokieMoraleHelp(card, playerBoard.CardsOblezniczePlayer);
            }

        }
        private static void WysokieMoraleHelp<T>(Card card, Deck<T> deck) where T : CardWarrior
        {
            var otherCard = deck.Where(c => c is CardWarrior cw && cw.Name.Contains(card.Name) && cw.Effect == WysokieMorale);
            foreach (CardWarrior cardWarrior in otherCard)
            {
                if (otherCard.Count() > 1)
                {
                    cardWarrior.ForceTemporary = cardWarrior.ForceDefault + otherCard.Count();
                    cardWarrior.setDefaultForce();
                }
            }
        }
        public static void Default(Card card, PlayerBoard playerBoard) { }
    }
}
