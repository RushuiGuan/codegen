using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class QualifiedIdentifierNameExpression : SyntaxNode, IIdentifierNameExpression {
		public IdentifierNameExpression Identifier { get; }
		public NamespaceExpression Source { get; }
		
		public QualifiedIdentifierNameExpression(string name, NamespaceExpression source) {
			Identifier = new IdentifierNameExpression(name);
			this.Source = source;
		}
		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [Identifier, Source];
	}
}