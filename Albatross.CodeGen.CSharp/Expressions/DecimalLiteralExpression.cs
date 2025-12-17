using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class DecimalLiteralExpression : LiteralExpression {
		public DecimalLiteralExpression(decimal value) {
			this.Value = value;
		}
		public decimal Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(Value).Append("m");
		}
	}
}