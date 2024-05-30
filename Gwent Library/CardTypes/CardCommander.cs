using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwent_Library.TypyKart;

namespace Gwent_Library.Karty
{
    public abstract class CardCommander : CardGlobal, ICloneable
    {
        public string Description { get; set;}
        public CardCommander(string name,  string description) : base(name)
        {
            Description = description;  
        }
        public override string ToString()
        {
            return base.ToString() + $" Description: {Description}, ";
        }
    }
}
