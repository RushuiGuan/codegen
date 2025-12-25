using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class StringInterpolationExpression : CodeNode, IExpression {
		public ListOfNodes<IExpression> Items { get; } = new ListOfNodes<IExpression>();

		public override TextWriter Generate(TextWriter writer) {
			if (this.Items.Any(x => !(x is StringLiteralExpression))) {
				writer.Append("$\"");
			} else {
				writer.Append("\"");
			}
			foreach (var item in Items) {
				if (item is StringLiteralExpression literal) {
					literal.WriteEscapedValue(writer);
				} else {
					writer.Append("{");
					writer.Code(item);
					writer.Append("}");
				}
			}
			writer.Append("\"");
			return writer;
		}
		public override IEnumerable<ICodeNode> Children => Items;
	}
}