using Albatross.Text;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen {
	public static class TextWriterExtensions {
		public static TextWriter Code(this TextWriter writer, ICodeElement codeElement) {
			codeElement.Generate(writer);
			return writer;
		}
		public static TextWriter CodeIfNotNull(this TextWriter writer, ICodeElement? codeElement) {
			codeElement?.Generate(writer);
			return writer;
		}
	}
}