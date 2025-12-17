using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class AssignmentExpression : CodeNode, IExpression {
		public required IExpression Left { get; init; }
		public required IExpression Expression { get; init; }

		public override TextWriter Generate(TextWriter writer)
			=> writer.Code(Left).Code(Defined.Operators.Assignment).Code(Expression);

		public override IEnumerable<ICodeNode> Children
			=> [Left, Expression];
	}
}