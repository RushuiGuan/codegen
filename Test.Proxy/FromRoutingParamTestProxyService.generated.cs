using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.Proxy {
	public partial class FromRoutingParamTestProxyService : ClientBase {
		public FromRoutingParamTestProxyService(ILogger<FromRoutingParamTestProxyService> logger, HttpClient client) : base(logger, client) {
		}
		public const string ControllerPath = "api/from-routing-param-test";
		public async Task ImplicitRoute(string name, int id) {
			string path = $"{ControllerPath}/implicit-route/{name}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task ExplicitRoute(string name, int id) {
			string path = $"{ControllerPath}/explicit-route/{name}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task WildCardRouteDouble(string name, int id) {
			string path = $"{ControllerPath}/wild-card-route-double/{id}/{name}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task WildCardRouteSingle(string name, int id) {
			string path = $"{ControllerPath}/wild-card-route-single/{id}/{name}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task DateTimeRoute(System.DateTime date, int id) {
			string path = $"{ControllerPath}/date-time-route/{date.ISO8601String()}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task DateOnlyRoute(System.DateOnly date, int id) {
			string path = $"{ControllerPath}/date-only-route/{date.ISO8601String()}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task DateTimeOffsetRoute(System.DateTimeOffset date, int id) {
			string path = $"{ControllerPath}/datetimeoffset-route/{date.ISO8601String()}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task TimeOnlyRoute(System.TimeOnly time, int id) {
			string path = $"{ControllerPath}/timeonly-route/{time.ISO8601String()}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
		public async Task EnumRoute(Test.Dto.Enums.MyEnum value, int id) {
			string path = $"{ControllerPath}/enum-route/{value}/{id}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
	}
}