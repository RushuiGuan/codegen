using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Test.WithInterface.Client {
	public partial class OmittedConstructorClient {
		public OmittedConstructorClient(ILogger<OmittedConstructorClient> logger, HttpClient client) {
			this.client = client;
			jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions();
		}
	}
}