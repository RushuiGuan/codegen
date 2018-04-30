using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest
{
	public class TestClass : List<string> { }

	[TestFixture]
    public class RenderDotNetTypeTest {
		public class TestInnerClass {
			public class TestInnerInnerClass { }
		}

		public static IEnumerable<TestCaseData> GetTestCases() {
			TestInnerClass.TestInnerInnerClass c = new TestInnerClass.TestInnerInnerClass();

			return new TestCaseData[] {
				new TestCaseData(typeof(string), true){ ExpectedResult = "string"},
				new TestCaseData(typeof(string), false){ ExpectedResult = "string"},
				new TestCaseData(typeof(int), true){ ExpectedResult = "int?"},
				new TestCaseData(typeof(int), false){ ExpectedResult = "int"},
				new TestCaseData(typeof(int?), true){ ExpectedResult = "int?"},
				new TestCaseData(typeof(TestClass), false){ ExpectedResult = "Albatross.CodeGen.UnitTest.TestClass"},
				new TestCaseData(typeof(TestInnerClass), false){ ExpectedResult = "Albatross.CodeGen.UnitTest.RenderDotNetTypeTest.TestInnerClass"},
				new TestCaseData(typeof(TestInnerClass.TestInnerInnerClass), false){ ExpectedResult = "Albatross.CodeGen.UnitTest.RenderDotNetTypeTest.TestInnerClass.TestInnerInnerClass"},
				new TestCaseData(typeof(DateTime), false){ ExpectedResult = "DateTime"},
			};
		}


		[TestCaseSource(nameof(GetTestCases))]
		public string Run(Type type, bool nullable) {
			return new RenderDotNetType().Render(new StringBuilder(), type, nullable).ToString();
		}
    }
}
