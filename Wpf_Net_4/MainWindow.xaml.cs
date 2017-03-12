using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;

using Sql.Query;
using Sqlite.Connector;

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

            var b = new Builder.SELECTBuilder();
            b.Columns("UserKey", "UserName");
            b.Table("User");
            b.WhereConjunction(Builder.BinaryCompare("UserKey", "0", Builder.BinaryOperator.Equal)
                , Builder.BinaryCompare("UserName", "aunggyi", Builder.BinaryOperator.Equal));
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

            if (false)
            {
                string constr = Connector.ConnectionString(dataSource: @"W:\Databases\Sqlite\pos.db"
                                    , password: "htetaung"
                                    , isReadonly: false
                                    , pooling: true
                                    , version: 3);
                MessageBox.Show(
                    Connector.Dml.NonQuery(constr
                        , @"UPDATE [UserRole] SET [UserRoleName]='Sales Manager' WHERE [UserRoleDescription]='-';")
                    .ToString());

                constr = Connector.ConnectionString(dataSource: @"W:\Databases\Sqlite\pos.db"
                                    , password: "htetaung"
                                    , isReadonly: true
                                    , pooling: true
                                    , version: 3);
                var dt = Connector.Dml.Select(constr,
                            @"SELECT * FROM [UserRole];");
                dtgHola.ItemsSource = dt.DefaultView;

                var v = Connector.Dml.Scalar<long>(constr
                        , @"SELECT [UserRoleKey] FROM [UserRole] WHERE [UserRoleName]='Office Manager';");
                MessageBox.Show(null != v ? v.Value.ToString() : "NULL");
            }
        }
    }
}
