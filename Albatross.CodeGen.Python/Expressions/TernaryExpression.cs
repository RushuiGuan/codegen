using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class TernaryExpression : SyntaxNode, IExpression {
		public required IExpression Condition { get; init; }
		public required IExpression TrueExpression { get; init; }
		public required IExpression FalseExpression { get; init; }

		public override IEnumerable<ISyntaxNode> Children => [Condition, TrueExpression, FalseExpression];

		public override TextWriter Generate(TextWriter writer) {
			return writer
				.Code(TrueExpression)
				.Append(" if ")
				.Code(Condition)
				.Append(" else ")
				.Code(FalseExpression);
		}
	}
}
