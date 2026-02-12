using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface INullableParamTestClient {
		Task<string> NullableStringParam(string? text, CancellationToken cancellationToken);
		Task<string> NullableValueType(System.Nullable<int> id, CancellationToken cancellationToken);
		Task<string> NullableDateOnly(System.Nullable<System.DateOnly> date, CancellationToken cancellationToken);
		Task NullablePostParam(Test.Dto.Classes.MyDto? dto, CancellationToken cancellationToken);
		Task<string> NullableStringArray(string?[] values, CancellationToken cancellationToken);
		Task<string> NullableStringCollection(System.Collections.Generic.IEnumerable<string?> values, CancellationToken cancellationToken);
		Task<string> NullableValueTypeArray(System.Nullable<int>[] values, CancellationToken cancellationToken);
		Task<string> NullableValueTypeCollection(System.Collections.Generic.IEnumerable<System.Nullable<int>> values, CancellationToken cancellationToken);
		Task<string> NullableDateOnlyCollection(System.Collections.Generic.IEnumerable<System.Nullable<System.DateOnly>> dates, CancellationToken cancellationToken);
		Task<string> NullableDateOnlyArray(System.Nullable<System.DateOnly>[] dates, CancellationToken cancellationToken);
		Task<System.Nullable<Test.Dto.Enums.MyEnum>> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value, CancellationToken cancellationToken);
		Task<System.Nullable<Test.Dto.Enums.MyEnum>[]> NullableEnumArray(System.Nullable<Test.Dto.Enums.MyEnum>[] value, CancellationToken cancellationToken);
	}
	public partial class NullableParamTestClient : INullableParamTestClient {
		public NullableParamTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/nullable-param-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<string> NullableStringParam(string? text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-string-param");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableValueType(System.Nullable<int> id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-value-type");
			builder.AddQueryStringIfSet("id", id);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableDateOnly(System.Nullable<System.DateOnly> date, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-date-only");
			builder.AddQueryStringIfSet("date", date);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullablePostParam(Test.Dto.Classes.MyDto? dto, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-post-param");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?>(dto);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableStringArray(string?[] values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-string-array");
			foreach (var item in values) {
				builder.AddQueryStringIfSet("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableStringCollection(System.Collections.Generic.IEnumerable<string?> values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-string-collection");
			foreach (var item in values) {
				builder.AddQueryStringIfSet("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableValueTypeArray(System.Nullable<int>[] values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-value-type-array");
			foreach (var item in values) {
				builder.AddQueryStringIfSet("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableValueTypeCollection(System.Collections.Generic.IEnumerable<System.Nullable<int>> values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-value-type-collection");
			foreach (var item in values) {
				builder.AddQueryStringIfSet("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableDateOnlyCollection(System.Collections.Generic.IEnumerable<System.Nullable<System.DateOnly>> dates, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-date-only-collection");
			foreach (var item in dates) {
				builder.AddQueryStringIfSet("dates", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> NullableDateOnlyArray(System.Nullable<System.DateOnly>[] dates, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-date-only-array");
			foreach (var item in dates) {
				builder.AddQueryStringIfSet("dates", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<Test.Dto.Enums.MyEnum>> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-enum-parameter");
			builder.AddQueryStringIfSet("value", value);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<Test.Dto.Enums.MyEnum>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<Test.Dto.Enums.MyEnum>[]> NullableEnumArray(System.Nullable<Test.Dto.Enums.MyEnum>[] value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-enum-array");
			foreach (var item in value) {
				builder.AddQueryStringIfSet("value", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<System.Nullable<Test.Dto.Enums.MyEnum>[]>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}