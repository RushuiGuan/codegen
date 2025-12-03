using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class GenericTypeExpression : SyntaxNode, ITypeExpression {
		public GenericTypeExpression(string name) {
			this.Identifier = new IdentifierNameExpression(name);
		}
		public GenericTypeExpression(IIdentifierNameExpression identifier) {
			this.Identifier = identifier;
		}
		public IIdentifierNameExpression Identifier { get; }
		public required ListOfSyntaxNodes<ITypeExpression> Arguments { get; init; }
		public bool Nullable { get; init; }
		public override IEnumerable<ISyntaxNode> Children => [Identifier, Arguments];
		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Identifier).Append("<").Code(Arguments).Append(">").AppendIf(Nullable, "?");
		}
	}
}