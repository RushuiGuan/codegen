using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Proxy {
	public partial class DuplicateNameTestWebClient {
		public DuplicateNameTestWebClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/duplicate-name-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task Submit(int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/by-id");
			builder.AddQueryString("id", id);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task Submit(string name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/by-name");
			builder.AddQueryStringIfSet("name", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}