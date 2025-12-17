using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.Syntax {
	public record class NewLineExpression : CodeNode, IExpression {
		public override TextWriter Generate(TextWriter writer)
			=> writer.AppendLine();
	}
}