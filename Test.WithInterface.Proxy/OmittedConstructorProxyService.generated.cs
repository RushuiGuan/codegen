using Albatross.Dates;
using Albatross.WebClient;
using Microsoft.Extensions.Logging;
using System.Net.Http;
#nullable enable
namespace Test.WithInterface.Proxy {
	public partial interface IOmittedConstructorProxyService {
	}
	public partial class OmittedConstructorProxyService : ClientBase, IOmittedConstructorProxyService {
		public const string ControllerPath = "api/omittedconstructor";
	}
}