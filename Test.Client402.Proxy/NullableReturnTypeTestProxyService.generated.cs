using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable
namespace Test.Proxy {
	public partial class NullableReturnTypeTestProxyService : ClientBase {
		public NullableReturnTypeTestProxyService(ILogger<NullableReturnTypeTestProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/nullable-return-type";
		public async Task<System.String?> GetString(System.String? text) {
			string path = $"{ControllerPath}/string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", $"{text}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String?> GetAsyncString(System.String? text) {
			string path = $"{ControllerPath}/async-string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", $"{text}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String?> GetActionResultString(System.String? text) {
			string path = $"{ControllerPath}/action-result-string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", $"{text}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String?> GetAsyncActionResultString(System.String? text) {
			string path = $"{ControllerPath}/async-action-result-string";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", $"{text}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.Nullable<System.Int32>> GetInt(System.Nullable<System.Int32> n) {
			string path = $"{ControllerPath}/int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n.Value}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.Int32>>(request);
			}
		}

		public async Task<System.Nullable<System.Int32>> GetAsyncInt(System.Nullable<System.Int32> n) {
			string path = $"{ControllerPath}/async-int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n.Value}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.Int32>>(request);
			}
		}

		public async Task<System.Nullable<System.Int32>> GetActionResultInt(System.Nullable<System.Int32> n) {
			string path = $"{ControllerPath}/action-result-int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n.Value}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.Int32>>(request);
			}
		}

		public async Task<System.Nullable<System.Int32>> GetAsyncActionResultInt(System.Nullable<System.Int32> n) {
			string path = $"{ControllerPath}/async-action-result-int";
			var queryString = new NameValueCollection();
			if (n != null) {
				queryString.Add("n", $"{n.Value}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.Int32>>(request);
			}
		}

		public async Task<System.Nullable<System.DateTime>> GetDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", $"{v.Value:yyyy-MM-ddTHH:mm:ssK}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}

		public async Task<System.Nullable<System.DateTime>> GetAsyncDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/async-datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", $"{v.Value:yyyy-MM-ddTHH:mm:ssK}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}

		public async Task<System.Nullable<System.DateTime>> GetActionResultDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/action-result-datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", $"{v.Value:yyyy-MM-ddTHH:mm:ssK}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<System.DateTime>>(request);
			}
		}

		public async Task<System.Nullable<System.DateTime>> GetAsyncActionResultDateTime(System.Nullable<System.DateTime> v) {
			string path = $"{ControllerPath}/async-action-result-datetime";
			var queryString = new NameValueCollection();
			if (v != null) {
				queryString.Add("v", $"{v.Value:yyyy-MM-ddTHH:mm:ssK}");
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
				return await this.GetJsonResponse<Test.Dto.Classes.MyDto?[]>(request);
			}
		}

		public async Task<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>> GetMyDtoCollection(System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto> values) {
			string path = $"{ControllerPath}/nullable-collection-return-type";
			var queryString = new NameValueCollection();
			using (var request = this.CreateJsonRequest<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>>(HttpMethod.Post, path, queryString, values)) {
				return await this.GetJsonResponse<System.Collections.Generic.IEnumerable<Test.Dto.Classes.MyDto>>(request);
			}
		}
	}
}
#nullable disable