using Albatross.CodeGen.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class StatementTerminator : IExpression{
		public TextWriter Generate(TextWriter writer) {
			writer.Write(";");
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [];
	}
}