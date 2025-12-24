using Albatross.Collections;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ReturnExpression : CodeNode, IExpression {
		public override IEnumerable<ICodeNode> Children => new List<ICodeNode>().AddIfNotNull(this.Expression);
		public IExpression? Expression { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Defined.Keywords.Return);
			if (this.Expression != null) {
				writer.Code(this.Expression);
			}
			return writer;
		}
	}
}