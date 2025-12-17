using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class SimpleTypeExpression : CodeNode, ITypeExpression {
		public required IIdentifierNameExpression Identifier { get; init; }
		public override IEnumerable<ICodeNode> Children => [Identifier];
		public bool Optional { get; init; }

		public override TextWriter Generate(TextWriter writer) => writer.Code(Identifier);
	}
}