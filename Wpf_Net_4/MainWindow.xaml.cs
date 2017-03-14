using MahApps.Metro.Controls;

using System.Collections.Generic;
using System.Windows;

using Sqlite.Connector;
using Sql.QueryBuilder;

using F = Query.Builder;
using MF = Microsoft.FSharp.Collections;

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
        readonly string conStrWrite = Connector.ConnectionString(dataSource: @"W:\Databases\Sqlite\pos.db"
                                    , password: "htetaung"
                                    , isReadonly: false
                                    , pooling: true
                                    , version: 3);

        public MainWindow()
        {
            InitializeComponent();

            //FSharpTest();
            //Inserter();
            //Updater();
            //Deleter();

            string q = new Sql.QueryBuilder.Enforced
                .Select()
                .Columns("u.[UserKey] [Key]", "u.[UserName] [User Name]", "u.[Password]", "r.[UserRoleName] [Role]", "c.[UserName] [Created By]")
                .From("[User] u")
                    .InnerJoin("[UserRole] r").On("u.[UserRoleKey]", "r.[UserRoleKey]")
                    .InnerJoin("[User] c").On("u.[CreatedBy]", "c.[UserKey]")
                .Where(Logic.OR
                        , Utils.BinaryCompare("u.[UserKey]", BinaryOperator.Equal, "0")
                        , Utils.BinaryCompare("u.[UserKey]", BinaryOperator.Equal, "2"))
                .GroupBy("u.[CreatedBy]")
                .OrderBy("u.[UserName]", "u.[Password]")
                .Query;
            MessageBox.Show(q);

            var dt = Connector.Dml.Select(conStrReadonly, q);
            dtgHola.ItemsSource = dt.DefaultView;
        }

        void Inserter()
        {
            string p = new Sql.QueryBuilder.Enforced
               .Insert()
               .Into("[User]", "[UserKey]", "[UserName]", "[Password]", "[UserRoleKey]", "[CreatedBy]")
               .Values("11", "HtetAung", "hola", "0", "1")
               .Query;
            MessageBox.Show(Connector.Dml.NonQuery(conStrWrite, p).ToString());
        }
        void Updater()
        {
            string q = new Sql.QueryBuilder.Enforced
                .Update("[User]")
                .Set(Utils.BinaryCompare("[UserName]", BinaryOperator.Equal, "Zoey Deschannel")
                    , Utils.BinaryCompare("[Password]", BinaryOperator.Equal, "Helga Lovekatty"))
                .Where(Logic.AND
                    , Utils.BinaryCompare("[UserKey]", BinaryOperator.Equal, "0"))
                .Query;
            MessageBox.Show(Connector.Dml.NonQuery(conStrWrite, q).ToString());
        }
        void Deleter()
        {
            string q = new Sql.QueryBuilder.Enforced
                .Delete().From("[User]")
                .Where(Logic.AND
                    , Utils.BinaryCompare("[UserKey]", BinaryOperator.Equal, "1"))
                .Query;
            MessageBox.Show("Deleted # items: " + Connector.Dml.NonQuery(conStrWrite, q).ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new WindowRdlc().ShowDialog();
        }


        private void FSharpTest()
        {
            IEnumerable<string> cols = new List<string>()
            {
                "[UserKey]"
                , "[UserName]"
                , "[Password]"
            };
            MessageBox.Show(F.Select.reduceAdd(F.Select.commaSeparate(cols.ToFSharpList())));
        }
    }
}
