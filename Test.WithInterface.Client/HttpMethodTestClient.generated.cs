using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface IHttpMethodTestClient {
		Task Delete(CancellationToken cancellationToken);
		Task Post(CancellationToken cancellationToken);
		Task Patch(CancellationToken cancellationToken);
		Task<int> Get(CancellationToken cancellationToken);
		Task Put(CancellationToken cancellationToken);
	}
	public partial class HttpMethodTestClient : IHttpMethodTestClient {
		public HttpMethodTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/http-method-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task Delete(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Delete)
				.WithRelativeUrl($"{ControllerPath}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task Post(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task Patch(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Patch)
				.WithRelativeUrl($"{ControllerPath}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> Get(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task Put(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Put)
				.WithRelativeUrl($"{ControllerPath}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}