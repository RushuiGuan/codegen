using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestDefaultPythonSourceLookup {
	[Fact]
	public async Task TryGet_ShouldMatchByNamespacePrefix() {
		const string code = """
namespace Test.Dto.Models;
public class MyDto {}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Test.Dto.Models.MyDto");
		var lookup = new DefaultPythonSourceLookup(new Dictionary<string, string> {
			["Test.Dto"] = "dto"
		});

		var found = lookup.TryGet(symbol, out var source);

		found.Should().BeTrue();
		source.Should().NotBeNull();
		source!.Source.Should().Be("dto");
	}

	[Fact]
	public async Task TryGet_ShouldReturnFalse_WhenNoMappingExists() {
		const string code = """
namespace Test.Other.Models;
public class MyDto {}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Test.Other.Models.MyDto");
		var lookup = new DefaultPythonSourceLookup(new Dictionary<string, string> {
			["Test.Dto"] = "dto"
		});

		var found = lookup.TryGet(symbol, out var source);

		found.Should().BeFalse();
		source.Should().BeNull();
	}
}
