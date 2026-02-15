using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.TypeScript.UnitTest;

public class TestDefaultTypeScriptSourceLookup {
	[Fact]
	public async Task TryGet_ShouldMapNamespacePrefix() {
		const string code = """
namespace Test.Dto.Models;
public class MyType {}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Test.Dto.Models.MyType");
		var lookup = new DefaultTypeScriptSourceLookup(new Dictionary<string, string> {
			["Test.Dto"] = "./dto.generated"
		});

		var found = lookup.TryGet(symbol, out var source);

		found.Should().BeTrue();
		source.Should().NotBeNull();
		source!.Source.Should().Be("./dto.generated");
	}
}
