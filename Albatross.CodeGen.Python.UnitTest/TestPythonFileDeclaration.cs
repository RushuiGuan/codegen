using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using FluentAssertions;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestPythonFileDeclaration {
	[Fact]
	public void Generate_ShouldExcludeSelfImport_AndCollectQualifiedImports() {
		var file = new PythonFileDeclaration("dto") {
			Banner = [new CommentDeclaration("@generated")],
			Imports = [
				new ImportExpression(new IdentifierNameExpression("MyDto")) { Source = new ModuleSourceExpression("dto") },
				new ImportExpression(new IdentifierNameExpression("BaseModel")) { Source = new ModuleSourceExpression("pydantic") }
			],
			Classes = [
				new ClassDeclaration("Sample") {
					Fields = [
						new FieldDeclaration("payload") { Type = Defined.Types.Any }
					]
				}
			]
		};

		var text = file.Render();

		text.Should().Contain("# @generated");
		text.Should().Contain("from pydantic import BaseModel");
		text.Should().Contain("from typing import Any");
		text.Should().NotContain("from dto import MyDto");
		text.Should().Contain("class Sample:");
		text.Should().Contain("    payload: Any");
	}
}
