using Albatross.CodeGen;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Settings;
using System.IO;

namespace Albatross.CodeGen.WebClient.CSharp.UnitTest;

internal static class TestHelpers {
	public static string Render(this ICodeElement element) {
		using var writer = new StringWriter();
		element.Generate(writer);
		return writer.ToString().Replace("\r", "");
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
