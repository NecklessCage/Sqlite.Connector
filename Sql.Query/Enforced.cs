using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Xtensions;

namespace Sql.QueryBuilder.Enforced
{
    /// <summary>
    /// Interface for classes that are ready for querying.
    /// </summary>
    interface IQueryReady
    {
        string Query { get; }
    }

    /// <summary>
    /// Select query builder.
    /// </summary>
    public class Select
    {
        private readonly string _q = KeyWord.SELECT;
        public ColumnsReady Columns(params string[] columns)
        {
            return new ColumnsReady(_q, columns);
        }

        /// <summary>
        /// Columns are ready.
        /// </summary>
        public class ColumnsReady
        {
            private readonly string _q;
            internal ColumnsReady(string q, params string[] cols)
            {
                _q = q + (cols.Count() > 0 ? cols.Aggregate((res, next) => res + KeyWord.COMMA + next) : "*");
            }
            public TableReady From(string table)
            {
                return new TableReady(_q, table);
            }

            /// <summary>
            /// Table is ready.
            /// </summary>
            public class TableReady : IQueryReady
            {
                private readonly string _q;
                public string Query { get { return _q; } }
                internal TableReady(string q, string tbl)
                {
                    _q = q + KeyWord.FROM + tbl;
                }
                internal TableReady(string q) { _q = q; }

                public InnerJoiner InnerJoin(string table)
                {
                    return new InnerJoiner(_q, table);
                }

                public WhereReady Where(Logic lg, params string[] criteria)
                {
                    return new WhereReady(_q, lg, criteria);
                }
                public WhereReady Where(string preparedClause)
                {
                    return new WhereReady(_q, preparedClause);
                }

                public WhereReady.GroupByReady.OrderByReady OrderBy(params string[] columns)
                {
                    return new WhereReady.GroupByReady.OrderByReady(_q, columns);
                }

                /// <summary>
                /// Ready for ON clause
                /// </summary>
                public class InnerJoiner
                {
                    private string _q;
                    internal InnerJoiner(string q, string tbl)
                    {
                        _q = q + KeyWord.INNER_JOIN + tbl;
                    }

                    public TableReady On(string table1column, string table2column)
                    {
                        _q += KeyWord.ON + table1column + "=" + table2column;
                        return new TableReady(_q);
                    }
                }

                /// <summary>
                /// Where is ready.
                /// </summary>
                public class WhereReady : IQueryReady
                {
                    private readonly string _q;
                    public string Query { get { return _q; } }
                    internal WhereReady(string q, Logic l, params string[] cri)
                    {
                        _q = q + KeyWord.WHERE + cri.Aggregate((res, next) => res + " " + l.ToString() + " " + next);
                    }
                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="q">Query string so far</param>
                    /// <param name="w">Prepared where clause</param>
                    internal WhereReady(string q, string w)
                    {
                        _q = q + KeyWord.WHERE + w;
                    }

                    public GroupByReady GroupBy(params string[] columns)
                    {
                        return new GroupByReady(_q, columns);
                    }

                    public GroupByReady.OrderByReady OrderBy(params string[] columns)
                    {
                        return new GroupByReady.OrderByReady(_q, columns);
                    }

                    /// <summary>
                    /// Group By is ready.
                    /// </summary>
                    public class GroupByReady : IQueryReady
                    {
                        private readonly string _q;
                        public string Query { get { return _q; } }
                        internal GroupByReady(string q, params string[] cols)
                        {
                            if (cols.Count() < 1) throw new Exception("Must have at least one column to group by.");
                            _q = q + KeyWord.GROUP_BY + cols.Aggregate((res, next) => res + KeyWord.COMMA + next);
                        }
                        public OrderByReady OrderBy(params string[] columns)
                        {
                            return new OrderByReady(_q, columns);
                        }

                        /// <summary>
                        /// Order By is ready.
                        /// </summary>
                        public class OrderByReady : IQueryReady
                        {
                            private readonly string _q;
                            public string Query { get { return _q; } }
                            internal OrderByReady(string q, params string[] cols)
                            {
                                if (cols.Count() < 1) throw new Exception("Must have at least one column to order by.");
                                _q = q + KeyWord.ORDER_BY + cols.Aggregate((res, next) => res + KeyWord.COMMA + next);
                            }
                        }
                    }
                }
            }
        }
    } // Select

    /// <summary>
    /// Insert query builder.
    /// </summary>
    public class Insert
    {
        private readonly string _q = KeyWord.INSERT_INTO;
        public TableAndColumnsReady Into(string table, params string[] columns)
        {
            if (columns.Count() < 1) throw new Exception("Must have at least one column to insert.");
            return new TableAndColumnsReady(_q, table, columns);
        }

        /// <summary>
        ///  Table and columns are ready.
        /// </summary>
        public class TableAndColumnsReady
        {
            private readonly string _q;
            private readonly int _nCols;
            internal TableAndColumnsReady(string q, string tbl, params string[] cols)
            {
                _nCols = cols.Count();
                _q = q + tbl + " (" + cols.Aggregate((res, next) => res + KeyWord.COMMA + next) + ")";
            }

            public ValuesReady Values(params string[] values)
            {
                if (values.Count() != _nCols) throw new Exception("Number of columns and that of values must be the same.");
                return new ValuesReady(_q, values);
            }

            /// <summary>
            /// Values are ready.
            /// </summary>
            public class ValuesReady : IQueryReady
            {
                private readonly string _q;
                public string Query { get { return _q; } }
                internal ValuesReady(string q, params string[] vals)
                {
                    string tmp = Helper.Wrap(vals.GetEnumerator());
                    _q = q + KeyWord.VALUES + "(" +
                        tmp.Substring(0, tmp.Length - 1)
                        + ")";
                }
            }
        }
    } // Insert

    /// <summary>
    /// Update query builder.
    /// </summary>
    public class Update
    {
        private readonly string _q;
        public Update(string table)
        {
            _q = KeyWord.UPDATE + table;
        }

        public SetReady Set(params string[] mods)
        {
            return new SetReady(_q, mods);
        }

        /// <summary>
        /// Set is ready.
        /// </summary>
        public class SetReady
        {
            private readonly string _q;
            internal SetReady(string q, params string[] mods)
            {
                _q = q + KeyWord.SET + mods.Aggregate((res, next) => res + KeyWord.COMMA + next);
            }

            public WhereReady Where(Logic lg, params string[] criteria)
            {
                return new WhereReady(_q, lg, criteria);
            }

            /// <summary>
            /// Where is ready.
            /// </summary>
            public class WhereReady : IQueryReady
            {
                private readonly string _q;
                public string Query { get { return _q; } }
                internal WhereReady(string q, Logic l, params string[] cri)
                {
                    _q = q + KeyWord.WHERE + cri.Aggregate((res, next) => res + l.ToString() + next);
                }
            }
        }
    } // Update

    /// <summary>
    /// Delete query builder.
    /// </summary>
    public class Delete
    {
        private readonly string _q = KeyWord.DELETE_FROM;
        public TableReady From(string table)
        {
            return new TableReady(_q, table);
        }

        /// <summary>
        /// Table is ready.
        /// </summary>
        public class TableReady
        {
            private readonly string _q;
            internal TableReady(string q, string tbl)
            {
                _q = q + tbl;
            }
            public WhereReady Where(Logic lg, params string[] criteria)
            {
                return new WhereReady(_q, lg, criteria);
            }

            /// <summary>
            /// Where is ready.
            /// </summary>
            public class WhereReady : IQueryReady
            {
                private readonly string _q;
                public string Query { get { return _q; } }
                internal WhereReady(string q, Logic l, params string[] cri)
                {
                    _q = q + KeyWord.WHERE + cri.Aggregate((res, next) => res + " " + l.ToString() + " " + next);
                }
            }
        }
    } // Delete

    static class Helper
    {
        internal static string Wrap(IEnumerator e)
        {
            if (!e.MoveNext()) return "";
            else return "'" + e.Current + "'" + KeyWord.COMMA + Wrap(e);
        }
    }
}
