using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface IOmittedConstructorClient {
	}
	public partial class OmittedConstructorClient : IOmittedConstructorClient {
		public const string ControllerPath = "api/omittedconstructor";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
	}
}