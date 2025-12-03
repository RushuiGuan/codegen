using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class AttributeExpression : InvocationExpression {
		public override TextWriter Generate(TextWriter writer) {
			writer.Append("[");
			base.Generate(writer);
			writer.Append("]");
			return writer;
		}
	}
}