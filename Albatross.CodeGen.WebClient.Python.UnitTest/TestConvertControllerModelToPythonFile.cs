using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Python;
using Albatross.Testing;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.Python.UnitTest;

public class TestConvertControllerModelToPythonFile {
	const string AspNetCoreStubs = """
namespace Microsoft.AspNetCore.Mvc.Routing {
	public abstract class HttpMethodAttribute : System.Attribute {
		public HttpMethodAttribute(string template = "") { }
	}
}
namespace Microsoft.AspNetCore.Mvc {
	public class ControllerBase {}
	public class RouteAttribute : System.Attribute { public RouteAttribute(string template) {} }
	public class HttpGetAttribute : Routing.HttpMethodAttribute { public HttpGetAttribute(string template = "") : base(template) {} }
	public class HttpPostAttribute : Routing.HttpMethodAttribute { public HttpPostAttribute(string template = "") : base(template) {} }
	public class FromQueryAttribute : System.Attribute { public string? Name { get; set; } }
	public class FromRouteAttribute : System.Attribute {}
	public class FromBodyAttribute : System.Attribute {}
	public class FromHeaderAttribute : System.Attribute { public string? Name { get; set; } }
	public class ActionResult {}
	public class ActionResult<T> {}
	public interface IActionResult {}
}
""";

	const string ControllerCode = """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class DemoController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet("item/{id}")]
	public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<int>> Get(
		[Microsoft.AspNetCore.Mvc.FromRoute] int id,
		[Microsoft.AspNetCore.Mvc.FromQuery(Name = "q")] string query) => throw null!;

	[Microsoft.AspNetCore.Mvc.HttpPost("save")]
	public Microsoft.AspNetCore.Mvc.IActionResult Save([Microsoft.AspNetCore.Mvc.FromBody] string body) => throw null!;
}
""";

	[Fact]
	public async Task Convert_ShouldGenerateAsyncHttpxClient_WhenAsyncEnabled() {
		var compilation = await (AspNetCoreStubs + ControllerCode).CreateNet8CompilationAsync();
		var model = new ControllerInfo(compilation, compilation.GetRequiredSymbol("DemoController"));
		var settings = new PythonWebClientSettings {
			Async = true,
			HttpClientClassNameMapping = new Dictionary<string, string> {
				["DemoController"] = "DemoApiClient"
			}
		};
		var typeConverter = TypeConverterFactory.Build(compilation, settings);
		var converter = new ConvertControllerModelToPythonFile(
			new CompilationFactory(compilation),
			new StaticSettingsFactory(settings),
			typeConverter);

		var file = converter.Convert(model);
		using var writer = new StringWriter();
		file.Generate(writer);
		var text = writer.ToString().NormalizeLineEnding()!;

		text.Should().Contain("class DemoApiClient:");
		text.Should().Contain("from httpx import AsyncClient, Auth");
		text.Should().Contain("self._client = AsyncClient(base_url = self._base_url, auth = auth)");
		text.Should().Contain("async def get(self, id: int, query: str) -> int:");
		text.Should().Contain("response = await self._client.get(request_url, params = params)");
		text.Should().Contain("async def save(self, body: str) -> None:");
		text.Should().Contain("response = await self._client.post(request_url, headers = {\"Content-Type\": \"text/plain\"}, content = body)");
	}

	[Fact]
	public async Task Convert_ShouldGenerateSyncRequestsClient_WhenAsyncDisabled() {
		var compilation = await (AspNetCoreStubs + ControllerCode).CreateNet8CompilationAsync();
		var model = new ControllerInfo(compilation, compilation.GetRequiredSymbol("DemoController"));
		var settings = new PythonWebClientSettings {
			Async = false,
		};
		var typeConverter = TypeConverterFactory.Build(compilation, settings);
		var converter = new ConvertControllerModelToPythonFile(
			new CompilationFactory(compilation),
			new StaticSettingsFactory(settings),
			typeConverter);

		using var writer = new StringWriter();
		converter.Convert(model).Generate(writer);
		var text = writer.ToString().NormalizeLineEnding()!;

		text.Should().Contain("from requests import Session");
		text.Should().Contain("from requests.auth import AuthBase");
		text.Should().Contain("self._client = Session()");
		text.Should().Contain("self._client.auth = auth");
		text.Should().Contain("request_url = f\"{self._base_url}/item/{id}\"");
		text.Should().Contain("response = self._client.get(request_url, params = params)");
		text.Should().Contain("response = self._client.post(request_url, headers = {\"Content-Type\": \"text/plain\"}, data = body)");
	}
}
