using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface ICancellationTokenTestProxyService {
		Task<string> Get();
	}
	public partial class CancellationTokenTestProxyService : ClientBase, ICancellationTokenTestProxyService {
		public CancellationTokenTestProxyService(ILogger<CancellationTokenTestProxyService> logger, HttpClient client) : base(logger, client) {
		}
		public const string ControllerPath = "api/cancellationtokentest";
		public async Task<string> Get() {
			string path = $"{ControllerPath}";
			var queryString = new NameValueCollection();

			using (var request = this.CreateRequest(HttpMethod.Get, path, queryString)) {
				return await this.GetRawResponse(request);
			}
		}
	}
}