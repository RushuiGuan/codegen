using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Dto.Classes;

namespace Test.WebApi.Controllers {
	[Route("api/nullable-return-type")]
	[ApiController]
	public class NullableReturnTypeTestController : ControllerBase {
		[HttpGet("string")]
		public string? GetString(string? text) => text;

		[HttpGet("async-string")]
		public Task<string?> GetAsyncString(string? text) => Task.FromResult<string?>(text);

		[HttpGet("action-result-string")]
		public ActionResult<string?> GetActionResultString(string? text) => Ok(text);

		[HttpGet("async-action-result-string")]
		public Task<ActionResult<string?>> GetAsyncActionResultString(string? text) => Task.FromResult<ActionResult<string?>>(Ok(text));

		[HttpGet("int")]
		public int? GetInt(int? n) => n;

		[HttpGet("async-int")]
		public Task<int?> GetAsyncInt(int? n) => Task.FromResult<int?>(n);

		[HttpGet("action-result-int")]
		public ActionResult<int?> GetActionResultInt(int? n) => Ok(n);

		[HttpGet("async-action-result-int")]
		public Task<ActionResult<int?>> GetAsyncActionResultInt(int? n) => Task.FromResult<ActionResult<int?>>(Ok(n));

		[HttpGet("datetime")]
		public DateTime? GetDateTime(DateTime? v) => v;

		[HttpGet("async-datetime")]
		public Task<DateTime?> GetAsyncDateTime(DateTime? v) => Task.FromResult<DateTime?>(v);

		[HttpGet("action-result-datetime")]
		public ActionResult<DateTime?> GetActionResultDateTime(DateTime? v) => Ok(v);

		[HttpGet("async-action-result-datetime")]
		public Task<ActionResult<DateTime?>> GetAsyncActionResultDateTime(DateTime? v) => Task.FromResult<ActionResult<DateTime?>>(Ok(v));

		[HttpPost("object")]
		public MyDto? GetMyDto([FromBody] MyDto? value) => value;

		[HttpPost("async-object")]
		public Task<MyDto?> GetAsyncMyDto([FromBody]MyDto? value) => Task.FromResult<MyDto?>(value);

		[HttpPost("action-result-object")]
		public ActionResult<MyDto?> ActionResultObject([FromBody] MyDto? value) {
			return Ok(value);
		}

		[HttpPost("async-action-result-object")]
		public async Task<ActionResult<MyDto?>> AsyncActionResultObject([FromBody] MyDto? value) {
			await Task.Delay(1);
			return Ok(value);
		}

		[HttpPost("nullable-array-return-type")]
		public MyDto?[] GetMyDtoNullableArray([FromBody] MyDto?[] values) => values;

		[HttpPost("nullable-collection-return-type")]
		public IEnumerable<MyDto?> GetMyDtoCollection([FromBody] IEnumerable<MyDto?> values) => values;
	}
}