using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TypeExpression : SyntaxNode, ITypeExpression {
		public TypeExpression(string name, params string[] genericArguments) {
			this.Identifier = new IdentifierNameExpression(name);
			this.GenericArguments = genericArguments.Select(arg => new TypeExpression(arg));
		}
		public bool Nullable { get; init; }
		public bool IsGeneric => GenericArguments.Any();
		public IEnumerable<ITypeExpression> GenericArguments { get; init; } = [];
		public IIdentifierNameExpression Identifier { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			writer.WriteItems(GenericArguments, ",", (w, args) => w.Code(args), "<", ">");
			writer.AppendIf(Nullable, "?");
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				var list = new List<ISyntaxNode> { Identifier };
				list.AddRange(GenericArguments);
				return list;
			}
		}
	}
}