using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Test.WebApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class CustomRouteTypeController : ControllerBase {
		[HttpGet("{name}")]
		public string Get([FromRoute] EntityName name, CancellationToken cancellationToken) {
			return name.Value;
		}
	}
}