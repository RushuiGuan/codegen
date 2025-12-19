using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ThrowExpression : CodeNode, IExpression {
		public required IExpression Expression { get; init; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Defined.Keywords.Throw).Code(Expression);
		}
		public override IEnumerable<ICodeNode> Children => [Expression];
	}
}
