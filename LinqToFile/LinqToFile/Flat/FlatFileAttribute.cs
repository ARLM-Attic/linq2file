using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using LinqToFile.Generic;

namespace LinqToFile.Flat
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FlatFileAttribute : FileAttributeBase
    {
        public override IFileParser<T> GetLineParser<T>(Stream stream)
        {
            return new FlatFileParser<T>(
                typeof(T)
                    .GetProperties()
                    .Select(i => new
                    {
                        PropertyInfo = i,
                        Attribute = i
                            .GetCustomAttributes(typeof(FlatFileFieldAttribute), true)
                            .OfType<FlatFileFieldAttribute>()
                            .FirstOrDefault()
                    })
                    .Where(i => i.PropertyInfo.CanWrite && i.PropertyInfo.CanRead && i.Attribute != null)
                    .Select(i => new
                        FlatFileMetadata
                    {
                        PropertyInfo = i.PropertyInfo,
                        Start = i.Attribute.Start,
                        DateTimeFormat = i.Attribute.DateTimeFormat,
                        NumberFormatProvider = i.Attribute.GetNumberFormat(),
                        Length = i.Attribute.Length,
                        Type = Type.GetTypeCode(i.PropertyInfo.PropertyType)
                    }).ToList(),
                    stream);
        }
    }
}
