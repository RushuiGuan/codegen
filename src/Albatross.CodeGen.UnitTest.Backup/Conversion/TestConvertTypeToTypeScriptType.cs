using Albatross.CodeGen.TypeScript.Conversion;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest.Conversion
{
    [TestFixture]
    public class TestConvertTypeToTypeScriptType
    {
        [TestCase(typeof(int), ExpectedResult = null)]
        [TestCase(typeof(string), ExpectedResult = null)]
        [TestCase(typeof(Array), ExpectedResult = typeof(object))]
        [TestCase(typeof(int[]), ExpectedResult = typeof(int))]
        [TestCase(typeof(IEnumerable), ExpectedResult = typeof(object))]
        [TestCase(typeof(System.Collections.ArrayList), ExpectedResult = typeof(object))]
        [TestCase(typeof(List<string>), ExpectedResult = typeof(string))]
        [TestCase(typeof(IEnumerable<string>), ExpectedResult = typeof(string))]
        public Type TestGetCollectionType(Type type) {
            if(new ConvertTypeToTypeScriptType().GetCollectionType(type, out Type newType)){
                return newType;
            }
            else
            {
                return null;
            }
        }
    }
}
