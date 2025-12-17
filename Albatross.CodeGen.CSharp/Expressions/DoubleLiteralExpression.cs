using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class DoubleLiteralExpression : LiteralExpression {
		public DoubleLiteralExpression(double value) {
			this.Value = value;
		}
		public double Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(Value).Append("d");
		}
	}
}