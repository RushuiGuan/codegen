using System;
using System.IO;
using Albatross.CodeGen.Core;
using NUnit.Framework;

namespace Albatross.CodeGen.UnitTest {
	[TestFixture]
	public class CodeGeneratorScopeTest {

		[Test(ExpectedResult ="{\n\tint i = 100;\n}\n")]
		public string SingleScope() {
			StringWriter writer = new StringWriter();
			CodeGeneratorScope scope = new CodeGeneratorScope(writer, args => args.WriteLine("{"), args => args.WriteLine("}"));
			using (scope) {
				scope.Writer.WriteLine("int i = 100;");
			}
			return writer.ToString();
		}


		const string NestedScopeResult = @"{
	int i = 100;
	test {
		int a = 200;
	}
}
";
		[Test(ExpectedResult = NestedScopeResult)]
		public string NestedScope() {
			StringWriter writer = new StringWriter();
			using (CodeGeneratorScope scope = new CodeGeneratorScope(writer, args => args.WriteLine("{"), args => args.WriteLine("}"))){
		
				scope.Writer.WriteLine("int i = 100;");
				using (var child = scope.Writer.BeginScope("test")) {
					child.Writer.WriteLine("int a = 200;");
				}
			}
			return writer.ToString().Replace("\n", "\r\n");
		}
	}
}
