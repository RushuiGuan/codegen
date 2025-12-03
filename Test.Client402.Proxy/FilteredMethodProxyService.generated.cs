using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable
namespace Test.Proxy {
	public partial class FilteredMethodProxyService : ClientBase {
		public FilteredMethodProxyService(ILogger<FilteredMethodProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/filtered-method";
		public async Task FilteredByNone() {
			string path = $"{ControllerPath}/none";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
	}
}
#nullable disable