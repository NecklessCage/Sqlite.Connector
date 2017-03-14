using System.Linq;
using System.Text;

using Xtensions;

namespace Sql.QueryBuilder
{
    public class Select
    {
        #region Readiness booleans
        // The bool must be true before the bool below can be executed + set true
        private bool _columnNamesReady = false;
        private bool _tableNameReady = false;
        private bool _whereReady = false;
        private bool _isReady = false;
        #endregion

        private readonly StringBuilder _builder = new StringBuilder(KeyWord.SELECT);

        public Select(bool withWhere = false, params string[] columnNames)
        {
            _whereReady = !withWhere;
            Columns(columnNames);
        }

        /// <summary>
        /// This method requires that column names are not yet set.
        /// </summary>
        /// <param name="columnNames">Columns to be fetched.</param>
        private void Columns(params string[] columnNames)
        {
            /*  The query string at this point should be
                "SELECT"
                Add a space before adding column names. */
            #region Precondition(s)
            (!_columnNamesReady).Assert("Column name(s) has been set, and cannot be set more than once.");
            #endregion

            _builder.Append(columnNames.Aggregate((res, next) => res + "," + next));
            _columnNamesReady = true;
        }

        /// <summary>
        /// This method requires that column names are added first and that table is not yet set.
        /// </summary>
        /// <param name="tableName">Name of the table to select from.</param>
        public Select From(string tableName)
        {
            #region Preconditions
            _columnNamesReady.Assert("Columns must be set before setting the table in the query.");
            (!_tableNameReady).Assert("Table name has been set, and cannot be set more than once.");
            #endregion

            _builder.Append(KeyWord.FROM + tableName);
            _tableNameReady = true;
            _isReady = _whereReady;
            return this;
        }

        public Select Where(Logic l, params string[] criteria)
        {
            #region Precondition(s)
            _tableNameReady.Assert("Table must be set before setting where clause in the query.");
            #endregion

            _builder.Append(KeyWord.WHERE + criteria.Aggregate((res, next) => res + l.ToString() + next));
            _isReady = true;
            return this;
        }

        public string Query()
        {
            #region Precondition(s)
            _isReady.Assert("Query is not ready for execution.");
            #endregion

            return _builder.ToString();
        }
    }
}
