using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Proxy {
	public partial class RequiredParamTestWebClient {
		public RequiredParamTestWebClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/required-param-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<string> ExplicitStringParam(string text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/explicit-string-param");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> ImplicitStringParam(string text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/implicit-string-param");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredStringParam(string text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-string-param");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredValueType(int id, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-value-type");
			builder.AddQueryString("id", id);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredDateOnly(System.DateOnly date, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-date-only");
			builder.AddQueryString("date", date);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredDateTime(System.DateTime date, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetime");
			builder.AddQueryString("date", date);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredPostParam(Test.Dto.Classes.MyDto dto, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/required-post-param");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto>(dto);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredStringArray(string[] values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-string-array");
			foreach (var item in values) {
				builder.AddQueryStringIfSet("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredStringCollection(System.Collections.Generic.IEnumerable<string> values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-string-collection");
			foreach (var item in values) {
				builder.AddQueryStringIfSet("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredValueTypeArray(int[] values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-value-type-array");
			foreach (var item in values) {
				builder.AddQueryString("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredValueTypeCollection(System.Collections.Generic.IEnumerable<int> values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-value-type-collection");
			foreach (var item in values) {
				builder.AddQueryString("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredDateOnlyCollection(System.Collections.Generic.IEnumerable<System.DateOnly> dates, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-date-only-collection");
			foreach (var item in dates) {
				builder.AddQueryString("dates", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredDateOnlyArray(System.DateOnly[] dates, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-date-only-array");
			foreach (var item in dates) {
				builder.AddQueryString("dates", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredDateTimeCollection(System.Collections.Generic.IEnumerable<System.DateTime> dates, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetime-collection");
			foreach (var item in dates) {
				builder.AddQueryString("dates", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> RequiredDateTimeArray(System.DateTime[] dates, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetime-array");
			foreach (var item in dates) {
				builder.AddQueryString("dates", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum> RequiredEnum(Test.Dto.Enums.MyEnum value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum");
			builder.AddQueryString("value", value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<Test.Dto.Enums.MyEnum>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum[]> RequiredEnumArray(Test.Dto.Enums.MyEnum[] values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum-array");
			foreach (var item in values) {
				builder.AddQueryString("values", item);
			}
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Enums.MyEnum[]>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}