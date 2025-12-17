using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ImportExpression : CodeNode {
		public IdentifierNameExpression? Alias { get; init; }
		public NamespaceExpression Namespace { get; init; }

		public ImportExpression(string name) {
			Namespace = new NamespaceExpression(name);
		}
		public ImportExpression(NamespaceExpression namespaceExpression) {
			Namespace = namespaceExpression;
		}

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.Using);
			if (Alias != null) {
				writer.Code(Alias).Append(" = ");
			}
			writer.Code(Namespace).Semicolon();
			return writer;
		}
		public override IEnumerable<ICodeNode> Children => new ICodeNode[] { Namespace }.UnionIfNotNull(Alias);
	}
}