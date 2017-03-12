using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sql.QueryBuilder
{
    public class Select
    {
        #region Readiness booleans
        // The bool must be true before the bool below can be executed + set true
        private bool _columnNamesReady = false;
        private bool _tableNameReady = false;
        private bool _isReady = false;
        #endregion

        private readonly StringBuilder _builder = new StringBuilder(KeyWord.SELECT);

        /// <summary>
        /// This method does not require any preconditions.
        /// </summary>
        /// <param name="columnNames">Columns to be fetched.</param>
        public bool Columns(params string[] columnNames)
        {
            /*  The query string at this point should be
                "SELECT"
                Add a space before adding column names. */
            if (!_columnNamesReady)
            {
                _builder.Append(KeyWord.SPACE);
                foreach (var col in columnNames)
                {
                    _builder.Append(string.Concat(col, KeyWord.COMMA));
                }
                // Remove the last extra comma.
                _builder.Length--;
                _columnNamesReady = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Table(string tableName)
        {
            if (_columnNamesReady && !_tableNameReady)
            {
                _builder.Append(string.Concat(KeyWord.SPACE, KeyWord.FROM, KeyWord.SPACE, tableName));
                _tableNameReady = true;
                _isReady = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WhereConjunction(params string[] criteria)
        {
            if (_tableNameReady)
            {
                _builder.Append(string.Concat(KeyWord.SPACE, KeyWord.WHERE));
                foreach (var cri in criteria)
                {
                    _builder.Append(string.Concat(KeyWord.SPACE, cri, KeyWord.SPACE, KeyWord.AND));
                }
                _builder.Length -= (KeyWord.SPACE.Length + KeyWord.AND.Length);
                _isReady = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Query(out string query)
        {
            if (_isReady)
            {
                query = _builder.ToString();
                return true;
            }
            else
            {
                query = "";
                return false;
            }
        }
    }
}
