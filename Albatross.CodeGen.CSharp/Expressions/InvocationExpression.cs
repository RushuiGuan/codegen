using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class InvocationExpression : SyntaxNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IExpression CallableExpression { get; init; }
		public ListOfArguments ListOfArguments { get; init; } = new();

		public override TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) { writer.Code(Defined.Keywords.Await); }
			writer.Code(CallableExpression);
			writer.Code(ListOfArguments);
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children {
			get {
				return new List<ISyntaxNode>{
					CallableExpression, ListOfArguments
				};
			}
		}
	}
}