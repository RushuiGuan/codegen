using Albatross.CodeGen.CSharp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class TestWriteCSharpScopedObject {

		[TestCase(ExpectedResult = @"namespace System.Text {
	public class Test {
		int i = 0;
	}
}")]
		public string Run() {
			StringBuilder sb = new StringBuilder();
			using (var namespaceWriter = new WriteCSharpScopedObject(sb).BeginScope("namespace System.Text")) {
				using (var classWriter = new WriteCSharpScopedObject(namespaceWriter.Content).BeginScope("public class Test")) {
					classWriter.Content.Append("int i = 0;");
				}
			}
			return sb.ToString();
		}
	}
}
