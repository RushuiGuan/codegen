using Albatross.CodeGen.Syntax;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Declarations {
	public class CommentDeclaration : IDeclaration {
		public string Text { get; }

		public CommentDeclaration(string text) {
			this.Text = text;
		}

		public TextWriter Generate(TextWriter writer) {
			var lines = Text.Split('\n');
			foreach (var line in lines) {
				writer.Append("# ").AppendLine(line);
			}
			return writer;
		}

		public IEnumerable<ISyntaxNode> GetDescendants() => [];
	}
}