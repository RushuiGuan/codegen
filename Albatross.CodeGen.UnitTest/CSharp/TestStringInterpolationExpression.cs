using Albatross.CodeGen.CSharp.Expressions;
using Xunit;

namespace Albatross.CodeGen.UnitTest.CSharp {
	public class TestStringInterpolationExpression {
		[Theory]
		[InlineData("hello", "hello")]
		public void TestLiteral(string text, string expected) {
			var result = new StringInterpolationExpression {
				Expressions = [new StringLiteralExpression(text)]
			};
			Assert.Equal(expected, result.ToString());
		}
	}
}
