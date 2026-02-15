using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.Python.TypeConversions;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.Python.UnitTest;

public class TestTypeConverters {
	[Theory]
	[InlineData(@"class Example { public string P { get; } }", "str")]
	[InlineData(@"class Example { public string? P { get; } }", "str | None")]
	[InlineData(@"class Example { public int? P { get; } }", "int | None")]
	[InlineData(@"class Example { public byte[] P { get; } }", "bytes")]
	[InlineData(@"class Example { public System.Collections.Generic.List<int> P { get; } }", "list[int]")]
	[InlineData(@"class Example { public System.Threading.Tasks.Task<string> P { get; } }", "str")]
	public async Task Convert_ShouldMapCommonTypes(string code, string expected) {
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Example");
		var property = symbol.GetMembers().OfType<IPropertySymbol>().Single(x => x.Name == "P");
		var converter = BuildConverter(compilation);

		var actual = converter.Convert(property.Type).Render();

		actual.Should().Be(expected);
	}

	[Fact]
	public async Task Convert_ShouldFallbackToCustomTypeName_WhenNoBuiltInMatch() {
		const string code = """
namespace Demo;
public class Child {}
public class Example { public Child P { get; } }
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Demo.Example");
		var property = symbol.GetMembers().OfType<IPropertySymbol>().Single(x => x.Name == "P");
		var converter = BuildConverter(compilation);

		var actual = converter.Convert(property.Type).Render();

		actual.Should().Be("Child");
	}

	static ConvertType BuildConverter(Compilation compilation) {
		var factory = new CompilationFactory(compilation);
		var sourceLookup = new DefaultPythonSourceLookup(new Dictionary<string, string>());
		var converters = new ITypeConverter[] {
			new VoidTypeConverter(),
			new BooleanTypeConverter(),
			new DateConverter(),
			new DateTimeConverter(),
			new DecimalTypeConverter(),
			new GuidTypeConverter(),
			new IntTypeConverter(),
			new JsonElementConverter(),
			new NumericTypeConverter(),
			new ObjectTypeConverter(),
			new StringTypeConverter(),
			new TimeConverter(),
			new TimeSpanConverter(),
			new ActionResultConverter(factory),
			new ArrayTypeConverter(factory),
			new NullableTypeConverter(factory),
			new AsyncTypeConverter(factory),
			new GenericTypeConverter(),
			new CustomTypeConverter(sourceLookup, NullLogger<CustomTypeConverter>.Instance)
		};
		return new ConvertType(converters, NullLogger<ConvertType>.Instance);
	}
}
