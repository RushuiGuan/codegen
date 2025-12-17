using Albatross.Text;
using Microsoft.CodeAnalysis;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	/// <summary>
	/// On rare occasions, a rosyln syntax node is the source of truth for code generation.  Use this expression to use it directly as a code node
	/// </summary>
	public record SyntaxNodeExpression : CodeNode, IExpression {
		private readonly SyntaxNode syntaxNode;

		public SyntaxNodeExpression(SyntaxNode syntaxNode) {
			this.syntaxNode = syntaxNode;
		}

		public override TextWriter Generate(TextWriter writer)
			=> writer.Append(syntaxNode.NormalizeWhitespace().ToFullString());
	}
}