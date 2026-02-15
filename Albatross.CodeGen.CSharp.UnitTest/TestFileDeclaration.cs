using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.CSharp.UnitTest;

public class TestFileDeclaration {
	[Fact]
	public void Generate_ShouldIncludeNullableAndNamespaceAndDiscoveredImports() {
		var file = new FileDeclaration("sample") {
			Namespace = new NamespaceExpression("Demo.Client"),
			Classes = [
				new ClassDeclaration {
					Name = new IdentifierNameExpression("MyClient"),
					Fields = [
						new FieldDeclaration {
							AccessModifier = null,
							Type = new TypeExpression(Defined.Identifiers.HttpClient),
							Name = new IdentifierNameExpression("client")
						}
					]
				}
			]
		};

		var text = file.Render();

		text.Should().Contain("using System.Net.Http;");
		text.Should().Contain("#nullable enable");
		text.Should().Contain("namespace Demo.Client");
		text.Should().Contain("class MyClient");
	}

	[Fact]
	public void Generate_ShouldSkipNullableDirective_WhenDisabled() {
		var file = new FileDeclaration("sample") {
			NullableEnabled = false,
			Classes = [
				new ClassDeclaration {
					Name = new IdentifierNameExpression("MyClient")
				}
			]
		};

		var text = file.Render();

		text.Should().NotContain("#nullable enable");
	}
}
