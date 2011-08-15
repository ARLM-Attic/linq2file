using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using LinqToFile.Generic;

namespace LinqToFile.Csv
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvFileFieldAttribute : FileFieldAttributeBase
    {
        public CsvFileFieldAttribute()
        {
            this.Column = -1;
        }

        public CsvFileFieldAttribute(string name)
        {
            this.Name = name;
            this.Column = -1;
        }
        public CsvFileFieldAttribute(int column)
        {
            this.Name = null;
            this.Column = column;
        }

        public string Name { get; private set; }
        public int Column { get; private set; }
    }
}
