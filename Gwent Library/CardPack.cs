using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class CardPack : List<Card>
    {
        public override string ToString()
        {
            return string.Join("\n ", this.Select(card => card.ToString()));
        }
    }
}
