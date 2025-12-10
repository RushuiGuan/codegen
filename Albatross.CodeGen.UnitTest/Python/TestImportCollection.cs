using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Expressions;
using System.Linq;
using Xunit;

namespace Albatross.CodeGen.UnitTest.Python {
	public class TestImportCollection {
		[Fact]
		public void TestImportCollectionDeduplicates() {
			var import1 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
				Source = new ModuleSourceExpression("os"),
			};
			Assert.Equal(2, import1.Symbols.Count());
			var import2 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
				Source = new ModuleSourceExpression("os"),
			};
			Assert.Equal(2, import1.Symbols.Count());

			var collection = new ImportCollection(import1, import2);
			Assert.Single(collection.Imports);
			Assert.Equal(2, collection.Imports.First().Symbols.Count());
		}

		[Fact]
		public void TestImportCollectionCounting() {
			var import1 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
				Source = new ModuleSourceExpression("os1"),
			};
			Assert.Equal(2, import1.Symbols.Count());
			var import2 = new ImportExpression(new IdentifierNameExpression("file"), new IdentifierNameExpression("file2")) {
				Source = new ModuleSourceExpression("os2"),
			};
			Assert.Equal(2, import1.Symbols.Count());

			var collection = new ImportCollection(import1, import2);
			Assert.Equal(2, collection.Imports.Count());
			Assert.Equal(2, collection.Imports.First().Symbols.Count());
			Assert.Equal(2, collection.Imports.Last().Symbols.Count());
		}

		[Fact]
		public void TestQualifiedIdentifierImportCollectionDeduplicates() {
			var name1 = new QualifiedIdentifierNameExpression("file", Defined.Sources.RequestsAuth);
			var name2 = new QualifiedIdentifierNameExpression("file2", Defined.Sources.RequestsAuth);
			var collection = new ImportCollection(name1, name2);
			Assert.Single(collection.Imports);
			Assert.Equal(2, collection.Imports.First().Symbols.Count());
		}
	}
}