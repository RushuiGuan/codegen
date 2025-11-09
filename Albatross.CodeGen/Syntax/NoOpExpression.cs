using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public record NoOpExpression : SyntaxNode, IExpression {
		public override TextWriter Generate(TextWriter writer) {
			return writer;
		}
		public override IEnumerable<ISyntaxNode> Children { get; } = [];
	}
}