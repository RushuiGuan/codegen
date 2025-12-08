using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface IFromHeaderParamTestProxyService {
		Task OmitFromHeaderParameters();
	}
	public partial class FromHeaderParamTestProxyService : ClientBase, IFromHeaderParamTestProxyService {
		public FromHeaderParamTestProxyService(ILogger<FromHeaderParamTestProxyService> logger, HttpClient client) : base(logger, client) {
		}
		public const string ControllerPath = "api/from-header-param-test";
		public async Task OmitFromHeaderParameters() {
			string path = $"{ControllerPath}";
			var queryString = new NameValueCollection();

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
	}
}