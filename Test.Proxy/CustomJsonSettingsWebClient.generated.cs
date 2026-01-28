using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
#nullable enable
namespace Test.Proxy {
	public partial class CustomJsonSettingsWebClient {
		public CustomJsonSettingsWebClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = MyCustomJsonSettings.Options;
		}
		public const string ControllerPath = "api/customjsonsettings";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
	}
}