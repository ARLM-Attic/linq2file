using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using LinqToFile.Flat;

namespace WindowsFormsApplication1
{
    [FlatFile]
    public class FlatTable
    {
        [FlatFileField(0, 4)]
        public string Field1 { set; get; }

        [FlatFileField(4, 3, NegativeSign = "n", PositiveSign = "p")]
        public int Field2 { set; get; }

        [FlatFileField(7, 8, DateTimeFormat = "yyyyMMdd")]
        public DateTime DateField { get; set; }
    }
}
