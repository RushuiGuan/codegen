using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Python;
using Albatross.Testing;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.Python.UnitTest;

public class TestConvertEnumModelToPythonEnum {
	[Fact]
	public async Task Convert_ShouldUseStringValues_WhenJsonStringEnumConverterIsApplied() {
		const string code = """
[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
public enum Status {
	New = 1,
	Done = 2
}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Status");
		var model = new EnumInfo(compilation, symbol);
		var converter = new ConvertEnumModelToPythonEnum();

		using var writer = new StringWriter();
		converter.Convert(model).Generate(writer);
		var text = writer.ToString().NormalizeLineEnding()!;

		text.Should().Contain("class Status(Enum):");
		text.Should().Contain("NEW = \"New\"");
		text.Should().Contain("DONE = \"Done\"");
	}

	[Fact]
	public async Task Convert_ShouldUseNumericValues_WhenNoJsonStringConverter() {
		const string code = """
public enum Status {
	New = 1,
	Done = 2
}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Status");
		var model = new EnumInfo(compilation, symbol);
		var converter = new ConvertEnumModelToPythonEnum();

		using var writer = new StringWriter();
		converter.Convert(model).Generate(writer);
		var text = writer.ToString().NormalizeLineEnding()!;

		text.Should().Contain("NEW = 1");
		text.Should().Contain("DONE = 2");
	}
}
