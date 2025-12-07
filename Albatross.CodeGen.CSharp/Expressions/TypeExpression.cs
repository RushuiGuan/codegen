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

		public TypeExpression(IIdentifierNameExpression identifier, params IEnumerable<ITypeExpression> genericArguments) {
			this.Identifier = identifier;
			this.GenericArguments = genericArguments;
		}

		public bool Nullable { get; init; }
		public bool IsGeneric => GenericArguments.Any();
		public IEnumerable<ITypeExpression> GenericArguments { get; init; } = [];
		public IIdentifierNameExpression Identifier { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Identifier)
				.WriteItems(GenericArguments, ",", (w, args) => w.Code(args), "<", ">")
				.AppendIf(Nullable, "?");
		}

		public override IEnumerable<ISyntaxNode> Children =>
			new List<ISyntaxNode>(GenericArguments) {
				Identifier
			};
	}
}