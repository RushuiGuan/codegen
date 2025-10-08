using System.IO;

namespace Albatross.CodeGen {
	/// <summary>
	/// the most basic building block of code generation
	/// </summary>
	public interface ICodeElement {
		TextWriter Generate(TextWriter writer);
	}
}