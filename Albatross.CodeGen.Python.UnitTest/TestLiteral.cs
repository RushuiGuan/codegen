using Albatross.CodeGen.Python.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestLiteral {
	[Theory]
	[InlineData(true, "True")]
	[InlineData(false, "False")]
	public void TestBoolean(bool value, string expected) {
		var literal = new BooleanLiteralExpression(value);

		var actual = literal.Render();

		actual.Should().Be(expected);
	}
}
