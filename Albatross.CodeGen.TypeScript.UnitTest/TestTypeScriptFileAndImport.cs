using Albatross.CodeGen.TypeScript.Declarations;
using Albatross.CodeGen.TypeScript.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.TypeScript.UnitTest;

public class TestTypeScriptFileAndImport {
	[Fact]
	public void ImportCollection_ShouldDeduplicateAndSortItems() {
		var imports = new ImportCollection([
			new ImportExpression([new IdentifierNameExpression("B"), new IdentifierNameExpression("A")]) {
				Source = new ModuleSourceExpression("rxjs")
			},
			new ImportExpression([new IdentifierNameExpression("A")]) {
				Source = new ModuleSourceExpression("rxjs")
			}
		]);

		var text = imports.Render();

		text.Should().Contain("import { A, B } from \"rxjs\";");
	}

	[Fact]
	public void TypeScriptFileDeclaration_ShouldSkipSelfImport() {
		var file = new TypeScriptFileDeclaration("dto.generated") {
			ImportDeclarations = [
				new ImportExpression([new IdentifierNameExpression("SelfType")]) {
					Source = new FileNameSourceExpression("dto.generated.ts")
				}
			],
			InterfaceDeclarations = [
				new InterfaceDeclaration("Person")
			]
		};

		var text = file.Render();

		text.Should().NotContain("from './dto.generated'");
		text.Should().Contain("export interface Person");
	}
}
