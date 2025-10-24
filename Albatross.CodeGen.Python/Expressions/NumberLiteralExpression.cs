using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class NumberLiteralExpression : LiteralExpression {
		public NumberLiteralExpression(double value) {
			this.Value = value;
		}
		public NumberLiteralExpression(object value) {
			this.Value = System.Convert.ToDouble(value);
		}
		public double Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(Value);
		}
	}
}