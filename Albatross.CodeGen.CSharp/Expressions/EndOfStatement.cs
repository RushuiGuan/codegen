using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class EndOfStatement : ICodeElement {
		public TextWriter Generate(TextWriter writer) => writer.Append(";");
	}
}