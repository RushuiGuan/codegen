using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.WebClient.Settings;
using System;

namespace Albatross.CodeGen.CommandLine {
	public class CodeGenSettingsFactory : ICodeGenSettingsFactory {
		private readonly CodeGenParams @params;

		public CodeGenSettingsFactory(CodeGenParams @params) {
			this.@params = @params;
		}
		public T Get<T>() where T : CodeGenSettings, new () {
			var settings = @params.CodeGenSettings;
			if (settings == null) {
				return new T();
			} else if (settings is T) {
				return (T)settings;
			} else {
				throw new InvalidOperationException($"Cannot cast CodeGenSettings from {settings.GetType().Name} to {typeof(T).Name}");
			}
		}
	}
}