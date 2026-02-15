using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Python;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.Python.UnitTest;

public class TestDtoAndTypeMappingConverters {
	[Fact]
	public async Task ConvertDtoClass_ShouldApplyClassAndPropertyMappings_AndDefaults() {
		const string code = """
namespace Demo.Models;
public class DemoDto {
	public string? Name { get; set; }
	public bool IsActive { get; set; }
	public System.Collections.Generic.List<int> Values { get; set; } = new();
}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("Demo.Models.DemoDto");
		var index = new JsonDerivedTypeIndex(new CompilationFactory(compilation));
		var dto = new DtoClassInfo(compilation, symbol, index) {
			TypeDiscriminator = "demo"
		};
		var settings = new PythonWebClientSettings {
			DtoClassNameMapping = new Dictionary<string, string> {
				[dto.FullName] = "RenamedDto"
			},
			PropertyNameMapping = new Dictionary<string, string> {
				[$"{dto.FullName}.IsActive"] = "is_enabled"
			}
		};
		var typeConverter = TestHelpers.BuildTypeConverter(compilation, settings);
		var propertyConverter = new ConvertDtoClassPropertyModelToFieldDeclaration(
			new CompilationFactory(compilation),
			new StaticSettingsFactory(settings),
			typeConverter);
		var converter = new ConvertDtoClassModelToDataClass(new StaticSettingsFactory(settings), propertyConverter);

		var text = converter.Convert(dto).Render();

		text.Should().Contain("class RenamedDto(BaseModel):");
		text.Should().Contain("discriminator_: str = Field(alias = \"$type\", default = \"demo\")");
		text.Should().Contain("name: str | None = Field(alias = \"name\", default = None)");
		text.Should().Contain("is_enabled: bool = Field(alias = \"isActive\", default = False)");
		text.Should().Contain("values: list[int] = Field(alias = \"values\", default_factory = list)");
	}

	[Fact]
	public async Task MappedTypeConverter_ShouldUseSettingsTypeMapping() {
		const string code = """
public class DemoModel {
	public System.Uri? Resource { get; set; }
}
""";
		var compilation = await code.CreateNet8CompilationAsync();
		var symbol = compilation.GetRequiredSymbol("DemoModel");
		var property = symbol.GetMembers().OfType<IPropertySymbol>().Single(x => x.Name == "Resource");
		var settings = new PythonWebClientSettings {
			TypeMapping = new Dictionary<string, string> {
				["System.Uri"] = "AnyUrl,pydantic"
			}
		};
		var converter = new MappedTypeConverter(new StaticSettingsFactory(settings));

		var result = converter.TryConvert(property.Type.WithNullableAnnotation(NullableAnnotation.None),
			new NullTypeConverter(), out var expression);

		result.Should().BeTrue();
		expression.Should().NotBeNull();
		expression!.Render().Should().Be("AnyUrl");
		expression.Should().BeOfType<SimpleTypeExpression>();
	}

	private sealed class NullTypeConverter : IConvertObject<ITypeSymbol, ITypeExpression> {
		public ITypeExpression Convert(ITypeSymbol from) => throw new System.NotSupportedException();
		object IConvertObject<ITypeSymbol>.Convert(ITypeSymbol from) => Convert(from);
	}
}
