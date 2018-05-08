using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class ReadCustomCodeTest {
		[TestCase(@"#region <albatross.codegen.csharp name=""body"">
	test
#endregion</albatross.codegen.csharp>", "body", ExpectedResult = "\ttest\r\n")]

		[TestCase(@"#region <albatross.codegen.csharp name=""body""> this line is not read
	test1
	test2
#endregion</albatross.codegen.csharp>", "body", ExpectedResult = "\ttest1\r\n\ttest2\r\n")]

		public string ReadCSharpCustomCodeTest(string text, string tag) {
			Dictionary<string, string> dict = new ReadCSharpCustomCode().Read(text);
			return dict[tag];
		}
	}
}
