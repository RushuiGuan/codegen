using Albatross.Text;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class StringInterpolationExpression : ListOfNodes<IExpression>, IExpression {
		public override TextWriter Generate(TextWriter writer) {
			if (this.Any(x => !(x is StringLiteralExpression))) {
				writer.Append("$\"");
			} else {
				writer.Append("\"");
			}
			foreach (var item in this) {
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
	}
}