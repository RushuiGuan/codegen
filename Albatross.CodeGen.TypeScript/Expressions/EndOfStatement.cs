using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen.TypeScript.Expressions {
	public record class EndOfStatement : ICodeElement {
		public TextWriter Generate(TextWriter writer) => writer.Append(";");
	}
}