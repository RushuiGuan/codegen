using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestStringExtensions {
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
		public string TestTabify(string input, int count, bool removeBlankLines) {
			StringBuilder sb = new StringBuilder();
			sb.Tabify(input, count, removeBlankLines);
			return sb.ToString();
		}

		[TestCase("   ", 1, ExpectedResult = " ")]
		[TestCase(" a ", 0, ExpectedResult = " a")]
		[TestCase("a", 0, ExpectedResult = "a")]
		[TestCase("", 0, ExpectedResult = "")]
		[TestCase("\n", 0, ExpectedResult = "")]
		public string TestTrimEnd(string input, int offset) {
			return new StringBuilder(input).TrimEnd(offset).ToString();
		}

		[TestCase("a,; ", 0, new char[] {';', ','}, ExpectedResult ="a")]
		public string TestTrimAdditional(string input, int offset, char[] array) {
			return new StringBuilder(input).TrimEnd(offset, array).ToString();
		}
	}
}
