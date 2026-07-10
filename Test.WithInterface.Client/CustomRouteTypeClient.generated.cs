using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface ICustomRouteTypeClient {
		Task<string> Get(string name, CancellationToken cancellationToken);
	}
	public partial class CustomRouteTypeClient : ICustomRouteTypeClient {
		public CustomRouteTypeClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/customroutetype";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<string> Get(string name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/{name}");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
	}
}