using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Python.Keywords;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestDeclarations {
	[Fact]
	public void ClassDeclaration_ShouldRenderPass_WhenEmpty() {
		var declaration = new ClassDeclaration("empty");

		var text = declaration.Render();

		text.Should().Be("class empty:\n    pass");
	}

	[Fact]
	public void MethodDeclaration_ShouldRenderDocStringAndBody() {
		var declaration = new MethodDeclaration("get_value") {
			Parameters = [
				new ParameterDeclaration {
					Identifier = Defined.Identifiers.Self,
					Type = Defined.Types.None
				}
			],
			ReturnType = Defined.Types.Int,
			Modifiers = [new AsyncKeyword()],
			DocString = new DocStringExpression("line one\nline two")
		};
		declaration.Body.Add(new ReturnExpression(new IntLiteralExpression(2)));

		var text = declaration.Render();

		text.Should().Contain("async def get_value(self) -> int:");
		text.Should().Contain("\"\"\"");
		text.Should().Contain("line one");
		text.Should().Contain("line two");
		text.Should().Contain("return 2");
	}

	[Fact]
	public void GetPropertyDeclaration_ShouldIncludePropertyDecoratorAndSelfParameter() {
		var declaration = new GetPropertyDeclaration("name");

		var text = declaration.Render();

		text.Should().Contain("@property()");
		text.Should().Contain("def name(self) -> None:");
	}

	[Fact]
	public void FieldAndParameterDeclaration_ShouldRenderTypeAndDefaultValue() {
		var field = new FieldDeclaration("count") {
			Type = Defined.Types.Int,
			Initializer = new IntLiteralExpression(5)
		};
		var parameter = new ParameterDeclaration {
			Identifier = new IdentifierNameExpression("count"),
			Type = Defined.Types.Int,
			DefaultValue = new IntLiteralExpression(1)
		};

		field.Render().Should().Be("count: int = 5");
		parameter.Render().Should().Be("count: int = 1");
	}
}
