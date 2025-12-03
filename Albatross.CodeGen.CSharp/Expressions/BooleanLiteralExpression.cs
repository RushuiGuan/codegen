using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class BooleanLiteralExpression : LiteralExpression {
		public BooleanLiteralExpression(bool value) {
			this.Value = value;
		}
		public bool Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(Value.ToString(null).ToLowerInvariant());
		}
	}
}