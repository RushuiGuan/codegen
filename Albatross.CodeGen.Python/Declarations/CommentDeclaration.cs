using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.Python.Declarations {
	public record class CommentDeclaration : CodeNode, IDeclaration {
		public string Text { get; }

		public CommentDeclaration(string text) {
			this.Text = text;
		}

		public override TextWriter Generate(TextWriter writer) {
			var lines = Text.Split('\n');
			foreach (var line in lines) {
				writer.Append("# ").AppendLine(line);
			}
			return writer;
		}
	}
}