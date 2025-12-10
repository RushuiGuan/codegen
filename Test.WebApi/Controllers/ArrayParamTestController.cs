using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Test.WebApi.Controllers {
	[Route("api/array-param-test")]
	[ApiController]
	public class ArrayParamTestController : ControllerBase {
		[HttpGet("array-string-param")]
		public string ArrayStringParam([FromQuery(Name = "a")] string[] array) => string.Join(',', array);

		[HttpGet("array-value-type")]
		public string ArrayValueType([FromQuery(Name = "a")] int[] array) => string.Join(',', array);

		[HttpGet("collection-string-param")]
		public string CollectionStringParam([FromQuery(Name = "c")] IEnumerable<string> collection) => string.Join(',', collection);

		[HttpGet("collection-value-type")]
		public string CollectionValueType([FromQuery(Name = "c")] IEnumerable<int> collection) => string.Join(',', collection);

		[HttpGet("collection-date-param")]
		public string CollectionDateParam([FromQuery(Name = "c")] IEnumerable<DateOnly> collection) => string.Join(',', collection);

		[HttpGet("collection-datetime-param")]
		public string CollectionDateTimeParam([FromQuery(Name = "c")] IEnumerable<DateTime> collection) => string.Join(',', collection);

	}
}