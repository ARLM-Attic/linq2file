using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LinqToFile;
using System.IO;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            using (FileQuery<Compte> fq = new FileQuery<Compte>(GetTestStream("Comptes.csv")))
            {
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = fq
                    .Content
                    .GroupBy(i => i.Date.Year)
                    .Select(i => new { Year = i.Key, Total = i.Sum(g => g.Amount) }).ToList();
                this.dataGridView2.AutoGenerateColumns = false;
                this.dataGridView2.DataSource = fq.Content.OrderByDescending(i => i.Date).ToList();
            }
            using (FileQuery<FlatTable> fq = new FileQuery<FlatTable>(GetTestStream("Book1.txt")))
                this.dataGridView3.DataSource = fq
                    .Content
                    .Where(i => i.Field2 != 12)
                    .ToList();
            using (FileQuery<CsvTable> fq = new FileQuery<CsvTable>(GetTestStream("Book1.csv")))
                this.dataGridView4.DataSource = fq
                    .Content
                    .Where(i => i.Field2 != 9)
                    .ToList();
        }

        private static Stream GetTestStream(string resource)
        {
            return typeof(Program).Assembly.GetManifestResourceStream(@"WindowsFormsApplication1.InputTestFiles." + resource);
        }
    }
}
