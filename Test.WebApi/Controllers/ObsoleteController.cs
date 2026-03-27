using Microsoft.AspNetCore.Mvc;
using System;

namespace Test.WebApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Obsolete("This controller is obsolete.")]
	public class ObsoleteController : ControllerBase {
		[Obsolete("This method is obsolete.")]
		[HttpGet("get")]
		public string Get() {
			return "This is an obsolete method.";
		}
	}
}