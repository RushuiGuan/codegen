using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class FloatLiteralExpression : LiteralExpression {
		public FloatLiteralExpression(float value) {
			this.Value = value;
		}
		public float Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(Value).Append("f");
		}
	}
}