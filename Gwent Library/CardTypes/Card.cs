using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public abstract class Card : ICloneable
    {
        public string Name { get; set; }
        public string ImgName { get; }
        public string Description { get; set; }
        public Card(string name)
        {
            Name = name;
            ImgName=name;
        }
        public abstract void PutCard(PlayerBoard playerBoard);

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return $"Name: {Name}, ";
        }
    }
}