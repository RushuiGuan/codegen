using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.UnitTest.AspNetCoreWebApiProxy {
	[Route("api/{controller}")]
	public class ValueController: Controller {
		[HttpGet]
		public string Test() {
			return string.Empty;
		}
	}
}
