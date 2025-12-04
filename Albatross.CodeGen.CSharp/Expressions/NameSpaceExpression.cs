using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NamespaceExpression : SyntaxNode, ISourceExpression {
		public IdentifierNameExpression Name { get; init; }

		public NamespaceExpression(string name) {
			Name = new IdentifierNameExpression(name);
		}

		public override IEnumerable<ISyntaxNode> Children => [];

		public string Source => this.Name.Name;

		public override TextWriter Generate(TextWriter writer) => writer.Code(Name);
	}
}