using Albatross.CodeGen.Python.Expressions;
using FluentAssertions;
using System;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestIdentifierAndDocString {
	[Fact]
	public void IdentifierNameExpression_ShouldThrowForInvalidName() {
		var action = () => new IdentifierNameExpression("my-name");

		action.Should().Throw<ArgumentException>()
			.WithMessage("Invalid identifier name*");
	}

	[Fact]
	public void IdentifierNameExpression_ShouldSupportForwardReference() {
		var expression = new IdentifierNameExpression("my_type") {
			ForwardReference = true
		};

		var text = expression.Render();

		text.Should().Be("'my_type'");
	}

	[Fact]
	public void DocStringExpression_ShouldRenderSingleAndMultiLineForms() {
		var single = new DocStringExpression("one line").Render();
		var multi = new DocStringExpression("first\nsecond").Render();

		single.Should().Be("\"\"\" one line \"\"\"");
		multi.Should().Contain("\"\"\"");
		multi.Should().Contain("first");
		multi.Should().Contain("second");
	}
}
