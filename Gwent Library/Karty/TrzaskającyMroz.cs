﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public class TrzaskającyMroz : KartaPogody, IPolaPiechoty
    {
        public TrzaskającyMroz (string nazwa, string nazwaZdjecia) : base(nazwa, nazwaZdjecia)
        {
        }
    }
}
