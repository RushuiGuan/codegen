using Albatross.CodeGen.CSharp.Expressions;
using Albatross.Testing;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestCodeBlock {
		[Fact]
		public void TestSingleline() {
			var codeBlock = new CSharpCodeBlock {
				new AssignmentExpression {
					Left = new IdentifierNameExpression("x"),
					Expression = new IntLiteralExpression(5)
				}
			};
			var writer = new StringWriter();
			writer.Code(codeBlock);
			var text = writer.ToString().NormalizeLineEnding();
			Assert.Equal(" {\n\tx = 5;\n}", text);
		}

		[Fact]
		public void TestMultiline() {
			var codeBlock = new CSharpCodeBlock {
				new AssignmentExpression {
					Left = new IdentifierNameExpression("x"),
					Expression = new IntLiteralExpression(5)
				},
				new AssignmentExpression {
					Left = new IdentifierNameExpression("y"),
					Expression = new IntLiteralExpression(10)
				}
			};
			var writer = new StringWriter();
			writer.Code(codeBlock);
			var text = writer.ToString().NormalizeLineEnding();
			Assert.Equal(" {\n\tx = 5;\n\ty = 10;\n}", text);
		}
	}
}