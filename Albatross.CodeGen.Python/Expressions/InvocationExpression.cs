using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class InvocationExpression : CodeNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IExpression CallableExpression { get; init; }
		public ListOfNodes<IExpression> Arguments { get; init; } = new ListOfNodes<IExpression>();
		public override IEnumerable<ICodeNode> Children => [CallableExpression, Arguments];

		public override TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) {
				writer.Append("await ");
			}
			writer.Code(CallableExpression);
			writer.OpenParenthesis().Code(Arguments).CloseParenthesis();
			return writer;
		}
	}
}