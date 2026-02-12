using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Client {
	public partial interface IFromQueryParamTestClient {
		Task RequiredString(string name, CancellationToken cancellationToken);
		Task RequiredStringImplied(string name, CancellationToken cancellationToken);
		Task RequiredStringDiffName(string name, CancellationToken cancellationToken);
		Task RequiredInt(int value, CancellationToken cancellationToken);
		Task RequiredDateTime(System.DateTime datetime, CancellationToken cancellationToken);
		Task RequiredDateTimeDiffName(System.DateTime datetime, CancellationToken cancellationToken);
		Task RequiredDateOnly(System.DateOnly dateonly, CancellationToken cancellationToken);
		Task RequiredDateOnlyDiffName(System.DateOnly dateonly, CancellationToken cancellationToken);
		Task RequiredDateTimeOffset(System.DateTimeOffset dateTimeOffset, CancellationToken cancellationToken);
		Task RequiredDateTimeOffsetDiffName(System.DateTimeOffset dateTimeOffset, CancellationToken cancellationToken);
		Task<Test.Dto.Enums.MyEnum> RequiredEnumParameter(Test.Dto.Enums.MyEnum value, CancellationToken cancellationToken);
		Task NullableString(string? name, CancellationToken cancellationToken);
		Task NullableStringImplied(string? name, CancellationToken cancellationToken);
		Task NullableStringDiffName(string? name, CancellationToken cancellationToken);
		Task NullableInt(System.Nullable<int> value, CancellationToken cancellationToken);
		Task NullableDateTime(System.Nullable<System.DateTime> datetime, CancellationToken cancellationToken);
		Task NullableDateTimeDiffName(System.Nullable<System.DateTime> datetime, CancellationToken cancellationToken);
		Task NullableDateOnly(System.Nullable<System.DateOnly> dateonly, CancellationToken cancellationToken);
		Task NullableDateOnlyDiffName(System.Nullable<System.DateOnly> dateonly, CancellationToken cancellationToken);
		Task NullableDateTimeOffset(System.Nullable<System.DateTimeOffset> dateTimeOffset, CancellationToken cancellationToken);
		Task NullableDateTimeOffsetDiffName(System.Nullable<System.DateTimeOffset> dateTimeOffset, CancellationToken cancellationToken);
		Task<Test.Dto.Enums.MyEnum> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value, CancellationToken cancellationToken);
	}
	public partial class FromQueryParamTestClient : IFromQueryParamTestClient {
		public FromQueryParamTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/from-query-param-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task RequiredString(string name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-string");
			builder.AddQueryStringIfSet("name", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredStringImplied(string name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-string-implied");
			builder.AddQueryStringIfSet("name", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredStringDiffName(string name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-string-diff-name");
			builder.AddQueryStringIfSet("n", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredInt(int value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-int");
			builder.AddQueryString("value", value);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredDateTime(System.DateTime datetime, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetime");
			builder.AddQueryString("datetime", datetime);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredDateTimeDiffName(System.DateTime datetime, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetime_diff-name");
			builder.AddQueryString("d", datetime);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredDateOnly(System.DateOnly dateonly, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-dateonly");
			builder.AddQueryString("dateonly", dateonly);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredDateOnlyDiffName(System.DateOnly dateonly, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-dateonly_diff-name");
			builder.AddQueryString("d", dateonly);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredDateTimeOffset(System.DateTimeOffset dateTimeOffset, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetimeoffset");
			builder.AddQueryString("dateTimeOffset", dateTimeOffset);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task RequiredDateTimeOffsetDiffName(System.DateTimeOffset dateTimeOffset, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-datetimeoffset_diff-name");
			builder.AddQueryString("d", dateTimeOffset);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum> RequiredEnumParameter(Test.Dto.Enums.MyEnum value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/required-enum-parameter");
			builder.AddQueryString("value", value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<Test.Dto.Enums.MyEnum>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableString(string? name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-string");
			builder.AddQueryStringIfSet("name", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableStringImplied(string? name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-string-implied");
			builder.AddQueryStringIfSet("name", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableStringDiffName(string? name, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-string-diff-name");
			builder.AddQueryStringIfSet("n", name);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableInt(System.Nullable<int> value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-int");
			builder.AddQueryStringIfSet("value", value);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableDateTime(System.Nullable<System.DateTime> datetime, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-datetime");
			builder.AddQueryStringIfSet("datetime", datetime);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableDateTimeDiffName(System.Nullable<System.DateTime> datetime, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-datetime_diff-name");
			builder.AddQueryStringIfSet("d", datetime);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableDateOnly(System.Nullable<System.DateOnly> dateonly, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-dateonly");
			builder.AddQueryStringIfSet("dateonly", dateonly);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableDateOnlyDiffName(System.Nullable<System.DateOnly> dateonly, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-dateonly_diff-name");
			builder.AddQueryStringIfSet("d", dateonly);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableDateTimeOffset(System.Nullable<System.DateTimeOffset> dateTimeOffset, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-datetimeoffset");
			builder.AddQueryStringIfSet("dateTimeOffset", dateTimeOffset);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task NullableDateTimeOffsetDiffName(System.Nullable<System.DateTimeOffset> dateTimeOffset, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-datetimeoffset_diff-name");
			builder.AddQueryStringIfSet("d", dateTimeOffset);
			using var request = builder.Build();
			await this.client.Send<string>(request, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Enums.MyEnum> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Get)
				.WithRelativeUrl($"{ControllerPath}/nullable-enum-parameter");
			builder.AddQueryStringIfSet("value", value);
			using var request = builder.Build();
			return await this.client.ExecuteOrThrowStruct<Test.Dto.Enums.MyEnum>(request, this.jsonSerializerOptions, cancellationToken);
		}
	}
}