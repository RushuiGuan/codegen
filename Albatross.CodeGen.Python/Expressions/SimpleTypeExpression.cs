using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class SimpleTypeExpression : CodeNode, ITypeExpression {
		public required IIdentifierNameExpression Identifier { get; init; }
		public override IEnumerable<ICodeNode> Children => [Identifier];
		public override TextWriter Generate(TextWriter writer) => writer.Code(Identifier);
	}
}