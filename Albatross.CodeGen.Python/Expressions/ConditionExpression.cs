using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ConditionExpression : CodeNode, IExpression {
		private readonly string @operator;

		public ConditionExpression(string @operator) {
			this.@operator = @operator;
		}

		public required IExpression Left { get; init; }
		public required IExpression Right { get; init; }

		public override IEnumerable<ICodeNode> Children => [Left, Right];

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Left).Append($" {@operator} ").Code(Right);
	}
}