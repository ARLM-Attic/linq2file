using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using LinqToFile.Generic;

namespace LinqToFile.Csv
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CsvFileAttribute : FileAttributeBase
    {
        public CsvFileAttribute(bool hasHeader, char textDelimiter, char fieldDelimiter)
        {
            this.HasHeader = hasHeader;
            this.TextDemimiter = textDelimiter;
            this.FieldDemimiter = fieldDelimiter;
        }
        public bool HasHeader { get; private set; }
        public char TextDemimiter { get; private set; }
        public char FieldDemimiter { get; private set; }

        public override IFileParser<T> GetLineParser<T>(Stream stream)
        {
            return new CsvFileParser<T>(
                this.HasHeader,
                this.TextDemimiter,
                this.FieldDemimiter,
                typeof(T)
                    .GetProperties()
                    .Select(i => new
                        {
                            PropertyInfo = i,
                            Attribute = i
                                .GetCustomAttributes(typeof(CsvFileFieldAttribute), true)
                                .OfType<CsvFileFieldAttribute>()
                                .FirstOrDefault()
                        })
                    .Where(i => i.PropertyInfo.CanWrite && i.PropertyInfo.CanRead && i.Attribute != null)
                    .Select(i => new
                        CsvFileMetadata
                        {
                            PropertyInfo = i.PropertyInfo,
                            Column = i.Attribute.Column,
                            DateTimeFormat = i.Attribute.DateTimeFormat,
                            NumberFormatProvider = i.Attribute.GetNumberFormat(),
                            Name = string.IsNullOrWhiteSpace(i.Attribute.Name) ? i.PropertyInfo.Name : i.Attribute.Name,
                            Type = Type.GetTypeCode(i.PropertyInfo.PropertyType)
                        })
                    .ToList(),
                stream);
        }
    }
}
