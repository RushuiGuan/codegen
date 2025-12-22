using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// interface for any modifier (public, private, static, readonly etc.)
	/// </summary>
	public interface IKeyword : ICodeElement {
		public string Name { get; }
	}

	/// <summary>
	/// Concrete implementation of a keyword with optional right padding
	/// </summary>
	public record class Keyword : IKeyword {
		/// <summary>
		/// Initializes a new instance of the Keyword class
		/// </summary>
		/// <param name="name">The keyword text</param>
		public Keyword(string name) {
			Name = name;
		}

		/// <summary>
		/// Gets the keyword text
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Gets or sets whether to add a space after the keyword when generating code
		/// </summary>
		public bool PadRight { get; init; } = true;
		
		/// <summary>
		/// Generates the keyword code with optional right padding
		/// </summary>
		/// <param name="writer">The TextWriter to write the generated code to</param>
		/// <returns>The TextWriter for method chaining</returns>
		public TextWriter Generate(TextWriter writer) => writer.Append(Name).Space(PadRight ? 1 : 0);
	}
}