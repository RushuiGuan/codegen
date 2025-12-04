using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TypeExpression : ISyntaxNode, ITypeExpression {
		public TypeExpression(string name, params string[] genericArguments) {
			this.Identifier = new IdentifierNameExpression(name);
			this.GenericArguments = genericArguments.Select(arg => new TypeExpression(arg));
		}
		public bool Nullable { get; init; }
		public bool IsGeneric => GenericArguments.Any();
		public IEnumerable<ITypeExpression> GenericArguments { get; init; } = [];
		public IIdentifierNameExpression Identifier { get; init; }

		public TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			writer.WriteItems(GenericArguments, ",", (w, args) => w.Code(args), "<", ">");
			writer.AppendIf(Nullable, "?");
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			var list = new List<ISyntaxNode> { Identifier };
			list.AddRange(GenericArguments);
			return list;
		}
	}
}
