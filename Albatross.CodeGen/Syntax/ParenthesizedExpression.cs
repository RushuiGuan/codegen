using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public record class ParenthesizedExpression : CodeNode, IExpression {
		public ParenthesizedExpression(IExpression expression) {
			this.Expression = expression;
		}

		public IExpression Expression { get; }
		public override TextWriter Generate(TextWriter writer)
			=> writer.Append('(').Code(Expression).Append(')');
		public override IEnumerable<ICodeNode> Children => [Expression];
	}
}