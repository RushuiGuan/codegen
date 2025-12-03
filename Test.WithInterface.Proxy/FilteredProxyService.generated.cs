using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface IFilteredProxyService {
		Task ThisShouldNoShowUp();
	}

	public partial class FilteredProxyService : ClientBase, IFilteredProxyService {
		public FilteredProxyService(ILogger<FilteredProxyService> logger, HttpClient client) : base(logger, client) {
		}

		public const string ControllerPath = "api/filtered-controller";
		public async Task ThisShouldNoShowUp() {
			string path = $"{ControllerPath}";
			var queryString = new NameValueCollection();
			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				await this.GetRawResponse(request);
			}
		}
	}
}
#nullable disable