using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ReturnExpression : SyntaxNode, IExpression {
		public ReturnExpression(IExpression expression) {
			this.Expression = expression;
		}
		public override IEnumerable<ISyntaxNode> Children => [this.Expression];
		public IExpression Expression { get; }

		public override TextWriter Generate(TextWriter writer) {
			if (this.Expression is NoneLiteralExpression) {
				return writer.Append("return");
			} else {
				return writer.Append("return ").Code(this.Expression);
			}
		}
	}
}