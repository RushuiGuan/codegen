using Xunit;

namespace Albatross.CodeGen.UnitTest.Python {
	public class TestLiteral {

		[Theory]
		[InlineData(true, "True")]
		public void TestBoolean(bool value, string expected) {
			var literal = new Albatross.CodeGen.Python.Expressions.BooleanLiteralExpression(value);
			var writer = new System.IO.StringWriter();
			literal.Generate(writer);
			var actual = writer.ToString();
			Assert.Equal(expected, actual);
		}
	}
}