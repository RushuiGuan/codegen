using Albatross.CodeGen.TypeScript.Declarations;
using Albatross.CodeGen.TypeScript.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.TypeScript.UnitTest;

public class TestDeclarations {
	[Fact]
	public void ConstructorDeclaration_ShouldNotRenderReturnType() {
		var declaration = new ConstructorDeclaration();

		var text = declaration.Render();

		text.Should().Be("constructor() {}");
	}

	[Fact]
	public void InterfaceDeclaration_ShouldRenderBaseInterfaceAndProperties() {
		var declaration = new InterfaceDeclaration("MyContract") {
			BaseInterfaceName = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("BaseContract")
			},
			Properties = [
				new PropertyDeclaration("id") { Type = Defined.Types.Numeric() }
			]
		};

		var text = declaration.Render();

		text.Should().Contain("export interface MyContract extends BaseContract");
		text.Should().Contain("id: number;");
	}

	[Fact]
	public void ClassDeclaration_ShouldRenderDecoratorExtendsAndMembers() {
		var declaration = new ClassDeclaration("MyService") {
			BaseClassName = new IdentifierNameExpression("BaseService"),
			Decorators = [
				new DecoratorExpression {
					CallableExpression = new IdentifierNameExpression("Injectable")
				}
			],
			Properties = [
				new PropertyDeclaration("id") { Type = Defined.Types.Numeric() }
			],
			Methods = [
				new MethodDeclaration("getName") {
					ReturnType = Defined.Types.String()
				}
			]
		};

		var text = declaration.Render();

		text.Should().Contain("@Injectable()");
		text.Should().Contain("export class MyService extends BaseService");
		text.Should().Contain("id: number");
		text.Should().Contain("getName(): string");
	}
}
