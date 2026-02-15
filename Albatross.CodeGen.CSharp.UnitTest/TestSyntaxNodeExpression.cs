using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.CSharp.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.CSharp.UnitTest {
	public class TestSyntaxNodeExpression {
		[Fact]
		public async Task VerifySyntaxNodeNamespaceDependency() {
			var code = @"using System;
public class MyClass {
	public DateTime MyProperty { get; set; }
}";

			var compiler = await code.CreateNet8CompilationAsync();
			var syntaxTree = compiler.SyntaxTrees[0];
			var model = compiler.GetSemanticModel(syntaxTree);
			var classNode = syntaxTree.GetRoot().DescendantNodes().First(n => n is Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax);
			var expression = new SyntaxNodeExpression(classNode, model);
			Assert.Contains("System", expression.Children.OfType<QualifiedIdentifierNameExpression>().Select(d => d.Source.Source));
		}
	}
}
