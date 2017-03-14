using System;
using System.Collections.Generic;

namespace Xtensions
{
    public static class Misc
    {
        public static void Assert(this bool val, string exceptionMsg)
        {
            if (!val) throw new Exception(exceptionMsg);
        }
    }
}
