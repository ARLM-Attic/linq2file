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
        public FlatFileFieldAttribute(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }

        public int Start { get; private set; }
        public int Length { get; private set; }
    }
}
