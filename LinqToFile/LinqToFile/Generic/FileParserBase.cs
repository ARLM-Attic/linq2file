using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections;
using System.IO;

namespace LinqToFile.Generic
{
    public abstract class FileParserBase<T, M> : IFileParser<T>
        where T : new()
        where M : FileMetadata
    {
        private List<M> metadata;

        protected FileParserBase(List<M> metadata)
        {
            this.metadata = metadata;
        }

        protected virtual T Deserialize(string line, List<M> metadata)
        {
            T obj = new T();
            foreach (MetadataValue<M> metadataValue in this.Split(line, metadata))
                metadataValue.FieldMetadata.PropertyInfo.SetValue(obj, this.ParseValue(metadataValue), null);
            return obj;
        }

        protected abstract IEnumerable<MetadataValue<M>> Split(string line, List<M> metadata);

        protected object ParseValue(MetadataValue<M> metadataValue)
        {
            string item = metadataValue.Item.Trim();
            if (string.IsNullOrWhiteSpace(item)) return null;
            M fieldMetadata = metadataValue.FieldMetadata;
            switch (fieldMetadata.Type)
            {
                case TypeCode.Boolean:
                    if (item == "0") return false;
                    if (item == "1") return true;
                    return bool.Parse(item);
                case TypeCode.Byte: return byte.Parse(item);
                case TypeCode.Char: return item[0];
                case TypeCode.DateTime: return DateTime.ParseExact(item.Trim(), fieldMetadata.DateTimeFormat, CultureInfo.InvariantCulture);
                case TypeCode.Decimal: return Decimal.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.Double: return Double.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.Int16: return Int16.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.Int32: return Int32.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.Int64: return Int64.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.SByte: return SByte.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.Single: return Single.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.String: return item.TrimEnd();
                case TypeCode.UInt16: return UInt16.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.UInt32: return UInt32.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                case TypeCode.UInt64: return UInt64.Parse(item.Replace(" ", ""), fieldMetadata.NumberFormatProvider);
                default: return null;
            }
        }

        protected abstract bool End { get; }
        protected abstract string ReadRow();

        public virtual IEnumerator<T> GetEnumerator()
        {
            while (!this.End)
                yield return this.Deserialize(this.ReadRow(), this.metadata);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Dispose() { }
    }
}
