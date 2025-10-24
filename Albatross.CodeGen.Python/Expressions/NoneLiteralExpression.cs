using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class NoneLiteralExpression : LiteralExpression {
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append("None");
		}
	}
}