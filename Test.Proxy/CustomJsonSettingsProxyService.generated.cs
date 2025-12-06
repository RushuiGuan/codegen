using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Net.Http;
#nullable enable
namespace Test.Proxy {
	public partial class CustomJsonSettingsProxyService : ClientBase {
		public CustomJsonSettingsProxyService(ILogger<CustomJsonSettingsProxyService> logger, HttpClient client) : base(logger, client, MyCustomJsonSettings.Options) {
		}
		public const string ControllerPath = "api/customjsonsettings";
	}
}