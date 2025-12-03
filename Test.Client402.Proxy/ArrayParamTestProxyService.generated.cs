using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable
namespace Test.Proxy {
	public partial class ArrayParamTestProxyService : ClientBase {
		public ArrayParamTestProxyService(ILogger<ArrayParamTestProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/array-param-test";
		public async Task<System.String> ArrayStringParam(System.String[] array) {
			string path = $"{ControllerPath}/array-string-param";
			var queryString = new NameValueCollection();
			foreach (var item in array) {
				queryString.Add("a", $"{item}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String> ArrayValueType(System.Int32[] array) {
			string path = $"{ControllerPath}/array-value-type";
			var queryString = new NameValueCollection();
			foreach (var item in array) {
				queryString.Add("a", $"{item}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String> CollectionStringParam(System.Collections.Generic.IEnumerable<System.String> collection) {
			string path = $"{ControllerPath}/collection-string-param";
			var queryString = new NameValueCollection();
			foreach (var item in collection) {
				queryString.Add("c", $"{item}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String> CollectionValueType(System.Collections.Generic.IEnumerable<System.Int32> collection) {
			string path = $"{ControllerPath}/collection-value-type";
			var queryString = new NameValueCollection();
			foreach (var item in collection) {
				queryString.Add("c", $"{item}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String> CollectionDateParam(System.Collections.Generic.IEnumerable<System.DateOnly> collection) {
			string path = $"{ControllerPath}/collection-date-param";
			var queryString = new NameValueCollection();
			foreach (var item in collection) {
				queryString.Add("c", $"{item:yyyy-MM-dd}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}

		public async Task<System.String> CollectionDateTimeParam(System.Collections.Generic.IEnumerable<System.DateTime> collection) {
			string path = $"{ControllerPath}/collection-datetime-param";
			var queryString = new NameValueCollection();
			foreach (var item in collection) {
				queryString.Add("c", $"{item:yyyy-MM-ddTHH:mm:ssK}");
			}

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
	}
}
#nullable disable