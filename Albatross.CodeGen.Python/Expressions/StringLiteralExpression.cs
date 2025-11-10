using Albatross.Text;
using System.IO;
using System.Text.Json;

namespace Albatross.CodeGen.Python.Expressions {
	public record class StringLiteralExpression : LiteralExpression {
		public StringLiteralExpression(string value, bool useSingleQuote = false) {
			this.Value = value;
			UseSingleQuote = useSingleQuote;
		}
		public string Value { get; }
		public bool UseSingleQuote { get; }

		public override TextWriter Generate(TextWriter writer) {
			if (UseSingleQuote) {
				writer.AppendChar('\'');
			} else {
				writer.AppendChar('"');
			}
			foreach (char c in Value) {
				switch (c) {
					case '"':
						if (!UseSingleQuote) {
							writer.Append("\\\"");
						}
						break;
					case '\'':
						if (UseSingleQuote) {
							writer.Append("\\'");
						}
						break;
					case '\\':
						writer.Append("\\");
						break;
					case '\n':
						writer.Append("\\n");
						break;
					case '\t':
						writer.Append("\\t");
						break;
					case '\r':
						writer.Append("\\r");
						break;
					case '\b':
						writer.Append("\\b");
						break;
					case '\f':
						writer.Append("\\f");
						break;
					default:
						writer.Append(c);
						break;
				}
			}
			if (UseSingleQuote) {
				writer.AppendChar('\'');
			} else {
				writer.AppendChar('"');
			}
			return writer;
		}
	}
}