using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LinqToFile.Generic
{
    public class FileMetadata
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string DateTimeFormat { get; set; }
        public IFormatProvider NumberFormatProvider { get; set; }
        public TypeCode Type { get; set; }
    }
}
