using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Specialized;
using System.Globalization;
using LinqToFile.Generic;

namespace LinqToFile.Csv
{
    public class CsvFileParser<T> : FileParserBase<T, CsvFileMetadata> where T : new()
    {
        private bool hasHeader;
        private char textDemimiter;
        private char fieldDemimiter;
        private Regex regex = null;
        private StreamReader streamReader = null;

        public CsvFileParser(bool hasHeader, char textDemimiter, char fieldDemimiter, List<CsvFileMetadata> metadata, Stream stream)
            : base(metadata)
        {
            this.hasHeader = hasHeader;
            this.textDemimiter = textDemimiter;
            this.fieldDemimiter = fieldDemimiter;
            string sep = this.GetRegexTranslation(fieldDemimiter);
            string tq = this.GetRegexTranslation(textDemimiter);
            this.regex = new Regex(sep + @"(?=(?:[^" + tq + "]*" + tq + "[^" + tq + "]*" + tq + ")*(?![^" + tq + "]*" + tq + "))", RegexOptions.Compiled | RegexOptions.Singleline);
            this.streamReader = new StreamReader(stream);
            if (hasHeader)
            {
                List<string> collection = this.ParseCsvLine(this.streamReader.ReadLine());
                for (int counter = 0; counter < collection.Count; counter++)
                {
                    CsvFileMetadata fieldMetadata = metadata.FirstOrDefault(i => string.Equals(i.Name, collection[counter], StringComparison.InvariantCultureIgnoreCase));
                    if (fieldMetadata != null && fieldMetadata.Column < 0) fieldMetadata.Column = counter;
                }
            }
        }

        private List<string> ParseCsvLine(string line)
        {
            List<string> cells = new List<string>();
            foreach (string cell in this.regex.Split(line))
            {
                string newCell = cell;
                if (cell.StartsWith(this.textDemimiter.ToString()) && cell.EndsWith(this.textDemimiter.ToString()))
                    newCell = cell.Substring(1, cell.Length - 2).Replace("\"\"", "\"");
                cells.Add(newCell);
            }
            return cells;
        }

        private string GetRegexTranslation(char character)
        {
            switch ((int)char.GetNumericValue(character))
            {
                case 0x7: return @"\a";
                case 0x8: return @"\b";
                case 0x9: return @"\t";
                case 0xD: return @"\r";
                case 0xB: return @"\v";
                case 0xC: return @"\f";
                case 0xA: return @"\n";
                case 0x1B: return @"\e";
                default: return (@".$^{[(|)*+?\""".IndexOf(character) >= 0) ? @"\" + character.ToString() : character.ToString();
            }
        }

        protected override bool End { get { return this.streamReader.EndOfStream; } }

        protected override string ReadRow()
        {
            return this.streamReader.ReadLine();
        }

        public override void Dispose()
        {
            streamReader.Dispose();
        }

        protected override IEnumerable<MetadataValue<CsvFileMetadata>> Split(string line, List<CsvFileMetadata> metadata)
        {
            List<string> items = this.ParseCsvLine(line);
            return metadata.Select(i => new MetadataValue<CsvFileMetadata> { Item = items[i.Column], FieldMetadata = i }).ToList();
        }
    }
}
