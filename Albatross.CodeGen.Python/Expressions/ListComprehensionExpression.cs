using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ListComprehensionExpression : SyntaxNode, IExpression {
		public required IExpression IterableExpression { get; init; }
		public required IExpression Expression { get; init; }
		public required string VariableName { get; init; }
		public bool LineBreak { get; set; }


		public override TextWriter Generate(TextWriter writer) {
			if (LineBreak) {
				using var scope= writer.BeginPythonLineBreak("[", "]");
				scope.Writer.Code(Expression)
					.AppendLine()
					.Append("for ").Append(VariableName).Append(" in ").Code(IterableExpression);
			} else {
				writer.Append("[").Append(" for ").Append(VariableName).Append(" in ").Code(IterableExpression).Append("]");
			}
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [
			IterableExpression, Expression,
		];
	}
}