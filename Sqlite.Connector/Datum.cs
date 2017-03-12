using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sqlite.Connector
{
    public class Datum<T>
    {
        private T _tDatum;

        public Datum(object datum) { _tDatum = (T)datum; }

        public T Value { get { return _tDatum; } }
    }
}
