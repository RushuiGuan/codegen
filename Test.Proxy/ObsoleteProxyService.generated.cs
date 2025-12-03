using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable
namespace Test.Proxy {
	[System.ObsoleteAttribute]
	public partial class ObsoleteProxyService : ClientBase {
		public ObsoleteProxyService(ILogger<ObsoleteProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/obsolete";
		public async Task<System.String> Get() {
			string path = $"{ControllerPath}/get";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
	}
}
#nullable disable