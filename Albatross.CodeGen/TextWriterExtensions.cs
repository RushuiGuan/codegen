using Albatross.Text;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen {
	/// <summary>
	/// Provides extension methods for TextWriter to integrate with code generation elements
	/// </summary>
	public static class TextWriterExtensions {
		/// <summary>
		/// Generates code from a code element and writes it to the TextWriter
		/// </summary>
		/// <param name="writer">The TextWriter to write code to</param>
		/// <param name="codeElement">The code element to generate code from</param>
		/// <returns>The TextWriter for method chaining</returns>
		public static TextWriter Code(this TextWriter writer, ICodeElement codeElement) {
			codeElement.Generate(writer);
			return writer;
		}
		
		/// <summary>
		/// Generates code from a code element if it is not null and writes it to the TextWriter
		/// </summary>
		/// <param name="writer">The TextWriter to write code to</param>
		/// <param name="codeElement">The optional code element to generate code from</param>
		/// <returns>The TextWriter for method chaining</returns>
		public static TextWriter CodeIfNotNull(this TextWriter writer, ICodeElement? codeElement) {
			codeElement?.Generate(writer);
			return writer;
		}
	}
}