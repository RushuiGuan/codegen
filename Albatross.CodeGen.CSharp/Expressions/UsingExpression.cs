using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class UsingExpression : ISyntaxNode, ICodeElement {
		public IdentifierNameExpression? Alias { get; init; }
		public required NameSpaceExpression NameSpace { get; init; }
		
		public TextWriter Generate(TextWriter writer) {
			writer.Append("using ");
			if (Alias != null) {
				writer.Code(Alias).Append(" = ");
			}
			writer.Code(NameSpace).Semicolon().WriteLine();
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode>();
			if (Alias != null) {
				list.Add(Alias);
			}
			list.Add(NameSpace);
			return list;
		}
	}
}