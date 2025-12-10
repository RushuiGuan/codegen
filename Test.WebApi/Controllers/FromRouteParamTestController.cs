using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using Test.Dto.Enums;

namespace Test.WebApi.Controllers {
	[Route("api/from-routing-param-test")]
	[ApiController]
	public class FromRoutingParamTestController : ControllerBase {
		private readonly ILogger logger;

		public FromRoutingParamTestController(ILogger logger) {
			this.logger = logger;
		}

		[HttpGet("implicit-route/{name}/{id}")]
		public void ImplicitRoute(string name, int id) { }

		[HttpGet("explicit-route/{name}/{id}")]
		public void ExplicitRoute([FromRoute] string name, [FromRoute] int id) { }

		[HttpGet("wild-card-route-double/{id}/{**name}")]
		public void WildCardRouteDouble([FromRoute] string name, [FromRoute] int id) { }

		[HttpGet("wild-card-route-single/{id}/{*name}")]
		public void WildCardRouteSingle([FromRoute] string name, [FromRoute] int id) { }

		[HttpGet("date-time-route/{date}/{id}")]
		public void DateTimeRoute([FromRoute] DateTime date, [FromRoute] int id) { }

		[HttpGet("date-only-route/{date}/{id}")]
		public void DateOnlyRoute([FromRoute] DateOnly date, [FromRoute] int id) { }

		[HttpGet("datetimeoffset-route/{date}/{id}")]
		public void DateTimeOffsetRoute([FromRoute] DateTimeOffset date, [FromRoute] int id) { }

		[HttpGet("timeonly-route/{time}/{id}")]
		public void TimeOnlyRoute([FromRoute] TimeOnly time, [FromRoute] int id) { }

		[HttpGet("enum-route/{value}/{id}")]
		public void EnumRoute([FromRoute] MyEnum value, [FromRoute] int id) { }
	}
}