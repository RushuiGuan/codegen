using Albatross.CodeGen.Syntax;
using Albatross.Collections;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class ScopedVariableExpression : CodeNode, IExpression {
		public required IIdentifierNameExpression Identifier { get; init; }
		public ITypeExpression? Type { get; init; }
		public IExpression? Assignment { get; init; }

		public override IEnumerable<ICodeNode> Children => new List<ICodeNode> { Identifier }.AddIfNotNull(Type, Assignment);

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			if (Type != null && !Type.Equals(Defined.Types.None)) {
				writer.Append(" : ").Code(Type);
			}
			if (Assignment != null) {
				writer.Append(" = ").Code(Assignment);
			}
			return writer;
		}
	}
}