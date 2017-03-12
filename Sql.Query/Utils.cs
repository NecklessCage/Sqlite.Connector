using System.Collections.Generic;

namespace Sql.QueryBuilder
{
    public static class Utils
    {
        private static Dictionary<BinaryOperator, string> OperatorMapper = new Dictionary<BinaryOperator, string>()
        {
            { BinaryOperator.Equal, "=" }
            , { BinaryOperator.LessThan, "<" }
            , { BinaryOperator.GreaterThan, ">" }
            , { BinaryOperator.NotEqual, "<>" }
            , { BinaryOperator.LessThanOrEqual, "<=" }
            , { BinaryOperator.GreaterThanOrEqual, ">=" }
        };

        public static string BinaryCompare(string columnName, string value, BinaryOperator op)
        {
            return string.Concat("(", columnName, OperatorMapper[op], "'", value, "')");
        }
    }
}
