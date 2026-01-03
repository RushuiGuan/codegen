using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Albatross.CodeGen.WebClient.Settings {
	public record class CodeGenSettings {
		public SymbolFilterPatterns[] ControllerFilters { get; init; } = [];
		public SymbolFilterPatterns[] ControllerMethodFilters { get; init; } = [];
		public SymbolFilterPatterns[] DtoEnumFilters { get; init; } = [];
	}
}