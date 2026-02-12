using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Client {
	public partial class ArrayParamTestClient {
		public ArrayParamTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/array-param-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<string> ArrayStringParam(string[] array, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/array-string-param");
			foreach (var item in array) {
				builder.AddQueryStringIfSet("a", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> ArrayValueType(int[] array, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/array-value-type");
			foreach (var item in array) {
				builder.AddQueryString("a", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> CollectionStringParam(System.Collections.Generic.IEnumerable<string> collection, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-string-param");
			foreach (var item in collection) {
				builder.AddQueryStringIfSet("c", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> CollectionValueType(System.Collections.Generic.IEnumerable<int> collection, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-value-type");
			foreach (var item in collection) {
				builder.AddQueryString("c", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> CollectionNullableValueType(System.Collections.Generic.IEnumerable<System.Nullable<int>> collection, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-nullable-value-type");
			foreach (var item in collection) {
				builder.AddQueryStringIfSet("cv", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> CollectionDateParam(System.Collections.Generic.IEnumerable<System.DateOnly> collection, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-date-param");
			foreach (var item in collection) {
				builder.AddQueryString("c", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> CollectionNullableDateParam(System.Collections.Generic.IEnumerable<System.Nullable<System.DateOnly>> collection, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-nullable-date-param");
			foreach (var item in collection) {
				builder.AddQueryStringIfSet("cd", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> CollectionDateTimeParam(System.Collections.Generic.IEnumerable<System.DateTime> collection, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-datetime-param");
			foreach (var item in collection) {
				builder.AddQueryString("c", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}