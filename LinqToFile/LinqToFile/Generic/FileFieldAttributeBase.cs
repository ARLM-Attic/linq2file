using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace LinqToFile.Generic
{
    public abstract class FileFieldAttributeBase : Attribute
    {
        public string DateTimeFormat { get; set; }

        public string NegativeSign { get; set; }
        public string NumberDecimalSeparator { get; set; }
        public string NumberGroupSeparator { get; set; }
        public int NumberNegativePattern { get; set; }
        public string PositiveSign { get; set; }

        internal NumberFormatInfo GetNumberFormat()
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            if (!string.IsNullOrEmpty(this.NegativeSign)) numberFormatInfo.NegativeSign = this.NegativeSign;
            if (!string.IsNullOrEmpty(this.NumberDecimalSeparator)) numberFormatInfo.NumberDecimalSeparator = this.NumberDecimalSeparator;
            if (!string.IsNullOrEmpty(this.NumberGroupSeparator)) numberFormatInfo.NumberGroupSeparator = this.NumberGroupSeparator;
            if (!string.IsNullOrEmpty(this.PositiveSign)) numberFormatInfo.PositiveSign = this.PositiveSign;
            if (this.NumberNegativePattern != 0) numberFormatInfo.NumberNegativePattern = this.NumberNegativePattern;
            return numberFormatInfo;
        }
    }
}
