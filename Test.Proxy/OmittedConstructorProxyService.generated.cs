using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Net.Http;
#nullable enable
namespace Test.Proxy {
	public partial class OmittedConstructorProxyService : ClientBase {
		public const string ControllerPath = "api/omittedconstructor";
	}
}