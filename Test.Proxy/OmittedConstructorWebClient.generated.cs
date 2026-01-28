using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
#nullable enable
namespace Test.Proxy {
	public partial class OmittedConstructorWebClient {
		public const string ControllerPath = "api/omittedconstructor";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
	}
}