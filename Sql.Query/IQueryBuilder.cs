using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sql.Query
{
    interface IQueryBuilder
    {
        void Table(string tableName);
    }
}
