using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ListComprehensionExpression :  SyntaxNode, IExpression {
		public required IExpression IterableExpression { get; init; }
		public required IExpression Expression { get; init; }
		public required string VariableName { get; init; }


		public override TextWriter Generate(TextWriter writer) {
			writer.Append("[").Code(Expression).Append(" for ")
				.Append(VariableName).Append(" in ").Code(IterableExpression).Append("]");
			return writer;
		}

		public override IEnumerable<ISyntaxNode> Children => [
			IterableExpression, Expression, 
		];
	}
}