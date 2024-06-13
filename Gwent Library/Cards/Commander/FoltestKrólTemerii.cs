using Gwent_Library.Karty;
using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.Karty.KartyDowodcow
{
    public class FoltestKrólTemerii : CardCommander
    {
        public FoltestKrólTemerii(string name) : base(name, "Znajduje kartę gęsta mgła i nią zagrywa")
        {
        }
        public override void GlobalActionBoard(Player player1, Player player2)
        {
            player1.playerBoard.CardsSpecial.Add(new GestaMgla("Gęsta Mgła Króla Temerii"));
            player1.playerBoard.CardsSpecial.Remove(this);
        }
    }
}
