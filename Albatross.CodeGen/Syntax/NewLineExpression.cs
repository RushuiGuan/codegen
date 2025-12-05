using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public class NewLineExpression : IExpression {
		public TextWriter Generate(TextWriter writer)
			=> writer.AppendLine();

		public IEnumerable<ISyntaxNode> GetDescendants() => Array.Empty<ISyntaxNode>();
	}
}