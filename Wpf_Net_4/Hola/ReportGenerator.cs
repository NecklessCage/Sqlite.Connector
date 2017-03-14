using System;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Wpf_Net_4.Hola
{
    public class ReportGenerator
    {
        protected readonly Rdlc.Generator.Report Report = new Rdlc.Generator.Report();
        protected readonly ReportDataSourceCollection DataSources;

        private readonly LocalReport localReport;

        public ReportGenerator(LocalReport localReport)
        {
            if (localReport == null)
            {
                throw new ArgumentNullException("localReport");
            }

            this.localReport = localReport;
            this.DataSources = localReport.DataSources;
        }

        public virtual void Run()
        {
            //this.Report.Element.Save(Console.Out);  // Uncomment this to show the entire RDLC in the Output window.
            this.LoadReportDefinition();
        }

        private void LoadReportDefinition()
        {
            using (var stream = new MemoryStream())
            {
                this.Report.Element.Save(stream);
                stream.Position = 0;
                this.localReport.LoadReportDefinition(stream);
            }
        }
    }
}
