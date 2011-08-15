using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LinqToFile.Generic
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class FileAttributeBase : Attribute
    {
        public abstract IFileParser<T> GetLineParser<T>(Stream stream) where T : new();
    }
}
