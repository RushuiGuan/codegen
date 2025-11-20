using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.Settings {
	public record class PythonWebClientSettings {
		/// <summary>
		/// if true, use httpx, otherwise use requests
		/// </summary>
		public bool Async { get; init; }
		/// <summary>
		/// mapping bewteen c# namespace and python module
		/// </summary>
		public Dictionary<string, string> NameSpaceModuleMapping { get; init; } = new Dictionary<string, string>();
		public Dictionary<string, string> TypeMapping { get; init; } = new Dictionary<string, string>();
		public Dictionary<string, string> PropertyNameMapping { get; init; } = new Dictionary<string, string>();
		public Dictionary<string, WebClientConstructorSettings> ConstructorSettings { get; init; } = new Dictionary<string, WebClientConstructorSettings>();
	}
}