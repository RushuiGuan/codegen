using Albatross.CodeGen.TypeScript.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.TypeScript.UnitTest;

public class TestExpressions {
	[Fact]
	public void TypeScriptCodeBlock_ShouldTerminateNonBlockStatements() {
		var block = new TypeScriptCodeBlock {
			new ReturnExpression(new NumberLiteralExpression(1))
		};

		var text = block.Render();

		text.Should().Contain("return 1;");
	}

	[Fact]
	public void StringInterpolation_ShouldRenderTemplateExpression() {
		var expression = new StringInterpolationExpression(
			new StringLiteralExpression("hello "),
			new IdentifierNameExpression("name"));

		var text = expression.Render();

		text.Should().Be("`hello ${name}`");
	}

	[Fact]
	public void JsonProperty_ShouldUseShorthand_WhenIdentifierMatchesExpression() {
		var expression = new JsonPropertyExpression("id", new IdentifierNameExpression("id"));

		var text = expression.Render();

		text.Should().Be("id");
	}

	[Fact]
	public void ExportExpression_ShouldRenderWildcardAndNamedForms() {
		var wildcard = new ExportExpression {
			Source = new FileNameSourceExpression("model.generated.ts")
		};
		var named = new ExportExpression {
			Source = new FileNameSourceExpression("model.generated.ts"),
			Items = [new IdentifierNameExpression("MyType")]
		};

		wildcard.Render().Trim().Should().Be("export * from './model.generated';");
		named.Render().Trim().Should().Be("export {MyType} from './model.generated';");
	}

	[Fact]
	public void ArrayTypeExpression_ShouldWrapMultiTypeWithParentheses() {
		var expression = new ArrayTypeExpression {
			Type = new MultiTypeExpression {
				Defined.Types.String(),
				Defined.Types.Undefined()
			}
		};

		var text = expression.Render();

		text.Should().Be("(string|undefined)[]");
	}
}
