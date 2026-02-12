using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.Proxy {
	public partial class RequiredParamTestProxyService : ClientBase {
		public RequiredParamTestProxyService(ILogger<RequiredParamTestProxyService> logger, HttpClient client) : base(logger, client) { }
		public const string ControllerPath = "api/required-param-test";
		public async Task<string> ExplicitStringParam(string text) {
			string path = $"{ControllerPath}/explicit-string-param";
			var queryString = new NameValueCollection();
			queryString.Add("text", text);
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> ImplicitStringParam(string text) {
			string path = $"{ControllerPath}/implicit-string-param";
			var queryString = new NameValueCollection();
			queryString.Add("text", text);
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredStringParam(string text) {
			string path = $"{ControllerPath}/required-string-param";
			var queryString = new NameValueCollection();
			queryString.Add("text", text);
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredValueType(int id) {
			string path = $"{ControllerPath}/required-value-type";
			var queryString = new NameValueCollection();
			queryString.Add("id", $"{id}");
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredDateOnly(System.DateOnly date) {
			string path = $"{ControllerPath}/required-date-only";
			var queryString = new NameValueCollection();
			queryString.Add("date", date.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredDateTime(System.DateTime date) {
			string path = $"{ControllerPath}/required-datetime";
			var queryString = new NameValueCollection();
			queryString.Add("date", date.ISO8601String());
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task RequiredPostParam(Test.Dto.Classes.MyDto dto) {
			string path = $"{ControllerPath}/required-post-param";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto>(HttpMethod.Post, path, queryString, dto)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredStringArray(string[] values) {
			string path = $"{ControllerPath}/required-string-array";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				queryString.Add("values", item);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredStringCollection(System.Collections.Generic.IEnumerable<string> values) {
			string path = $"{ControllerPath}/required-string-collection";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				queryString.Add("values", item);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredValueTypeArray(int[] values) {
			string path = $"{ControllerPath}/required-value-type-array";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				queryString.Add("values", $"{item}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredValueTypeCollection(System.Collections.Generic.IEnumerable<int> values) {
			string path = $"{ControllerPath}/required-value-type-collection";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				queryString.Add("values", $"{item}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredDateOnlyCollection(System.Collections.Generic.IEnumerable<System.DateOnly> dates) {
			string path = $"{ControllerPath}/required-date-only-collection";
			var queryString = new NameValueCollection();
			foreach (var item in dates) {
				queryString.Add("dates", item.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredDateOnlyArray(System.DateOnly[] dates) {
			string path = $"{ControllerPath}/required-date-only-array";
			var queryString = new NameValueCollection();
			foreach (var item in dates) {
				queryString.Add("dates", item.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredDateTimeCollection(System.Collections.Generic.IEnumerable<System.DateTime> dates) {
			string path = $"{ControllerPath}/required-datetime-collection";
			var queryString = new NameValueCollection();
			foreach (var item in dates) {
				queryString.Add("dates", item.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> RequiredDateTimeArray(System.DateTime[] dates) {
			string path = $"{ControllerPath}/required-datetime-array";
			var queryString = new NameValueCollection();
			foreach (var item in dates) {
				queryString.Add("dates", item.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<Test.Dto.Enums.MyEnum> RequiredEnum(Test.Dto.Enums.MyEnum value) {
			string path = $"{ControllerPath}/required-enum";
			var queryString = new NameValueCollection();
			queryString.Add("value", $"{value}");
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRequiredJsonResponseForValueType<Test.Dto.Enums.MyEnum>(request);
			}
		}
		public async Task<Test.Dto.Enums.MyEnum[]> RequiredEnumArray(Test.Dto.Enums.MyEnum[] values) {
			string path = $"{ControllerPath}/required-enum-array";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				queryString.Add("values", $"{item}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRequiredJsonResponse<Test.Dto.Enums.MyEnum[]>(request);
			}
		}
	}
}