using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using LinqToFile.Csv;

namespace WindowsFormsApplication1
{
    [CsvFile(true, '"', ';')]
    public class CsvTable
    {
        [CsvFileField]
        public string Field1 { set; get; }

        [CsvFileField]
        public int Field2 { set; get; }

        [CsvFileField("Field3", DateTimeFormat = "yyyyMMdd")]
        public DateTime DateField { get; set; }
    }
}
