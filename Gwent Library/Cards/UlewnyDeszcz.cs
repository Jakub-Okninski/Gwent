﻿using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.Karty
{
    public class UlewnyDeszcz : CardWeather
    {
        public UlewnyDeszcz(string name) : base(name)
        {
            Description = "Siła wszystkich jednostek oblężniczych przyjmuje wartość 1.";
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
            if (player1.playerBoard.CardsSpecial.Any(card => card is UlewnyDeszcz) || player2.playerBoard.CardsSpecial.Any(card => card is UlewnyDeszcz))
            {
                setForceWeather(player1.playerBoard.CardsOblezniczePlayer, 1);
                setForceWeather(player2.playerBoard.CardsOblezniczePlayer, 1);
            }
        }
    }
}
