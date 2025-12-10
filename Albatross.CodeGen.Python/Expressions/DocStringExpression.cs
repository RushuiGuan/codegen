using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Expressions {
	public record class DocStringExpression : SyntaxNode, IExpression {
		public DocStringExpression(string value) {
			this.Value = value;
		}
		public string Value { get; }

		public override IEnumerable<ISyntaxNode> Children => [];

		public override TextWriter Generate(TextWriter writer) {
			var lines = Value.Split('\n').Select(x => x.TrimEnd()).ToArray();
			if (lines.Length == 1) {
				// Single line docstring
				writer.Append("\"\"\" ").Append(lines[0]).Append(" \"\"\"");
			} else if (lines.Length > 1) {
				writer.AppendLine("\"\"\"");
				foreach (var line in lines) {
					writer.AppendLine(line);
				}
				writer.Append("\"\"\"");
			}
			return writer;
		}
	}
}