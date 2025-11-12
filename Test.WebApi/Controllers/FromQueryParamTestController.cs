using Microsoft.AspNetCore.Mvc;
using System;
using Test.Dto.Enums;

namespace Test.WebApi.Controllers {
	[Route("api/from-query-param-test")]
	[ApiController]
	public class FromQueryParamTestController : ControllerBase {
		[HttpGet("required-string")]
		public void RequiredString([FromQuery] string name) { }

		[HttpGet("required-string-implied")]
		public void RequiredStringImplied(string name) { }

		[HttpGet("required-string-diff-name")]
		public void RequiredStringDiffName([FromQuery(Name = "n")] string name) { }

		[HttpGet("required-datetime")]
		public void RequiredDateTime(DateTime datetime) { }

		[HttpGet("required-datetime_diff-name")]
		public void RequiredDateTimeDiffName([FromQuery(Name = "d")] DateTime datetime) { }

		[HttpGet("required-dateonly")]
		public void RequiredDateOnly(DateOnly dateonly) { }

		[HttpGet("required-dateonly_diff-name")]
		public void RequiredDateOnlyDiffName([FromQuery(Name = "d")] DateOnly dateonly) { }

		[HttpGet("required-datetimeoffset")]
		public void RequiredDateTimeOffset(DateTimeOffset dateTimeOffset) { }

		[HttpGet("required-datetimeoffset_diff-name")]
		public void RequiredDateTimeOffsetDiffName([FromQuery(Name = "d")] DateTimeOffset dateTimeOffset) { }

		[HttpGet("required-enum-parameter")]
		public MyEnum RequiredEnumParameter([FromQuery] MyEnum value) => value;
	}
}