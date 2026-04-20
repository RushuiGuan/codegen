using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Client {
	public partial class RequiredReturnTypeTestClient {
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
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task GetAsync(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-task");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task GetActionResult(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task GetAsyncActionResult(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result");
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/string");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetAsyncString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-string");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetActionResultString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-string");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<string> GetAsyncActionResultString(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-string");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/int");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetAsyncInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-int");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetActionResultInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-int");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<int> GetAsyncActionResultInt(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-int");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<int>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/datetime");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetAsyncDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-datetime");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetActionResultDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-datetime");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTime> GetAsyncActionResultDateTime(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-datetime");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTime>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateOnly> GetDateOnly(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/dateonly");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateOnly>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.DateTimeOffset> GetDateTimeOffset(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/datetimeoffset");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.DateTimeOffset>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.TimeOnly> GetTimeOnly(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/timeonly");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<System.TimeOnly>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> GetMyDto(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/object");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> GetAsyncMyDto(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-object");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> ActionResultObject(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/action-result-object");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto> AsyncActionResultObject(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-action-result-object");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.MyDto[]> GetMyDtoArray(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/array-return-type");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.MyDto[]>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollection(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/collection-return-type");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollectionAsync(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/async-collection-return-type");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum> RequiredEnum(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrowStruct<Test.Dto.Enums.MyEnum>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum[]> RequiredEnumArray(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum-array");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Enums.MyEnum[]>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<dynamic> GetDynamic(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/dynamic");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<dynamic>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
	}
}