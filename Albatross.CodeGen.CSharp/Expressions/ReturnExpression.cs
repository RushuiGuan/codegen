using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ReturnExpression : SyntaxNode, IExpression {
		public ReturnExpression(IExpression expression) {
			this.Expression = expression;
		}
		public override IEnumerable<ISyntaxNode> Children => [this.Expression];
		public IExpression Expression { get; }

		public override TextWriter Generate(TextWriter writer) {
			if (this.Expression is NullExpression) {
				return writer.Append("return");
			} else {
				return writer.Append("return ").Code(this.Expression);
			}
		}
	}
}