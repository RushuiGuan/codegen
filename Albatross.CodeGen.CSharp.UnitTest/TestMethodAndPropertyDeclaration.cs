using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.Testing;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.CSharp.UnitTest;

public class TestMethodAndPropertyDeclaration {
	[Fact]
	public void Method_ShouldEndWithSemicolon_WhenNoBody() {
		var method = new MethodDeclaration {
			AccessModifier = Defined.Keywords.Public,
			IsStatic = true,
			IsAsync = true,
			ReturnType = Defined.Types.Task,
			Name = new IdentifierNameExpression("Run")
		};

		var text = method.Render();

		text.Should().Be("public static async Task Run();");
	}

	[Fact]
	public void Method_ShouldRenderBody_WhenStatementsExist() {
		var method = new MethodDeclaration {
			AccessModifier = Defined.Keywords.Public,
			ReturnType = Defined.Types.Int,
			Name = new IdentifierNameExpression("GetValue")
		};
		method.Body.Add(new ReturnExpression {
			Expression = new IntLiteralExpression(10)
		});

		var text = method.Render();

		text.Should().Be("public int GetValue() {\n\treturn 10;\n}");
	}

	[Fact]
	public void Property_ShouldRenderAutoPropertyByDefault() {
		var property = new PropertyDeclaration {
			Type = Defined.Types.String,
			Name = new IdentifierNameExpression("Name")
		};

		var text = property.Render().NormalizeLineEnding();

		text.Should().Be("public string Name {\n\tget;\n\tset;\n}");
	}

	[Fact]
	public void Property_ShouldSupportCustomGetterAndSetterBodies() {
		var property = new PropertyDeclaration {
			Type = Defined.Types.Int,
			Name = new IdentifierNameExpression("Count"),
			GetterBody = new ReturnExpression {
				Expression = new IdentifierNameExpression("_count")
			},
			SetterBody = new AssignmentExpression {
				Left = new IdentifierNameExpression("_count"),
				Expression = new IdentifierNameExpression("value")
			}
		};

		var text = property.Render();

		text.Should().Contain("get {");
		text.Should().Contain("return _count;");
		text.Should().Contain("set {");
		text.Should().Contain("_count = value;");
	}
}
