using Albatross.Text;
using System;
using System.IO;
using System.Text;

namespace Albatross.CodeGen {
	/// <summary>
	/// Provides a disposable scope for generating indented code blocks with automatic brace handling
	/// </summary>
	public class CodeGeneratorScope : IDisposable {
		TextWriter parentWriter;
		StringBuilder content = new StringBuilder();
		Action<TextWriter> end;
		
		/// <summary>
		/// Gets the TextWriter instance used for writing code within this scope
		/// </summary>
		public TextWriter Writer { get; private set; }

		/// <summary>
		/// Initializes a new instance of the CodeGeneratorScope class
		/// </summary>
		/// <param name="writer">The parent TextWriter to write the final output to</param>
		/// <param name="begin">Action to execute at the beginning of the scope (e.g., opening brace)</param>
		/// <param name="end">Action to execute at the end of the scope (e.g., closing brace)</param>
		public CodeGeneratorScope(TextWriter writer, Action<TextWriter> begin, Action<TextWriter> end) {
			parentWriter = writer;
			Writer = new StringWriter(content);
			begin(writer);
			this.end = end;
		}

		/// <summary>
		/// Disposes the scope, writing all accumulated content with proper indentation and executing the end action
		/// </summary>
		public void Dispose() {
			Writer.Flush();
			var reader = new StringReader(content.ToString());
			string? line = reader.ReadLine();
			while (line != null) {
				parentWriter.Tab().WriteLine(line);
				line = reader.ReadLine();
			}
			end(parentWriter);
		}
	}
}