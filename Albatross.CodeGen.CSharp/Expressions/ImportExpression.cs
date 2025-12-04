using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ImportExpression : ISyntaxNode, ICodeElement {
		public IdentifierNameExpression? Alias { get; init; }
		public NamespaceExpression Namespace { get; init; }

		public ImportExpression(string name){
			Namespace = new NamespaceExpression(name);
		}
		
		public TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.Using);
			if (Alias != null) {
				writer.Code(Alias).Append(" = ");
			}
			writer.Code(Namespace).Semicolon().WriteLine();
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>();
			if (Alias != null) {
				list.Add(Alias);
			}
			list.Add(Namespace);
			return list;
		}
	}
}