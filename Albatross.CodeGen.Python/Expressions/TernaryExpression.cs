using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class TernaryExpression : CodeNode, IExpression {
		public required IExpression Condition { get; init; }
		public required IExpression TrueExpression { get; init; }
		public required IExpression FalseExpression { get; init; }
		public bool LineBreak { get; set; }
		public override IEnumerable<ICodeNode> Children => [Condition, TrueExpression, FalseExpression];

		public override TextWriter Generate(TextWriter writer) {
			if (LineBreak) {
				writer.Code(TrueExpression)
					.AppendLine()
					.Append("if ").Code(Condition)
					.AppendLine()
					.Append("else ").Code(FalseExpression);
			} else {
				writer.Code(TrueExpression).Append(" if ").Code(Condition).Append(" else ").Code(FalseExpression);
			}
			return writer;
		}
	}
}