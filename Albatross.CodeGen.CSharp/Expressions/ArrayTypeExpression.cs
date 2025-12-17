using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class ArrayTypeExpression : CodeNode, ITypeExpression {
		public required ITypeExpression Type { get; init; }
		public override IEnumerable<ICodeNode> Children => [Type];
		public bool Nullable { get; init; }

		public override TextWriter Generate(TextWriter writer) {
			return writer.Code(Type).Append("[]").AppendIf(Nullable, "?");
		}
	}
}