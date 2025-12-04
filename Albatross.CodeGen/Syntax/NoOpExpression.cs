using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public record NoOpExpression : IExpression {
		public TextWriter Generate(TextWriter writer) => writer;
		public IEnumerable<ISyntaxNode> GetDescendants() => [];
	}
}