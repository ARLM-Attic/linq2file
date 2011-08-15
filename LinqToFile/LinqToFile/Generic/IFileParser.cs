using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace LinqToFile.Generic
{
    public interface IFileParser<T> : IEnumerable<T>, IDisposable where T : new() { }
}
