using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.Settings {
	public record class PythonWebClientSettings {
		public SymbolFilterPatterns ControllerFilter { get; init; } = new SymbolFilterPatterns();
		public SymbolFilterPatterns[] ControllerMethodFilters { get; init; } = [];
		public SymbolFilterPatterns DtoFilter { get; init; } = new SymbolFilterPatterns();
		public SymbolFilterPatterns EnumFilter { get; init; } = new SymbolFilterPatterns();

		public string EndPointName { get; init; } = "endpoint_name";
		public string BaseClassName { get; init; } = "WebClient";
		/// <summary>
		/// mapping bewteen c# namespace and typescript module
		/// </summary>
		public Dictionary<string, string> NameSpaceModuleMapping { get; init; } = new Dictionary<string, string>();
		public Dictionary<string, string> TypeMapping { get; init; } = new Dictionary<string, string>();
	}
}