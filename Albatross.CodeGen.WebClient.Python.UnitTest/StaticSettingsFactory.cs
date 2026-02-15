using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Settings;
using System.IO;

namespace Albatross.CodeGen.WebClient.Python.UnitTest;

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
