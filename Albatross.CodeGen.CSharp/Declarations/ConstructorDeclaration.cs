using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Modifiers;
using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using NoOpExpression = Albatross.CodeGen.Syntax.NoOpExpression;

namespace Albatross.CodeGen.CSharp.Declarations {
	public class ConstructorDeclaration : IDeclaration, ISyntaxNode {
		public AccessModifier? AccessModifier { get; init; } = AccessModifier.Public;
		public required IdentifierNameExpression Name { get; init; }
		public IEnumerable<ParameterDeclaration> Parameters { get; init; } = [];
		public IExpression Body { get; init; } = new NoOpExpression();

		public TextWriter Generate(TextWriter writer) {
			if (AccessModifier != null) {
				writer.Append(AccessModifier.Name).Space();
			}
			writer.Code(Name);
			writer.WriteItems(Parameters, ",", (w, item) => w.Code(item), "(", ")");
			using var scope = writer.BeginScope();
			scope.Writer.Code(Body);
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => new List<ISyntaxNode>(Parameters) {
			Name,
			Body
		};
	}
}