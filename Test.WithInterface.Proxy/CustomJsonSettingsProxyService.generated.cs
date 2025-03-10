using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Net.Http;

#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface ICustomJsonSettingsProxyService {
	}

	public partial class CustomJsonSettingsProxyService : ClientBase, ICustomJsonSettingsProxyService {
		public CustomJsonSettingsProxyService(ILogger<CustomJsonSettingsProxyService> logger, HttpClient client) : base(logger, client, MyCustomJsonSettings.Options) {
		}

		public const string ControllerPath = "api/customjsonsettings";
	}
}
#nullable disable

