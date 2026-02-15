using Albatross.CodeGen.Python.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestExpressions {
	[Fact]
	public void StringInterpolation_ShouldUseFPrefix_WhenNonLiteralExists() {
		var expression = new StringInterpolationExpression(
			new StringLiteralExpression("hello "),
			new IdentifierNameExpression("name"));

		var text = expression.Render();

		text.Should().Be("f\"hello {name}\"");
	}

	[Fact]
	public void Invocation_ShouldRenderAwait_WhenEnabled() {
		var expression = new InvocationExpression {
			UseAwaitOperator = true,
			CallableExpression = new IdentifierNameExpression("fetch"),
			Arguments = [new IdentifierNameExpression("arg")]
		};

		var text = expression.Render();

		text.Should().Be("await fetch(arg)");
	}

	[Fact]
	public void ReturnExpression_ShouldOmitNoneValue() {
		var expression = new ReturnExpression(new NoneLiteralExpression());

		var text = expression.Render();

		text.Should().Be("return");
	}

	[Fact]
	public void IfExpression_ShouldRenderElseBlock_WhenPresent() {
		var expression = new IfExpression {
			Condition = new IdentifierNameExpression("ok")
		};
		expression.CodeBlock.Add(new ReturnExpression(new StringLiteralExpression("yes")));
		expression.ElseBlock.Add(new ReturnExpression(new StringLiteralExpression("no")));

		var text = expression.Render();

		text.Should().Contain("if (ok):");
		text.Should().Contain("return \"yes\"");
		text.Should().Contain("else:");
		text.Should().Contain("return \"no\"");
	}

	[Fact]
	public void ListComprehension_ShouldRenderExpressionAndIteration() {
		var expression = new ListComprehensionExpression {
			Expression = new IdentifierNameExpression("x"),
			VariableName = "x",
			IterableExpression = new IdentifierNameExpression("items"),
		};

		var text = expression.Render();

		text.Should().Be("[x for x in items]");
	}
}
