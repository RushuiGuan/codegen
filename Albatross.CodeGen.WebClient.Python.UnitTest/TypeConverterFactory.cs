using Albatross.CodeGen;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.TypeConversions;
using Albatross.CodeGen.WebClient.Python;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging.Abstractions;

namespace Albatross.CodeGen.WebClient.Python.UnitTest;

internal static class TypeConverterFactory {
	public static ConvertType Build(Compilation compilation, PythonWebClientSettings settings) {
		var compilationFactory = new CompilationFactory(compilation);
		var settingsFactory = new StaticSettingsFactory(settings);
		var sourceLookup = new DefaultPythonSourceLookup(settings.NamespaceModuleMapping);
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
			new ActionResultConverter(compilationFactory),
			new ArrayTypeConverter(compilationFactory),
			new NullableTypeConverter(compilationFactory),
			new AsyncTypeConverter(compilationFactory),
			new GenericTypeConverter(),
			new MappedTypeConverter(settingsFactory),
			new CustomTypeConverter(sourceLookup, NullLogger<CustomTypeConverter>.Instance)
		};
		return new ConvertType(converters, NullLogger<ConvertType>.Instance);
	}
}
