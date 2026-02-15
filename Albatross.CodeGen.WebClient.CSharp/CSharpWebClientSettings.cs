using Albatross.CodeGen.WebClient.Settings;
using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.CSharp {
	public record class CSharpWebClientSettings : CodeGenSettings {
		public string Namespace { get; init; } = "MyNamespace";


		public bool UseInterface { get; init; }

		/// <summary>
		/// when true, proxies are created with internal access modifier instead of public.  Useful when paired with UseInterface
		/// to force the use of the interface.
		/// </summary>
		public bool UseInternalProxy { get; init; }

		public Dictionary<string, WebClientConstructorSettings> ConstructorSettings { get; init; } = new Dictionary<string, WebClientConstructorSettings>();
	}
}