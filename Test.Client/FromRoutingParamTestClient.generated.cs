using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Client {
	public partial class FromRoutingParamTestClient {
		public FromRoutingParamTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<int> GetTenantId(string tenant, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/tenantid");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task ImplicitRoute(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/implicit-route/{name}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task ExplicitRoute(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/explicit-route/{name}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task WildCardRouteDouble(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/wild-card-route-double/{id}/{name}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task WildCardRouteSingle(string name, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/wild-card-route-single/{id}/{name}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task DateTimeRoute(System.DateTime date, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/date-time-route/{date.ISO8601()}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task DateOnlyRoute(System.DateOnly date, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/date-only-route/{date.ISO8601()}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task DateTimeOffsetRoute(System.DateTimeOffset date, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/datetimeoffset-route/{date.ISO8601()}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task TimeOnlyRoute(System.TimeOnly time, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/timeonly-route/{time.ISO8601()}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task EnumRoute(Test.Dto.Enums.MyEnum value, int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"api/from-routing-param-test/{tenant}/enum-route/{value}/{id}");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
	}
}