using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class InvocationExpression : IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IExpression CallableExpression { get; init; }
		public ListOfArguments Arguments { get; init; } = new();

		public virtual TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) { writer.Code(Defined.Keywords.Await); }
			writer.Code(CallableExpression);
			writer.Code(Arguments);
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() {
			return new List<ISyntaxNode>{
				CallableExpression, Arguments
			};
		}
	}
}