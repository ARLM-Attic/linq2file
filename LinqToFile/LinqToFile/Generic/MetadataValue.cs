using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToFile.Generic
{
    public class MetadataValue<M> where M : FileMetadata
    {
        public string Item { get; set; }
        public M FieldMetadata { get; set; }
    }
}
