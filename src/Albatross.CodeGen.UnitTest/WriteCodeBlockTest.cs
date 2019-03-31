using System;
using System.IO;
using Albatross.CodeGen.CSharp.Model;
using Albatross.CodeGen.CSharp.Writer;
using Albatross.CodeGen.Core;
using NUnit.Framework;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture(TestOf =typeof(WriteCodeBlock))]
	public class WriteCodeBlockTest {


		[TestCase("test;", ExpectedResult ="{\n\ttest;\n}\n")]
		public string Run(string input) {
			StringWriter writer = new StringWriter();
			writer.Run(new WriteCodeBlock(), new CodeBlock { Content = input, });
			return writer.ToString().RemoveCarriageReturn();
		}
	}
}
