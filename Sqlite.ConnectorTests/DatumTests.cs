using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqlite.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sqlite.Connector.Tests
{
    [TestClass()]
    public class DatumTests
    {
        [TestMethod()]
        public void GetTestInt()
        {
            int i = 12;
            Datum d = new Datum(i);
            Assert.AreEqual(i, d.Get<int>());
        }
        [TestMethod()]
        public void GetTestLong()
        {
            long i = 12;
            Datum d = new Datum(i);
            Assert.AreEqual(i, d.Get<long>());
        }
        [TestMethod()]
        public void GetTestStr()
        {
            string i = "12";
            Datum d = new Datum(i);
            Assert.AreEqual(i, d.Get<string>());
        }
        [TestMethod()]
        public void GetTestChar()
        {
            char i = '1';
            Datum d = new Datum(i);
            Assert.AreEqual(i, d.Get<char>());
        }
    }
}