using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gwent_Library.TypyKart
{
    public abstract class CardGlobal : CardSpecial, ICloneable
    {
        public CardGlobal(string name) : base(name)
        {
        }
        public abstract void GlobalActionBoard(Player player1, Player player2);
    }
}
