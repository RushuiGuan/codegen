using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface IFromRoutingParamTestClient {
		Task ImplicitRoute(string name, int id, CancellationToken cancellationToken);
		Task ExplicitRoute(string name, int id, CancellationToken cancellationToken);
		Task WildCardRouteDouble(string name, int id, CancellationToken cancellationToken);
		Task WildCardRouteSingle(string name, int id, CancellationToken cancellationToken);
		Task DateTimeRoute(System.DateTime date, int id, CancellationToken cancellationToken);
		Task DateOnlyRoute(System.DateOnly date, int id, CancellationToken cancellationToken);
		Task DateTimeOffsetRoute(System.DateTimeOffset date, int id, CancellationToken cancellationToken);
		Task TimeOnlyRoute(System.TimeOnly time, int id, CancellationToken cancellationToken);
		Task EnumRoute(Test.Dto.Enums.MyEnum value, int id, CancellationToken cancellationToken);
	}
	public partial class FromRoutingParamTestClient : IFromRoutingParamTestClient {
		public FromRoutingParamTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/from-routing-param-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task ImplicitRoute(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/implicit-route/{name}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task ExplicitRoute(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/explicit-route/{name}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task WildCardRouteDouble(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/wild-card-route-double/{id}/{name}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task WildCardRouteSingle(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/wild-card-route-single/{id}/{name}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task DateTimeRoute(System.DateTime date, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/date-time-route/{date.ISO8601()}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task DateOnlyRoute(System.DateOnly date, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/date-only-route/{date.ISO8601()}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task DateTimeOffsetRoute(System.DateTimeOffset date, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/datetimeoffset-route/{date.ISO8601()}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task TimeOnlyRoute(System.TimeOnly time, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/timeonly-route/{time.ISO8601()}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task EnumRoute(Test.Dto.Enums.MyEnum value, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/enum-route/{value}/{id}");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}