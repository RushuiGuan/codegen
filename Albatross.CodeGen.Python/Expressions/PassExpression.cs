using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Python.Expressions {
	public record class PassExpression:CodeNode, IExpression {
		override public TextWriter Generate(TextWriter writer) {
			return writer.Append("pass");
		}
	}
}
