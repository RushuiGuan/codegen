using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.Proxy {
	public partial class FromQueryParamTestProxyService : ClientBase {
		public FromQueryParamTestProxyService(ILogger<FromQueryParamTestProxyService> logger, HttpClient client) : base(logger, client) { }
		public const string ControllerPath = "api/from-query-param-test";
		public async Task RequiredString(string name) {
			string path = $"{ControllerPath}/required-string";
			var queryString = new NameValueCollection();
			queryString.Add("name", name);
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredStringImplied(string name) {
			string path = $"{ControllerPath}/required-string-implied";
			var queryString = new NameValueCollection();
			queryString.Add("name", name);
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredStringDiffName(string name) {
			string path = $"{ControllerPath}/required-string-diff-name";
			var queryString = new NameValueCollection();
			queryString.Add("n", name);
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredInt(int value) {
			string path = $"{ControllerPath}/required-int";
			var queryString = new NameValueCollection();
			queryString.Add("value", $"{value}");
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredDateTime(System.DateTime datetime) {
			string path = $"{ControllerPath}/required-datetime";
			var queryString = new NameValueCollection();
			queryString.Add("datetime", datetime.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredDateTimeDiffName(System.DateTime datetime) {
			string path = $"{ControllerPath}/required-datetime_diff-name";
			var queryString = new NameValueCollection();
			queryString.Add("d", datetime.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredDateOnly(System.DateOnly dateonly) {
			string path = $"{ControllerPath}/required-dateonly";
			var queryString = new NameValueCollection();
			queryString.Add("dateonly", dateonly.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredDateOnlyDiffName(System.DateOnly dateonly) {
			string path = $"{ControllerPath}/required-dateonly_diff-name";
			var queryString = new NameValueCollection();
			queryString.Add("d", dateonly.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredDateTimeOffset(System.DateTimeOffset dateTimeOffset) {
			string path = $"{ControllerPath}/required-datetimeoffset";
			var queryString = new NameValueCollection();
			queryString.Add("dateTimeOffset", dateTimeOffset.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task RequiredDateTimeOffsetDiffName(System.DateTimeOffset dateTimeOffset) {
			string path = $"{ControllerPath}/required-datetimeoffset_diff-name";
			var queryString = new NameValueCollection();
			queryString.Add("d", dateTimeOffset.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task<Test.Dto.Enums.MyEnum> RequiredEnumParameter(Test.Dto.Enums.MyEnum value) {
			string path = $"{ControllerPath}/required-enum-parameter";
			var queryString = new NameValueCollection();
			queryString.Add("value", $"{value}");
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRequiredJsonResponseForValueType<Test.Dto.Enums.MyEnum>(request);
			}
		}
		public async Task NullableString(string? name) {
			string path = $"{ControllerPath}/nullable-string";
			var queryString = new NameValueCollection();
			if (name != null) {
				queryString.Add("name", name);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableStringImplied(string? name) {
			string path = $"{ControllerPath}/nullable-string-implied";
			var queryString = new NameValueCollection();
			if (name != null) {
				queryString.Add("name", name);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableStringDiffName(string? name) {
			string path = $"{ControllerPath}/nullable-string-diff-name";
			var queryString = new NameValueCollection();
			if (name != null) {
				queryString.Add("n", name);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableInt(System.Nullable<int> value) {
			string path = $"{ControllerPath}/nullable-int";
			var queryString = new NameValueCollection();
			if (value != null) {
				queryString.Add("value", $"{value}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableDateTime(System.Nullable<System.DateTime> datetime) {
			string path = $"{ControllerPath}/nullable-datetime";
			var queryString = new NameValueCollection();
			if (datetime != null) {
				queryString.Add("datetime", datetime.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableDateTimeDiffName(System.Nullable<System.DateTime> datetime) {
			string path = $"{ControllerPath}/nullable-datetime_diff-name";
			var queryString = new NameValueCollection();
			if (datetime != null) {
				queryString.Add("d", datetime.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableDateOnly(System.Nullable<System.DateOnly> dateonly) {
			string path = $"{ControllerPath}/nullable-dateonly";
			var queryString = new NameValueCollection();
			if (dateonly != null) {
				queryString.Add("dateonly", dateonly.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableDateOnlyDiffName(System.Nullable<System.DateOnly> dateonly) {
			string path = $"{ControllerPath}/nullable-dateonly_diff-name";
			var queryString = new NameValueCollection();
			if (dateonly != null) {
				queryString.Add("d", dateonly.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableDateTimeOffset(System.Nullable<System.DateTimeOffset> dateTimeOffset) {
			string path = $"{ControllerPath}/nullable-datetimeoffset";
			var queryString = new NameValueCollection();
			if (dateTimeOffset != null) {
				queryString.Add("dateTimeOffset", dateTimeOffset.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task NullableDateTimeOffsetDiffName(System.Nullable<System.DateTimeOffset> dateTimeOffset) {
			string path = $"{ControllerPath}/nullable-datetimeoffset_diff-name";
			var queryString = new NameValueCollection();
			if (dateTimeOffset != null) {
				queryString.Add("d", dateTimeOffset.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task<Test.Dto.Enums.MyEnum> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value) {
			string path = $"{ControllerPath}/nullable-enum-parameter";
			var queryString = new NameValueCollection();
			if (value != null) {
				queryString.Add("value", $"{value}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRequiredJsonResponseForValueType<Test.Dto.Enums.MyEnum>(request);
			}
		}
	}
}