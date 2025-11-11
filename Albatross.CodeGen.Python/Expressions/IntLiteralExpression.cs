using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class IntLiteralExpression : LiteralExpression {
		public IntLiteralExpression(int value) {
			this.Value = value;
		}
		public int Value { get; }
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append(Value);
		}
	}
}