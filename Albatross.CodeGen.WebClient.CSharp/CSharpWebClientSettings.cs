using Albatross.CodeGen.WebClient.Settings;
using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.CSharp {
	public record class CSharpWebClientSettings : CodeGenSettings {
		public string Namespace { get; init; } = "MyNamespace";

		/// <summary>
		/// If true, the client will use Text/Plain content type for string post.  This requires the server to accept 
		/// text/plain content type for the Body.  If false, the client will use application/json content type for string post.
		/// </summary>
		public bool UseTextContentTypeForStringPostRequest { get; init; } = true;

		public bool UseInterface { get; init; }

		/// <summary>
		/// when true, proxies are created with internal access modifier instead of public.  Useful when paired with UseInterface
		/// to force the use of the interface.
		/// </summary>
		public bool UseInternalProxy { get; init; }

		public Dictionary<string, WebClientConstructorSettings> ConstructorSettings { get; init; } = new Dictionary<string, WebClientConstructorSettings>();
	}
}