using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.Settings {
	public record class PythonWebClientSettings {
		/// <summary>
		/// mapping bewteen c# namespace and python module
		/// </summary>
		public Dictionary<string, string> NameSpaceModuleMapping { get; init; } = new Dictionary<string, string>();
		public Dictionary<string, string> TypeMapping { get; init; } = new Dictionary<string, string>();
	}
}