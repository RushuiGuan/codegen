using Albatross.CodeGen;
using Albatross.Testing;
using System.IO;

namespace Albatross.CodeGen.Python.UnitTest;

internal static class TestHelpers {
	public static string Render(this ICodeElement codeElement) {
		using var writer = new StringWriter();
		codeElement.Generate(writer);
		return writer.ToString().NormalizeLineEnding()!;
	}
}
