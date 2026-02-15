using Albatross.CodeGen;
using Albatross.Testing;
using System.IO;

namespace Albatross.CodeGen.CSharp.UnitTest;

internal static class TestHelpers {
	public static string Render(this ICodeElement element) {
		using var writer = new StringWriter();
		element.Generate(writer);
		return writer.ToString().NormalizeLineEnding()!;
	}
}
