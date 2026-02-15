using Albatross.CodeGen.CSharp.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.CSharp.UnitTest;

public class TestUsingAndSwitchExpression {
	[Fact]
	public void UsingExpression_ShouldRenderStatementForm_WhenBodyIsEmpty() {
		var expression = new UsingExpression {
			Resource = new IdentifierNameExpression("stream")
		};

		var text = expression.Render();

		text.Should().Be("using stream;");
	}

	[Fact]
	public void UsingExpression_ShouldRenderScopedForm_WhenBodyHasStatements() {
		var expression = new UsingExpression {
			Resource = new IdentifierNameExpression("stream")
		};
		expression.Body.Add(new ReturnExpression {
			Expression = new IdentifierNameExpression("stream")
		});

		var text = expression.Render();

		text.Should().Be("using (stream) {\n\treturn stream;\n}");
	}

	[Fact]
	public void SwitchExpression_ShouldRenderDefaultLambda_WhenDefaultExpressionIsSet() {
		var expression = new SwitchExpression {
			Value = new IdentifierNameExpression("value"),
			DefaultExpression = new StringLiteralExpression("other"),
			Sections = new ListOfNodes<SwitchCaseExpression> {
				new SwitchCaseExpression {
					Value = new IntLiteralExpression(1),
					Expression = new StringLiteralExpression("one")
				}
			}
		};

		var text = expression.Render();

		text.Should().Contain("value switch");
		text.Should().Contain("1 => \"one\"");
		text.Should().Contain("_ => \"other\"");
	}
}
