using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class StringLiteralExpression : LiteralExpression {
		public StringLiteralExpression(string value) {
			this.Value = value;
		}

		public string Value { get; }
		
		public void WriteEscapedValue(TextWriter writer){
			foreach (char c in Value) {
				switch (c) {
					case '"':
						writer.Append("\\\"");
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
		}

		public override TextWriter Generate(TextWriter writer) {
			writer.AppendChar('"');
			WriteEscapedValue(writer);
			writer.AppendChar('"');
			return writer;
		}
	}
}