using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface INullableParamTestProxyService {
		Task<string> NullableStringParam(string? text);
		Task<string> NullableValueType(System.Nullable<int> id);
		Task<string> NullableDateOnly(System.Nullable<System.DateOnly> date);
		Task NullablePostParam(Test.Dto.Classes.MyDto? dto);
		Task<string> NullableStringArray(string?[] values);
		Task<string> NullableStringCollection(System.Collections.Generic.IEnumerable<string?> values);
		Task<string> NullableValueTypeArray(System.Nullable<int>[] values);
		Task<string> NullableValueTypeCollection(System.Collections.Generic.IEnumerable<System.Nullable<int>> values);
		Task<string> NullableDateOnlyCollection(System.Collections.Generic.IEnumerable<System.Nullable<System.DateOnly>> dates);
		Task<string> NullableDateOnlyArray(System.Nullable<System.DateOnly>[] dates);
		Task<System.Nullable<Test.Dto.Enums.MyEnum>> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value);
		Task<System.Nullable<Test.Dto.Enums.MyEnum>[]> NullableEnumArray(System.Nullable<Test.Dto.Enums.MyEnum>[] value);
	}
	public partial class NullableParamTestProxyService : ClientBase, INullableParamTestProxyService {
		public NullableParamTestProxyService(ILogger<NullableParamTestProxyService> logger, HttpClient client) : base(logger, client) {
		}
		public const string ControllerPath = "api/nullable-param-test";
		public async Task<string> NullableStringParam(string? text) {
			string path = $"{ControllerPath}/nullable-string-param";
			var queryString = new NameValueCollection();
			if (text != null) {
				queryString.Add("text", text);
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableValueType(System.Nullable<int> id) {
			string path = $"{ControllerPath}/nullable-value-type";
			var queryString = new NameValueCollection();
			if (id != null) {
				queryString.Add("id", $"{id}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableDateOnly(System.Nullable<System.DateOnly> date) {
			string path = $"{ControllerPath}/nullable-date-only";
			var queryString = new NameValueCollection();
			if (date != null) {
				queryString.Add("date", date.Value.ISO8601String());
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task NullablePostParam(Test.Dto.Classes.MyDto? dto) {
			string path = $"{ControllerPath}/nullable-post-param";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?>(HttpMethod.Post, path, queryString, dto)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableStringArray(string?[] values) {
			string path = $"{ControllerPath}/nullable-string-array";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				if (item != null) {
					queryString.Add("values", item);
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableStringCollection(System.Collections.Generic.IEnumerable<string?> values) {
			string path = $"{ControllerPath}/nullable-string-collection";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				if (item != null) {
					queryString.Add("values", item);
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableValueTypeArray(System.Nullable<int>[] values) {
			string path = $"{ControllerPath}/nullable-value-type-array";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				if (item != null) {
					queryString.Add("values", $"{item}");
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableValueTypeCollection(System.Collections.Generic.IEnumerable<System.Nullable<int>> values) {
			string path = $"{ControllerPath}/nullable-value-type-collection";
			var queryString = new NameValueCollection();
			foreach (var item in values) {
				if (item != null) {
					queryString.Add("values", $"{item}");
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableDateOnlyCollection(System.Collections.Generic.IEnumerable<System.Nullable<System.DateOnly>> dates) {
			string path = $"{ControllerPath}/nullable-date-only-collection";
			var queryString = new NameValueCollection();
			foreach (var item in dates) {
				if (item != null) {
					queryString.Add("dates", item.Value.ISO8601String());
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<string> NullableDateOnlyArray(System.Nullable<System.DateOnly>[] dates) {
			string path = $"{ControllerPath}/nullable-date-only-array";
			var queryString = new NameValueCollection();
			foreach (var item in dates) {
				if (item != null) {
					queryString.Add("dates", item.Value.ISO8601String());
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
		public async Task<System.Nullable<Test.Dto.Enums.MyEnum>> NullableEnumParameter(System.Nullable<Test.Dto.Enums.MyEnum> value) {
			string path = $"{ControllerPath}/nullable-enum-parameter";
			var queryString = new NameValueCollection();
			if (value != null) {
				queryString.Add("value", $"{value}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetJsonResponse<System.Nullable<Test.Dto.Enums.MyEnum>>(request);
			}
		}
		public async Task<System.Nullable<Test.Dto.Enums.MyEnum>[]> NullableEnumArray(System.Nullable<Test.Dto.Enums.MyEnum>[] value) {
			string path = $"{ControllerPath}/nullable-enum-array";
			var queryString = new NameValueCollection();
			foreach (var item in value) {
				if (item != null) {
					queryString.Add("value", $"{item}");
				}
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRequiredJsonResponse<System.Nullable<Test.Dto.Enums.MyEnum>[]>(request);
			}
		}
	}
}