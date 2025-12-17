using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class InvocationExpression : CodeNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IExpression CallableExpression { get; init; }
		public ListOfArguments Arguments { get; init; } = new();

		public override TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) { writer.Code(Defined.Keywords.Await); }
			writer.Code(CallableExpression);
			writer.Code(Arguments);
			return writer;
		}

		public override IEnumerable<ICodeNode> Children {
			get {
				return new List<ICodeNode>{
					CallableExpression, Arguments
				};
			}
		}
	}
}