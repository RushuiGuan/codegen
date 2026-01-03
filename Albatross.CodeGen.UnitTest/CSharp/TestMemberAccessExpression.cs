using Albatross.CodeGen.CSharp.Expressions;
using Albatross.Testing;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestMemberAccessExpression {
		[Fact]
		public void Test_NoNewLine() {
			var expression = new IdentifierNameExpression("myObject");
			var result = expression.Chain(false, new IdentifierNameExpression("MyProperty"), new IdentifierNameExpression("MyMethod"));

			var writer = new StringWriter();
			writer.Code(result);
			Assert.Equal("myObject.MyProperty.MyMethod", writer.ToString());
		}

		[Fact]
		public void Test_WithNewLine() {
			var expression = new IdentifierNameExpression("myObject");
			var result = expression.Chain(true, new IdentifierNameExpression("MyProperty"), new IdentifierNameExpression("MyMethod"));

			var writer = new StringWriter();
			writer.Code(result);
			Assert.Equal("myObject\r\n\t.MyProperty\r\n\t.MyMethod", writer.ToString());
		}
	}
}

