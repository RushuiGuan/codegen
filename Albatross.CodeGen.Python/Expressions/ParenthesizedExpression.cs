using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ParenthesizedExpression : CodeNode, IExpression {
		public required IExpression Expression { get; init; }
		public override IEnumerable<ICodeNode> Children => [Expression];
		public override TextWriter Generate(TextWriter writer)
			=> writer.Append('(').Code(Expression).Append(')');
	}
}