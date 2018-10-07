using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestTabify {



		[TestCase("a\r\nb", 1, false, ExpectedResult = "\ta\r\n\tb")]
		[TestCase("a\nb", 1, false, ExpectedResult = "\ta\n\tb")]
		[TestCase("a\nb\nc", 1, false, ExpectedResult = "\ta\n\tb\n\tc")]

		[TestCase("a\n\nb", 1, false, ExpectedResult = "\ta\n\t\n\tb")]
		[TestCase("a\n\nb", 1, true, ExpectedResult = "\ta\n\tb")]
		[TestCase("a\n\n\nb\n\n\n", 1, true, ExpectedResult = "\ta\n\tb\n")]
		[TestCase("a\nb", 1, true, ExpectedResult = "\ta\n\tb")]

		[TestCase("test\n", 1, false, ExpectedResult = "\ttest\n")]
		[TestCase("test\n", 1, true, ExpectedResult = "\ttest\n")]
		[TestCase("test\n\n", 1, true, ExpectedResult = "\ttest\n")]

		[TestCase("test", 1, false, ExpectedResult = "\ttest")]

		[TestCase("\n\n\n\n", 1, false, ExpectedResult ="\t\n\t\n\t\n\t\n")]
		[TestCase("\n\n\n\n", 1, true, ExpectedResult = "")]
		public string Run(string input, int count, bool removeBlankLines) {
			StringBuilder sb = new StringBuilder();
			sb.Tabify(input, count, removeBlankLines);
			return sb.ToString();
		}
	}
}
