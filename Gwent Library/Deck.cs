using System;
using System.Collections.Generic;

namespace Gwent_Library
{
    public class Deck<T> : List<T>, ICloneable where T : ICloneable
    {
        public object Clone()
        {
            Deck<T> clonedTalia = new Deck<T>();
            foreach (T item in this)
            {
                clonedTalia.Add((T)item.Clone());
            }
            return clonedTalia;
        }
    }
}
