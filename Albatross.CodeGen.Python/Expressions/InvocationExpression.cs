using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class InvocationExpression : SyntaxNode, IExpression {
		public bool UseAwaitOperator { get; init; }
		public required IExpression CallableExpression { get; init; }
		public ListOfSyntaxNodes<IExpression> ArgumentList { get; init; } = new ListOfSyntaxNodes<IExpression>();
		public override IEnumerable<ISyntaxNode> Children => [CallableExpression, ArgumentList];

		public override TextWriter Generate(TextWriter writer) {
			if (UseAwaitOperator) {
				writer.Append("await ");
			}
			writer.Code(CallableExpression);
			writer.OpenParenthesis().Code(ArgumentList).CloseParenthesis();
			return writer;
		}
	}
}