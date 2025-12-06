using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface IFromBodyParamTestProxyService {
		Task<int> RequiredObject(Test.Dto.Classes.MyDto dto);
		Task<int> NullableObject(Test.Dto.Classes.MyDto? dto);
		Task<int> RequiredInt(int value);
		Task<int> NullableInt(System.Nullable<int> value);
		Task<int> RequiredString(string value);
		Task<int> NullableString(string? value);
		Task<int> RequiredObjectArray(Test.Dto.Classes.MyDto[] array);
		Task<int> NullableObjectArray(Test.Dto.Classes.MyDto?[] array);
	}
	public partial class FromBodyParamTestProxyService : ClientBase, IFromBodyParamTestProxyService {
		public FromBodyParamTestProxyService(ILogger<FromBodyParamTestProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/from-body-param-test";
		public async Task<int> RequiredObject(Test.Dto.Classes.MyDto dto) {
			string path = $"{ControllerPath}/required-object";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto>(HttpMethod.Post, path, queryString, dto)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> NullableObject(Test.Dto.Classes.MyDto? dto) {
			string path = $"{ControllerPath}/nullable-object";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?>(HttpMethod.Post, path, queryString, dto)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> RequiredInt(int value) {
			string path = $"{ControllerPath}/required-int";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<int>(HttpMethod.Post, path, queryString, value)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> NullableInt(System.Nullable<int> value) {
			string path = $"{ControllerPath}/nullable-int";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<System.Nullable<int>>(HttpMethod.Post, path, queryString, value)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> RequiredString(string value) {
			string path = $"{ControllerPath}/required-string";
			var queryString = new NameValueCollection();

			using (var request = this.CreateStringRequest(HttpMethod.Post, path, queryString, value)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> NullableString(string? value) {
			string path = $"{ControllerPath}/nullable-string";
			var queryString = new NameValueCollection();

			using (var request = this.CreateStringRequest(HttpMethod.Post, path, queryString, value)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> RequiredObjectArray(Test.Dto.Classes.MyDto[] array) {
			string path = $"{ControllerPath}/required-object-array";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto[]>(HttpMethod.Post, path, queryString, array)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
		public async Task<int> NullableObjectArray(Test.Dto.Classes.MyDto?[] array) {
			string path = $"{ControllerPath}/nullable-object-array";
			var queryString = new NameValueCollection();

			using (var request = this.CreateJsonRequest<Test.Dto.Classes.MyDto?[]>(HttpMethod.Post, path, queryString, array)) {
				return await this.GetRequiredJsonResponseForValueType<int>(request);
			}
		}
	}
}