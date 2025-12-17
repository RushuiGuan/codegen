using Albatross.Text;
using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// interface for any modifier (public, private, static, readonly etc.)
	/// </summary>
	public interface IKeyword : ICodeElement {
		public string Name { get; }
	}

	public record class Keyword : IKeyword {
		public Keyword(string name) {
			Name = name;
		}

		public string Name { get; }
		public bool PadRight { get; init; } = true;
		public TextWriter Generate(TextWriter writer) => writer.Append(Name).Space(PadRight ? 1 : 0);
	}
}