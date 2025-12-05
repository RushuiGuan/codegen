using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class TerminateExpression : IExpression {
		public TextWriter Generate(TextWriter writer) => writer.Append(";");
		public IEnumerable<ISyntaxNode> GetDescendants() => Array.Empty<ISyntaxNode>();
	}
}