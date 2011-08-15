using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using LinqToFile.Csv;

namespace WindowsFormsApplication1
{
    [CsvFile(true, '"', ';')]
    public class Compte
    {
        [CsvFileField("Valeur debitee", NumberDecimalSeparator = ".")]
        public decimal Amount { set; get; }

        [CsvFileField("Type")]
        public string Type { get; set; }

        [CsvFileField("Date", DateTimeFormat = "dd/MM/yyyy")]
        public DateTime Date { set; get; }

        public decimal? Debit { get { return this.Amount < 0 ? -this.Amount : (decimal?)null; } }
        public decimal? Credit { get { return this.Amount >= 0 ? this.Amount : (decimal?)null; } }
    }
}
