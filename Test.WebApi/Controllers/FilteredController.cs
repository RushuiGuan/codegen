using Microsoft.AspNetCore.Mvc;

namespace Test.WebApi.Controllers {
	[Route("api/filtered-controller")]
	[ApiController]
	public class FilteredController : ControllerBase {
		[HttpGet]
		public void ThisShouldNoShowUp() { }
	}
}