using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class DecoratorExpression : InvocationExpression {
		public override TextWriter Generate(TextWriter writer) {
			writer.Append('@');
			return base.Generate(writer);
		}
	}
}