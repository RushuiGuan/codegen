using Albatross.CodeGen.Python.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestRegex {
	[Theory]
	[InlineData(".xyz", true)]
	[InlineData("..xyz", true)]
	[InlineData("xyz", true)]
	[InlineData("x.y.z", true)]
	[InlineData("z.", false)]
	public void TestModuleRegex(string text, bool expected) {
		var match = ModuleSourceExpression.ModuleSource.Match(text);

		match.Success.Should().Be(expected);
	}
}
