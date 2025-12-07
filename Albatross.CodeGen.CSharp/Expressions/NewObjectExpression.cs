using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NewObjectExpression : SyntaxNode, IExpression {
		public required ITypeExpression Type { get; init; }
		public ListOfArguments Arguments { get; init; } = new();

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Defined.Keywords.New).Code(Type).Code(Arguments);

		public override IEnumerable<ISyntaxNode> Children => [Type, Arguments];
	}
}