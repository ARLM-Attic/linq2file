using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using LinqToFile.Generic;

namespace LinqToFile.Flat
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FlatFileFieldAttribute : FileFieldAttributeBase
    {
        public FlatFileFieldAttribute(int start, int lenght)
        {
            this.Start = start;
            this.Lenght = lenght;
        }

        public int Start { get; private set; }
        public int Lenght { get; private set; }
    }
}
