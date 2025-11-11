using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.Settings {
	public record class CSharpWebClientSettings {
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

		public WebClientMethodSettings? GlobalMethodSettings { get; set; }
		public Dictionary<string, WebClientMethodSettings> MethodSettings { get; init; } = new Dictionary<string, WebClientMethodSettings>();
		public Dictionary<string, WebClientConstructorSettings> ConstructorSettings { get; init; } = new Dictionary<string, WebClientConstructorSettings>();

		public WebClientMethodSettings Get(string controllerName, string methodName) {
			var key = $"{controllerName}.{methodName}";
			var globalSettings = GlobalMethodSettings ?? new WebClientMethodSettings();
			return MethodSettings.GetValueOrDefault(key, globalSettings);
		}
	}
}