using System.Data;
using System.Data.SQLite;

/// <summary>
/// Htet Aung
/// March 12, 2017
/// Sqlite database connection class with all generic query executors.
/// </summary>

namespace Sqlite.Connector
{
    public static class Connector
    {
        /// <summary>
        /// Builds a Sqlite connection string.
        /// </summary>
        /// <param name="dataSource">Full path to the database</param>
        /// <param name="password">Password to access the database</param>
        /// <param name="isReadonly">Readonly connection or not. False by default</param>
        /// <param name="pooling">Connection pool allowed or not. False by default</param>
        /// <param name="version">Sqlite version. 3 by default</param>
        /// <returns>Full connection string</returns>
        public static string ConnectionString(string dataSource, string password,
            bool isReadonly = false, bool pooling = false, int version = 3)
        {
            var builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = dataSource;
            builder.Version = version;
            builder.Password = password;
            builder.ReadOnly = isReadonly;
            builder.Pooling = pooling;
            return builder.ConnectionString;
        }
        public static class Dml
        {
            /// <summary>
            /// The method is made for a single SELECT statement.
            /// </summary>
            /// <param name="connectionString">Connection string to Sqlite database.</param>
            /// <param name="query">Select query</param>
            /// <returns>Result set in DataTable type</returns>
            public static DataTable Select(string connectionString, string query)
            {
                using (var con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    using (var trans = con.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        var res = new DataTable();
                        using (var cmd = new SQLiteCommand(query, con, trans))
                        {
                            using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                            {
                                res.Load(reader);
                            }
                        }
                        trans.Commit();
                        return res;
                    }
                }
            }

            /// <summary>
            /// Method is made for a single SELECT statement with single colum, single row.
            /// </summary>
            /// <typeparam name="T">Type parameter</typeparam>
            /// <param name="connectionString">Connection string to Sqlite database.</param>
            /// <param name="query">Select query</param>
            /// <returns>Returns a Sqlite.Connector.Datum object. Null if no datum is returned by the query.</returns>
            public static Datum<T> Scalar<T>(string connectionString, string query)
            { // T should probably be constrained.
                using (var con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    using (var trans = con.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        Datum<T> res;
                        using (var cmd = new SQLiteCommand(query, con, trans))
                        {
                            var o = cmd.ExecuteScalar();
                            res = null != o ? new Datum<T>(o) : null;
                        }
                        trans.Commit();
                        return res;
                    }
                }
            }

            /// <summary>
            /// Method is made for a single Insert, Update or Delete statement.
            /// </summary>
            /// <param name="connectionString">Connection string to Sqlite database.</param>
            /// <param name="query">Select query</param>
            /// <returns>Returns number of affected rows.</returns>
            public static int NonQuery(string connectionString, string query)
            {
                using (var con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    using (var trans = con.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        int res;
                        using (var cmd = new SQLiteCommand(query, con, trans))
                        {
                            res = cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return res;
                    }
                }
            }
        }
    }
}
