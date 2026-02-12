using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface INullableReturnTypeTestClient {
		Task<string?> GetString(string? text, CancellationToken cancellationToken);
		Task<string?> GetAsyncString(string? text, CancellationToken cancellationToken);
		Task<string?> GetActionResultString(string? text, CancellationToken cancellationToken);
		Task<string?> GetAsyncActionResultString(string? text, CancellationToken cancellationToken);
		Task<System.Nullable<int>> GetInt(System.Nullable<int> n, CancellationToken cancellationToken);
		Task<System.Nullable<int>> GetAsyncInt(System.Nullable<int> n, CancellationToken cancellationToken);
		Task<System.Nullable<int>> GetActionResultInt(System.Nullable<int> n, CancellationToken cancellationToken);
		Task<System.Nullable<int>> GetAsyncActionResultInt(System.Nullable<int> n, CancellationToken cancellationToken);
		Task<System.Nullable<System.DateTime>> GetDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken);
		Task<System.Nullable<System.DateTime>> GetAsyncDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken);
		Task<System.Nullable<System.DateTime>> GetActionResultDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken);
		Task<System.Nullable<System.DateTime>> GetAsyncActionResultDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto?> GetMyDto(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto?> GetAsyncMyDto(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto?> ActionResultObject(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto?> AsyncActionResultObject(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto?[]> GetMyDtoNullableArray(Test.Dto.Classes.MyDto?[] values, CancellationToken cancellationToken);
		Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>> GetMyDtoCollection(System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?> values, CancellationToken cancellationToken);
	}
	public partial class NullableReturnTypeTestClient : INullableReturnTypeTestClient {
		public NullableReturnTypeTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/nullable-return-type";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<string?> GetString(string? text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/string");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.Execute<string?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string?> GetAsyncString(string? text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-string");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.Execute<string?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string?> GetActionResultString(string? text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-string");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.Execute<string?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string?> GetAsyncActionResultString(string? text, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-string");
			builder.AddQueryStringIfSet("text", text);
			using var request = builder.Build();
			return await this.client.Execute<string?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<int>> GetInt(System.Nullable<int> n, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/int");
			builder.AddQueryStringIfSet("n", n);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<int>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<int>> GetAsyncInt(System.Nullable<int> n, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-int");
			builder.AddQueryStringIfSet("n", n);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<int>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<int>> GetActionResultInt(System.Nullable<int> n, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-int");
			builder.AddQueryStringIfSet("n", n);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<int>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<int>> GetAsyncActionResultInt(System.Nullable<int> n, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-int");
			builder.AddQueryStringIfSet("n", n);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<int>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<System.DateTime>> GetDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/datetime");
			builder.AddQueryStringIfSet("v", v);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<System.DateTime>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<System.DateTime>> GetAsyncDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-datetime");
			builder.AddQueryStringIfSet("v", v);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<System.DateTime>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<System.DateTime>> GetActionResultDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-datetime");
			builder.AddQueryStringIfSet("v", v);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<System.DateTime>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Nullable<System.DateTime>> GetAsyncActionResultDateTime(System.Nullable<System.DateTime> v, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-datetime");
			builder.AddQueryStringIfSet("v", v);
			using var request = builder.Build();
			return await this.client.Execute<System.Nullable<System.DateTime>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto?> GetMyDto(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/object");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?>(value);
			using var request = builder.Build();
			return await this.client.Execute<Test.Dto.Classes.MyDto?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto?> GetAsyncMyDto(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/async-object");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?>(value);
			using var request = builder.Build();
			return await this.client.Execute<Test.Dto.Classes.MyDto?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto?> ActionResultObject(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/action-result-object");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?>(value);
			using var request = builder.Build();
			return await this.client.Execute<Test.Dto.Classes.MyDto?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto?> AsyncActionResultObject(Test.Dto.Classes.MyDto? value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-object");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?>(value);
			using var request = builder.Build();
			return await this.client.Execute<Test.Dto.Classes.MyDto?>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto?[]> GetMyDtoNullableArray(Test.Dto.Classes.MyDto?[] values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-array-return-type");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?[]>(values);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto?[]>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>> GetMyDtoCollection(System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?> values, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-collection-return-type");
			builder.CreateJsonRequest<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>>(values);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}