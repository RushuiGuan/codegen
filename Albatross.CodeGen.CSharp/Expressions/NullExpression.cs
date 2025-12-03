using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NullExpression : LiteralExpression {
		public override TextWriter Generate(TextWriter writer) {
			return writer.Append("null");
		}
	}
}