using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LinqToFile.Generic;

namespace LinqToFile
{
    public class FileQuery<T> : IDisposable where T : new()
    {
        private FileAttributeBase fileAttribute;
        private Stream stream;
        private bool disposeStream = true;

        public FileQuery(string path) : this(new FileStream(path, FileMode.Open)) { }

        public FileQuery(Stream stream)
        {
            this.disposeStream = false;
            this.stream = stream;
            this.fileAttribute = typeof(T).GetCustomAttributes(typeof(FileAttributeBase), true).OfType<FileAttributeBase>().FirstOrDefault();
        }

        public IEnumerable<T> Content
        {
            get
            {
                stream.Seek(0, SeekOrigin.Begin);
                return this.fileAttribute.GetLineParser<T>(stream);
            }
        }

        public void Dispose()
        {
            if (this.disposeStream) stream.Dispose();
        }
    }
}
