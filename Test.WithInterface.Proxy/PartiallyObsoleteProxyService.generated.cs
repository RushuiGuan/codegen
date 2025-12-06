using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface IPartiallyObsoleteProxyService {
		Task<string> Get();
	}
	public partial class PartiallyObsoleteProxyService : ClientBase, IPartiallyObsoleteProxyService {
		public PartiallyObsoleteProxyService(ILogger<PartiallyObsoleteProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/partiallyobsolete";
		public async Task<string> Get() {
			string path = $"{ControllerPath}/get";
			var queryString = new NameValueCollection();

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
	}
}