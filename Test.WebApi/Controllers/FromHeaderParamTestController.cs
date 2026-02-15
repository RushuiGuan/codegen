using Microsoft.AspNetCore.Mvc;
using System;

namespace Test.WebApi.Controllers {
	[Route("api/from-header-param-test")]
	[ApiController]
	public class FromHeaderParamTestController : ControllerBase {
		[HttpGet]
		public void OmitFromHeaderParameters([FromHeader(Name = "n")] string name) { }
	}
}