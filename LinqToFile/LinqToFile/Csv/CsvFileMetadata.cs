using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using LinqToFile.Generic;

namespace LinqToFile.Csv
{
    public class CsvFileMetadata : FileMetadata
    {
        public int Column { get; set; }
        public string Name { get; set; }
    }
}
