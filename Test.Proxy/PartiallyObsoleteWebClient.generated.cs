using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Proxy {
	public partial class PartiallyObsoleteWebClient {
		public PartiallyObsoleteWebClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/partiallyobsolete";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<string> Get(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/get");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}