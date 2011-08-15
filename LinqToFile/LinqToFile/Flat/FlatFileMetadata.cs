using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using LinqToFile.Generic;

namespace LinqToFile.Flat
{
    public class FlatFileMetadata : FileMetadata
    {
        public int Start { get; set; }
        public int Lenght { get; set; }
    }
}
