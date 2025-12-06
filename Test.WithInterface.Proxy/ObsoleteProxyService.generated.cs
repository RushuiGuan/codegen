using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	[System.ObsoleteAttribute()]
	public partial interface IObsoleteProxyService {
		Task<string> Get();
	}
	[System.ObsoleteAttribute()]
	public partial class ObsoleteProxyService : ClientBase, IObsoleteProxyService {
		public ObsoleteProxyService(ILogger<ObsoleteProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/obsolete";
		public async Task<string> Get() {
			string path = $"{ControllerPath}/get";
			var queryString = new NameValueCollection();

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
	}
}