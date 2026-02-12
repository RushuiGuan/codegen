using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface IRequiredReturnTypeTestClient {
		Task Get(CancellationToken cancellationToken);
		Task GetAsync(CancellationToken cancellationToken);
		Task GetActionResult(CancellationToken cancellationToken);
		Task GetAsyncActionResult(CancellationToken cancellationToken);
		Task<string> GetString(CancellationToken cancellationToken);
		Task<string> GetAsyncString(CancellationToken cancellationToken);
		Task<string> GetActionResultString(CancellationToken cancellationToken);
		Task<string> GetAsyncActionResultString(CancellationToken cancellationToken);
		Task<int> GetInt(CancellationToken cancellationToken);
		Task<int> GetAsyncInt(CancellationToken cancellationToken);
		Task<int> GetActionResultInt(CancellationToken cancellationToken);
		Task<int> GetAsyncActionResultInt(CancellationToken cancellationToken);
		Task<System.DateTime> GetDateTime(CancellationToken cancellationToken);
		Task<System.DateTime> GetAsyncDateTime(CancellationToken cancellationToken);
		Task<System.DateTime> GetActionResultDateTime(CancellationToken cancellationToken);
		Task<System.DateTime> GetAsyncActionResultDateTime(CancellationToken cancellationToken);
		Task<System.DateOnly> GetDateOnly(CancellationToken cancellationToken);
		Task<System.DateTimeOffset> GetDateTimeOffset(CancellationToken cancellationToken);
		Task<System.TimeOnly> GetTimeOnly(CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto> GetMyDto(CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto> GetAsyncMyDto(CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto> ActionResultObject(CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto> AsyncActionResultObject(CancellationToken cancellationToken);
		Task<Test.Dto.Classes.MyDto[]> GetMyDtoArray(CancellationToken cancellationToken);
		Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollection(CancellationToken cancellationToken);
		Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollectionAsync(CancellationToken cancellationToken);
		Task<Test.Dto.Enums.MyEnum> RequiredEnum(CancellationToken cancellationToken);
		Task<Test.Dto.Enums.MyEnum[]> RequiredEnumArray(CancellationToken cancellationToken);
		Task<dynamic> GetDynamic(CancellationToken cancellationToken);
	}
	public partial class RequiredReturnTypeTestClient : IRequiredReturnTypeTestClient {
		public RequiredReturnTypeTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/required-return-type";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task Get(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/void");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task GetAsync(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-task");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task GetActionResult(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task GetAsyncActionResult(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result");
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/string");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetAsyncString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-string");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetActionResultString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-string");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetAsyncActionResultString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-string");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/int");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetAsyncInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-int");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetActionResultInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-int");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetAsyncActionResultInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-int");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/datetime");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetAsyncDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-datetime");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetActionResultDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-datetime");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetAsyncActionResultDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-datetime");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateOnly> GetDateOnly(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/dateonly");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateOnly>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTimeOffset> GetDateTimeOffset(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/datetimeoffset");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTimeOffset>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.TimeOnly> GetTimeOnly(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/timeonly");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.TimeOnly>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> GetMyDto(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/object");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> GetAsyncMyDto(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-object");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> ActionResultObject(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-object");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> AsyncActionResultObject(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-object");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto[]> GetMyDtoArray(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/array-return-type");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto[]>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollection(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-return-type");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollectionAsync(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-collection-return-type");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum> RequiredEnum(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<Test.Dto.Enums.MyEnum>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum[]> RequiredEnumArray(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum-array");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Enums.MyEnum[]>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<dynamic> GetDynamic(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/dynamic");
			using var request = builder.Build();
			return await this.client.ExecuteOrThrow<dynamic>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}