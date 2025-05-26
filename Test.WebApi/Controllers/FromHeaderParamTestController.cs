using Microsoft.AspNetCore.Mvc;
using System;

namespace Test.WebApi.Controllers {
	[Route("api/from-header-param-test")]
	public class FromHeaderParamTestController : ControllerBase {
		public class Context {
			[FromHeader(Name = "id")]
			public string? Id { get; set; }

			[FromHeader(Name = "type")]
			public string? Type { get; set; }
		}

		[HttpGet]
		public void OmitFromHeaderParameters([FromHeader(Name = "n")] string name, [FromHeader] Context context) { }
	}
}