using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class TypeCheckExpression : CodeNode, IExpression {
		public TypeCheckExpression(bool inversed) {
			Inversed = inversed;
		}
		public required IExpression Expression { get; init; }
		public required ITypeExpression Type { get; init; }

		public override IEnumerable<ICodeNode> Children => [Expression, Type];

		public bool Inversed { get; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Expression).Append(" is ");
			if (Inversed) {
				writer.Append("not ");
			}
			writer.Code(Type);
			return writer;
		}
	}
}