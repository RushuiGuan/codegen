using Albatross.Text;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	/// <summary>
	/// On rare occasions, a rosyln syntax node is the source of truth for code generation.  Use this expression to use it directly as a code node
	/// </summary>
	public record SyntaxNodeExpression : CodeNode, IExpression {
		private readonly SyntaxNode syntaxNode;
		private QualifiedIdentifierNameExpression[] dependencies;

		public SyntaxNodeExpression(SyntaxNode syntaxNode, SemanticModel semanticModel) {
			this.syntaxNode = syntaxNode;
			this.dependencies = syntaxNode.DescendantNodes().Select(n => semanticModel.GetTypeInfo(n).Type)
				.Where(t => t is not null && !t.ContainingNamespace.IsGlobalNamespace)
				.Select(t => t!.ContainingNamespace.ToDisplayString()).Distinct()
				.Select(x => new QualifiedIdentifierNameExpression("_", new NamespaceExpression(x))).ToArray();

		}
		public override TextWriter Generate(TextWriter writer) => writer.Append(syntaxNode.NormalizeWhitespace().ToFullString());
		public override IEnumerable<ICodeNode> Children => this.dependencies;
	}
}