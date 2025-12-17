using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen {
	public record class NewLineExpression : CodeNode, IExpression {
		public override TextWriter Generate(TextWriter writer)
			=> writer.AppendLine();
	}
}