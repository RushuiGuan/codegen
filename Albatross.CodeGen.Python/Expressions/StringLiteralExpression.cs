using Albatross.Text;
using System.IO;
using System.Text.Json;

namespace Albatross.CodeGen.Python.Expressions {
	public record class StringLiteralExpression : LiteralExpression {
		public StringLiteralExpression(string value) {
			this.Value = value;
		}
		public string Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(JsonSerializer.Serialize(Value));
		}
	}
}