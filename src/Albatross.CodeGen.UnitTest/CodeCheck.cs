using Albatross.CodeGen.Core;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class CodeCheck {

		[TestCase(@"c:\temp\test.txt", ExpectedResult =@"c:\temp")]
		[TestCase(@"c:\temp\test*.txt", ExpectedResult = @"c:\temp")]
		public string PathCheck(string path) {
			return Path.GetDirectoryName(path);
		}


		[TestCase(@"c:\temp\test.txt", ExpectedResult = @"test.txt")]
		[TestCase(@"c:\temp\test*.txt", ExpectedResult = @"test*.txt")]
		public string FileCheck(string path) {
			return Path.GetFileName(path);
		}

		[Test]
		public void GenericType() {
			Type type = typeof(ICodeGenerator<,>);
			type.ToString();
		}

		[Test]
		public void CovarianceTest() {
			ICodeGenerator<object, object> a = null;
			ICodeGenerator<string, string> b = a;
			Assert.True(typeof(object).IsAssignableFrom(typeof(string)));
		}

		[TestCase("_abc", ExpectedResult = false)]
		[TestCase("", ExpectedResult = false)]
		[TestCase("abc", ExpectedResult = true)]
		[TestCase("abc(1, 2)", ExpectedResult = true)]
		[TestCase("abc(1, 2, 3)", ExpectedResult = false)]
		public bool PatternCheck(string text) {
			const string Pattern = 
@"^\s* 
	([a-zA-Z]+) 
	( 
		\((\d+)(\s*,\s*(\d+))?\s*\) 
	)?
\s*$";
			Regex regex = new Regex(Pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
			Match match = regex.Match(text);
			return match.Success;
		}
	}
}
