using Albatross.CodeGen.CSharp.Expressions;
using System.IO;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestStringInterpolationExpression {
		[Theory]
		[InlineData("hello", "\"hello\"")]
		[InlineData("a\rb", "\"a\\rb\"")]
		public void TestLiteral(string text, string expected) {
			var result = new StringInterpolationExpression {
				Items = { new StringLiteralExpression(text) }
			};
			var writer = new StringWriter();
			writer.Code(result);
			Assert.Equal(expected, writer.ToString());
		}
	}
}