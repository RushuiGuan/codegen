using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using FluentAssertions;
using System;
using Xunit;

namespace Albatross.CodeGen.CSharp.UnitTest;

public class TestParameterAndNamespaceExpression {
	[Fact]
	public void Parameter_ShouldOmitType_WhenInferred() {
		var parameter = new ParameterDeclaration {
			Type = Defined.Types.Var,
			Name = new IdentifierNameExpression("value")
		};

		var text = parameter.Render();

		text.Should().Be("value");
	}

	[Fact]
	public void Parameter_ShouldIncludeThisKeyword_WhenConfigured() {
		var parameter = new ParameterDeclaration {
			UseThisKeyword = true,
			Type = Defined.Types.String,
			Name = new IdentifierNameExpression("value")
		};

		var text = parameter.Render();

		text.Should().Be("this string value");
	}

	[Fact]
	public void NamespaceExpression_ShouldThrowForInvalidIdentifier() {
		var action = () => new NamespaceExpression("invalid-namespace");

		action.Should().Throw<ArgumentException>()
			.WithMessage("Invalid namespace identifier:*");
	}
}
