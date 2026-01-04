using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface INullableReturnTypeTestProxyService {
		Task<string?> GetString(string? text);
		Task<string?> GetAsyncString(string? text);
		Task<string?> GetActionResultString(string? text);
		Task<string?> GetAsyncActionResultString(string? text);
		Task<System.Nullable<int>> GetInt(System.Nullable<int> n);
		Task<System.Nullable<int>> GetAsyncInt(System.Nullable<int> n);
		Task<System.Nullable<int>> GetActionResultInt(System.Nullable<int> n);
		Task<System.Nullable<int>> GetAsyncActionResultInt(System.Nullable<int> n);
		Task<System.Nullable<System.DateTime>> GetDateTime(System.Nullable<System.DateTime> v);
		Task<System.Nullable<System.DateTime>> GetAsyncDateTime(System.Nullable<System.DateTime> v);
		Task<System.Nullable<System.DateTime>> GetActionResultDateTime(System.Nullable<System.DateTime> v);
		Task<System.Nullable<System.DateTime>> GetAsyncActionResultDateTime(System.Nullable<System.DateTime> v);
		Task<Test.Dto.Classes.MyDto?> GetMyDto(Test.Dto.Classes.MyDto? value);
		Task<Test.Dto.Classes.MyDto?> GetAsyncMyDto(Test.Dto.Classes.MyDto? value);
		Task<Test.Dto.Classes.MyDto?> ActionResultObject(Test.Dto.Classes.MyDto? value);
		Task<Test.Dto.Classes.MyDto?> AsyncActionResultObject(Test.Dto.Classes.MyDto? value);
		Task<Test.Dto.Classes.MyDto?[]> GetMyDtoNullableArray(Test.Dto.Classes.MyDto?[] values);
		Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>> GetMyDtoCollection(System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?> values);
	}
	public partial class NullableReturnTypeTestProxyService : ClientBase, INullableReturnTypeTestProxyService {
		public NullableReturnTypeTestProxyService(ILogger<NullableReturnTypeTestProxyService> logger, HttpClient client) : base(logger, client) { }
		public const string ControllerPath = "api/nullable-return-type";
		public async Task<string?> GetString(string? text) {
			string path = $"{ControllerPath}/string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", text);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string?> GetAsyncString(string? text) {
			string path = $"{ControllerPath}/async-string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", text);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string?> GetActionResultString(string? text) {
			string path = $"{ControllerPath}/action-result-string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", text);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string?> GetAsyncActionResultString(string? text) {
			string path = $"{ControllerPath}/async-action-result-string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", text);
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<System.Nullable<int>> GetInt(System.Nullable<int> n) {
			string path = $"{ControllerPath}/int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<int>>(request);
			}
		}
		public async Task<System.Nullable<int>> GetAsyncInt(System.Nullable<int> n) {
			string path = $"{ControllerPath}/async-int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<int>>(request);
			}
		}
		public async Task<System.Nullable<int>> GetActionResultInt(System.Nullable<int> n) {
			string path = $"{ControllerPath}/action-result-int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<int>>(request);
			}
		}
		public async Task<System.Nullable<int>> GetAsyncActionResultInt(System.Nullable<int> n) {
			string path = $"{ControllerPath}/async-action-result-int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n}");
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<int>>(request);
			}
		}
		public async Task<System.Nullable<System.DateTime>> GetDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", v.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}
		public async Task<System.Nullable<System.DateTime>> GetAsyncDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/async-datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", v.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}
		public async Task<System.Nullable<System.DateTime>> GetActionResultDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/action-result-datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", v.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}
		public async Task<System.Nullable<System.DateTime>> GetAsyncActionResultDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/async-action-result-datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", v.Value.ISO8601String());
			}
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}
		public async Task<Test.Dto.Classes.MyDto?> GetMyDto(Test.Dto.Classes.MyDto? value) {
			string path = $"{ControllerPath}/object";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?>(HttpMethod.Post, path, queryString, value)) {
				return await this.GetJsonResponse<Test.Dto.Classes.MyDto?>(request);
			}
		}
		public async Task<Test.Dto.Classes.MyDto?> GetAsyncMyDto(Test.Dto.Classes.MyDto? value) {
			string path = $"{ControllerPath}/async-object";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?>(HttpMethod.Post, path, queryString, value)) {
				return await this.GetJsonResponse<Test.Dto.Classes.MyDto?>(request);
			}
		}
		public async Task<Test.Dto.Classes.MyDto?> ActionResultObject(Test.Dto.Classes.MyDto? value) {
			string path = $"{ControllerPath}/action-result-object";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?>(HttpMethod.Post, path, queryString, value)) {
				return await this.GetJsonResponse<Test.Dto.Classes.MyDto?>(request);
			}
		}
		public async Task<Test.Dto.Classes.MyDto?> AsyncActionResultObject(Test.Dto.Classes.MyDto? value) {
			string path = $"{ControllerPath}/async-action-result-object";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?>(HttpMethod.Post, path, queryString, value)) {
				return await this.GetJsonResponse<Test.Dto.Classes.MyDto?>(request);
			}
		}
		public async Task<Test.Dto.Classes.MyDto?[]> GetMyDtoNullableArray(Test.Dto.Classes.MyDto?[] values) {
			string path = $"{ControllerPath}/nullable-array-return-type";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?[]>(HttpMethod.Post, path, queryString, values)) {
				return await this.GetRequiredJsonResponse<Test.Dto.Classes.MyDto?[]>(request);
			}
		}
		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>> GetMyDtoCollection(System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?> values) {
			string path = $"{ControllerPath}/nullable-collection-return-type";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>>(HttpMethod.Post, path, queryString, values)) {
				return await this.GetRequiredJsonResponse<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto?>>(request);
			}
		}
	}
}