using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.Settings {
	public record class CodeGenSettings {
		public TypeScriptWebClientSettings TypeScriptWebClientSettings { get; init; } = new TypeScriptWebClientSettings();
		public PythonWebClientSettings PythonWebClientSettings { get; init; } = new PythonWebClientSettings();
		public CSharpWebClientSettings CSharpWebClientSettings { get; init; } = new CSharpWebClientSettings();

		public SymbolFilterPatterns[] ControllerFilters { get; init; } = [];
		public SymbolFilterPatterns[] ControllerMethodFilters { get; init; } = [];
		public SymbolFilterPatterns[] DtoEnumFilters { get; init; } = [];
	}
}