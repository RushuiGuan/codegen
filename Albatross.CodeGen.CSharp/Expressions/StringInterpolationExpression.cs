using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class StringInterpolationExpression : CodeNode, IExpression {
		public required IEnumerable<IExpression> Expressions { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			if (Expressions.Any(x => !(x is StringLiteralExpression))) {
				writer.Append("$\"");
			} else {
				writer.Append("\"");
			}
			foreach (var item in Expressions) {
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

		public override IEnumerable<ICodeNode> Children => Expressions;
	}
}