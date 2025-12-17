using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class ParameterDeclaration : CodeNode, IDeclaration {
		public ITypeExpression Type { get; init; } = Defined.Types.None;
		public required IIdentifierNameExpression Identifier { get; init; }
		public LiteralExpression? DefaultValue { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			writer.Code(Identifier);
			if (!Type.Equals(Defined.Types.None)) {
				writer.Append(": ").Code(Type);
			}
			if (DefaultValue != null) {
				writer.Append(" = ").Code(DefaultValue);
			}
			return writer;
		}
		public override IEnumerable<ICodeNode> Children => [Type, Identifier];
	}
}