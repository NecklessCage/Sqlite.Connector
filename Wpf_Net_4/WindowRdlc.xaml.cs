using System.Windows;
using System.Data;
using Microsoft.Reporting.WinForms;

using Sqlite.Connector;
using Sql.QueryBuilder;
using System.Collections;

namespace Wpf_Net_4
{
    /// <summary>
    /// Interaction logic for WindowRdlc.xaml
    /// </summary>
    public partial class WindowRdlc : Window
    {
        readonly string conStrReadonly = Connector.ConnectionString(dataSource: @"W:\Databases\Sqlite\pos.db"
                                    , password: "htetaung"
                                    , isReadonly: true
                                    , pooling: true
                                    , version: 3);

        public WindowRdlc()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            /*
            var b = new Sql.QueryBuilder.Select();
            b.Columns("[UserKey]", "[UserName]");
            b.From("[User]");
            b.Where(Logic.AND
                , Utils.BinaryCompare("[UserKey]", BinaryOperator.Equal, "0")
                , Utils.BinaryCompare("[UserName]", BinaryOperator.Equal, "aunggyi")
                );
            string q = b.Query();

            MessageBox.Show(q);
            var dt = Connector.Dml.Select(conStrReadonly, q); // The report

            var data = Hola.User.Users(dt);
            new HolaGenerator(rpvHola.LocalReport).Run(data, dt);
            */
        }
    }

    //---------------------------------------------------------------------------------
    class HolaGenerator : Hola.ReportGenerator
    {
        public HolaGenerator(LocalReport localReport) : base(localReport) { }

        public void Run(IEnumerable data, DataTable dt)
        {
            var dataSetForMainTable = CreateDataSet(dt.Columns);

            var mainTable = Hola.MainTable.Create(dataSetForMainTable.Name, dt);
            var body = new Rdlc.Generator.Body();
            body.AddReportItem(mainTable);
            this.Report.AddReportSection(new Rdlc.Generator.ReportSection(body));

            this.Report.AddDataSet(dataSetForMainTable);
            this.DataSources.Add(new ReportDataSource(dataSetForMainTable.Name, dt));

            base.Run();
        }

        private static Rdlc.Generator.DataSet CreateDataSet(DataColumnCollection columns)
        {
            var dataSet = new Rdlc.Generator.DataSet();
            foreach (DataColumn col in columns)
            {
                dataSet.AddField(col.ColumnName);
            }
            return dataSet;
        }
    }
}
