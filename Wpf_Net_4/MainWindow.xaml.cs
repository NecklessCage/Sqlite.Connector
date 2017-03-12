using MahApps.Metro.Controls;
using System.Windows;

using Sqlite.Connector;
using Sql.QueryBuilder;

namespace Wpf_Net_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        readonly string conStrReadonly = Connector.ConnectionString(dataSource: @"W:\Databases\Sqlite\pos.db"
                                    , password: "htetaung"
                                    , isReadonly: true
                                    , pooling: true
                                    , version: 3);

        public MainWindow()
        {
            InitializeComponent();

            var b = new Select();
            b.Columns("[UserKey]", "[UserName]");
            b.Table("[User]");
            b.WhereConjunction(
                Utils.BinaryCompare("[UserKey]", "0", BinaryOperator.Equal)
                , Utils.BinaryCompare("[UserName]", "aunggyi", BinaryOperator.Equal)
                );
            string q = "";
            if (b.Query(out q))
            {
                MessageBox.Show(q);
                var dt = Connector.Dml.Select(conStrReadonly, q);
                dtgHola.ItemsSource = dt.DefaultView;
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
