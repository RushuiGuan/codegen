using Albatross.CodeGen.Python.Expressions;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestImportCollection {
	[Fact]
	public void TestImportCollectionDeduplicates() {
		var import1 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
			Source = new ModuleSourceExpression("os"),
		};
		import1.Symbols.Should().HaveCount(2);
		var import2 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
			Source = new ModuleSourceExpression("os"),
		};
		import2.Symbols.Should().HaveCount(2);

		var collection = new ImportCollection(import1, import2);
		collection.Imports.Should().ContainSingle();
		collection.Imports.First().Symbols.Should().HaveCount(2);
	}

	[Fact]
	public void TestImportCollectionCounting() {
		var import1 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
			Source = new ModuleSourceExpression("os1"),
		};
		import1.Symbols.Should().HaveCount(2);
		var import2 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
			Source = new ModuleSourceExpression("os2"),
		};
		import2.Symbols.Should().HaveCount(2);

		var collection = new ImportCollection(import1, import2);
		collection.Imports.Should().HaveCount(2);
		collection.Imports.First().Symbols.Should().HaveCount(2);
		collection.Imports.Last().Symbols.Should().HaveCount(2);
	}

	[Fact]
	public void TestQualifiedIdentifierImportCollectionDeduplicates() {
		var name1 = new QualifiedIdentifierNameExpression("file", Defined.Sources.RequestsAuth);
		var name2 = new QualifiedIdentifierNameExpression("file2", Defined.Sources.RequestsAuth);
		var collection = new ImportCollection(name1, name2);
		collection.Imports.Should().ContainSingle();
		collection.Imports.First().Symbols.Should().HaveCount(2);
	}
}
