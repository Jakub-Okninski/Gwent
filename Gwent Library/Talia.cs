using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class Talia <T> : List<T>
    {
        public override string ToString()
        {
            return string.Join("\n ", this.Select(karta => karta.ToString()));
        }
     


    }
}
