﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class Talia : List<Karta>
    {
        public override string ToString()
        {
            return string.Join("\n ", this.Select(karta => karta.ToString()));
        }
    }
}