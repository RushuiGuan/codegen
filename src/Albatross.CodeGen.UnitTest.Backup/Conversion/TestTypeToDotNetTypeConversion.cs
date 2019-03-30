using Albatross.CodeGen.CSharp.Conversion;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest.Conversion
{
    [TestFixture]
    public class TestTypeToDotNetTypeConversion
    {
        [Test]
        public void Run() {
            var cvt = new ConvertTypeToDotNetType();
            var result = cvt.Convert(typeof(string));
            Assert.AreEqual("string", result.Name);
        }
    }
}
