using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.TypeScript.TypeConversions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Albatross.CodeGen.TypeScript.UnitTest {
	public class TestNullableTypeConverter {
		[Fact]
		public async Task NullableInt_ShouldConvertToUnionWithUndefined() {
			const string code = """
class Example { public int? P1 { get; } }
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var symbol = compilation.GetRequiredSymbol("Example");
			var p1Symbol = symbol.GetMembers().OfType<IPropertySymbol>().First(x => x.Name == "P1");
			var factory = new ConvertType(
				[
					new NumericTypeConverter(),
					new NullableTypeConverter(new CompilationFactory(compilation)),
					new GenericTypeConverter()
				],
				NullLogger<ConvertType>.Instance);

			var result = factory.Convert(p1Symbol.Type).Render();

			result.Should().Be("number|undefined");
		}
	}
}
