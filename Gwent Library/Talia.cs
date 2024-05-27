using Gwent_Library.TypyKart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library
{
    public class Talia<T> : List<T>, ICloneable
    {
        public object Clone()
        {
            Talia<T> clonedTalia = new Talia<T>();
            foreach (T item in this)
            {
                clonedTalia.Add(item);
            }
            return clonedTalia;
        }
    }
}
