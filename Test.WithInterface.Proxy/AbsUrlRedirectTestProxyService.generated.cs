using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface IAbsUrlRedirectTestProxyService {
		Task Get();
		Task Get1();
		Task Get2();
		Task Get3();
		Task Get4();
		Task Get5();
		Task Get6();
		Task Get7();
		Task Get8();
		Task Get9();
		Task<System.String> Get10();
	}

	public partial class AbsUrlRedirectTestProxyService : ClientBase, IAbsUrlRedirectTestProxyService {
		public AbsUrlRedirectTestProxyService(ILogger<AbsUrlRedirectTestProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/abs-url-redirect-test";
		public async Task Get() {
			string path = $"{ControllerPath}/test-0";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get1() {
			string path = $"{ControllerPath}/test-1";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get2() {
			string path = $"{ControllerPath}/test-2";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get3() {
			string path = $"{ControllerPath}/test-3";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get4() {
			string path = $"{ControllerPath}/test-4";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get5() {
			string path = $"{ControllerPath}/test-5";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get6() {
			string path = $"{ControllerPath}/test-6";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get7() {
			string path = $"{ControllerPath}/test-7";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get8() {
			string path = $"{ControllerPath}/test-8";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task Get9() {
			string path = $"{ControllerPath}/test-9";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}

		public async Task<System.String> Get10() {
			string path = $"{ControllerPath}/test-10";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
	}
}
#nullable disable