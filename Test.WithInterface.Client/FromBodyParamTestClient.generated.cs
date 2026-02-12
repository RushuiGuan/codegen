using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface IFromBodyParamTestClient {
		Task<int> RequiredObject(Test.Dto.Classes.MyDto dto, CancellationToken cancellationToken);
		Task<int> NullableObject(Test.Dto.Classes.MyDto? dto, CancellationToken cancellationToken);
		Task<int> RequiredInt(int value, CancellationToken cancellationToken);
		Task<int> NullableInt(System.Nullable<int> value, CancellationToken cancellationToken);
		Task<int> RequiredString(string value, CancellationToken cancellationToken);
		Task<int> NullableString(string? value, CancellationToken cancellationToken);
		Task<int> RequiredObjectArray(Test.Dto.Classes.MyDto[] array, CancellationToken cancellationToken);
		Task<int> NullableObjectArray(Test.Dto.Classes.MyDto?[] array, CancellationToken cancellationToken);
	}
	public partial class FromBodyParamTestClient : IFromBodyParamTestClient {
		public FromBodyParamTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/from-body-param-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task<int> RequiredObject(Test.Dto.Classes.MyDto dto, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/required-object");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto>(dto);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> NullableObject(Test.Dto.Classes.MyDto? dto, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-object");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?>(dto);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> RequiredInt(int value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/required-int");
			builder.CreateJsonRequest<int>(value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> NullableInt(System.Nullable<int> value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-int");
			builder.CreateJsonRequest<System.Nullable<int>>(value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> RequiredString(string value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/required-string");
			builder.CreateStringRequest(value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> NullableString(string? value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-string");
			builder.CreateStringRequest(value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> RequiredObjectArray(Test.Dto.Classes.MyDto[] array, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/required-object-array");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto[]>(array);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> NullableObjectArray(Test.Dto.Classes.MyDto?[] array, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/nullable-object-array");
			builder.CreateJsonRequest<Test.Dto.Classes.MyDto?[]>(array);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}