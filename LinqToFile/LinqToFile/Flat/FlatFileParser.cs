using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LinqToFile.Generic;

namespace LinqToFile.Flat
{
    public class FlatFileParser<T> : FileParserBase<T, FlatFileMetadata> where T : new()
    {
        private StreamReader streamReader = null;
        public FlatFileParser(List<FlatFileMetadata> metadata, Stream stream)
            : base(metadata)
        {
            this.streamReader = new StreamReader(stream);
        }

        public override void Dispose()
        {
            streamReader.Dispose();
        }

        protected override bool End { get { return this.streamReader.EndOfStream; } }

        protected override string ReadRow()
        {
            return this.streamReader.ReadLine();
        }

        protected override IEnumerable<MetadataValue<FlatFileMetadata>> Split(string line, List<FlatFileMetadata> metadata)
        {
            return metadata.Select(i => new MetadataValue<FlatFileMetadata> { Item = line.Substring(i.Start, i.Length), FieldMetadata = i }).ToList();
        }
    }
}
