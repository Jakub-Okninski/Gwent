using System;
using System.Collections.Generic;

namespace Gwent_Library
{
    public class Talia<T> : List<T>, ICloneable where T : ICloneable
    {
        public object Clone()
        {
            Talia<T> clonedTalia = new Talia<T>();
            foreach (T item in this)
            {
                // Wywołaj metodę Clone() na każdym elemencie typu T
                clonedTalia.Add((T)item.Clone());
            }
            return clonedTalia;
        }
    }
}
