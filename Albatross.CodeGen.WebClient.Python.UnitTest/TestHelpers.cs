using Albatross.CodeGen;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.TypeConversions;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.IO;

namespace Albatross.CodeGen.WebClient.Python.UnitTest;

internal static class TestHelpers {
	public static string Render(this ICodeElement element) {
		using var writer = new StringWriter();
		element.Generate(writer);
		return writer.ToString().Replace("\r", "");
	}

	public static ConvertType BuildTypeConverter(Compilation compilation, PythonWebClientSettings settings) {
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

internal sealed class StaticSettingsFactory : ICodeGenSettingsFactory {
	private readonly CodeGenSettings settings;

	public StaticSettingsFactory(CodeGenSettings settings) {
		this.settings = settings;
	}

	public T Get<T>() where T : CodeGenSettings, new() {
		if (settings is T typed) {
			return typed;
		}
		throw new InvalidDataException($"Cannot cast settings from {settings.GetType().Name} to {typeof(T).Name}");
	}
}
