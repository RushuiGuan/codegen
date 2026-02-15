using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.TypeConversions;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.CSharp.UnitTest;

public class TestConvertWebApiToCSharpFile {
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
	[Microsoft.AspNetCore.Mvc.HttpGet("item/{id}/{created}")]
	public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<int>> Get(
		[Microsoft.AspNetCore.Mvc.FromRoute] int id,
		[Microsoft.AspNetCore.Mvc.FromRoute] System.DateTime created,
		[Microsoft.AspNetCore.Mvc.FromQuery(Name = "q")] string query,
		[Microsoft.AspNetCore.Mvc.FromQuery] int page,
		[Microsoft.AspNetCore.Mvc.FromHeader(Name = "X-Token")] string token,
		System.Threading.CancellationToken cancellationToken) => throw null!;

	[Microsoft.AspNetCore.Mvc.HttpPost("save")]
	public Microsoft.AspNetCore.Mvc.IActionResult Save([Microsoft.AspNetCore.Mvc.FromBody] string body) => throw null!;

	[Microsoft.AspNetCore.Mvc.HttpPost("save2")]
	public Microsoft.AspNetCore.Mvc.IActionResult SaveDto([Microsoft.AspNetCore.Mvc.FromBody] DemoDto body) => throw null!;
}

public class DemoDto { public int Id { get; set; } }
""";

	[Fact]
	public async Task Convert_ShouldGenerateInterfaceInternalClassAndRequestBuilders() {
		var compilation = await (AspNetCoreStubs + ControllerCode).CreateNet8CompilationAsync();
		var controllerSymbol = compilation.GetRequiredSymbol("DemoController");
		var model = new ControllerInfo(compilation, controllerSymbol);
		var settings = new CSharpWebClientSettings {
			Namespace = "Demo.Client",
			UseInterface = true,
			UseInternalProxy = true,
			ConstructorSettings = new Dictionary<string, WebClientConstructorSettings> {
				["DemoController"] = new WebClientConstructorSettings {
					CustomJsonSettings = "MyJson.Options"
				}
			}
		};

		var converter = new ConvertWebApiToCSharpFile(
			new CompilationFactory(compilation),
			new StaticSettingsFactory(settings),
			new DefaultTypeConverter());
		var file = converter.Convert(model);
		var text = file.Render();

		text.Should().Contain("namespace Demo.Client");
		text.Should().Contain("public partial interface IDemoClient");
		text.Should().Contain("internal partial class DemoClient : IDemoClient");
		text.Should().Contain("public DemoClient(HttpClient client)");
		text.Should().Contain("this.jsonSerializerOptions = MyJson.Options;");

		text.Should().Contain("created.ISO8601()");
		text.Should().Contain("builder.AddQueryStringIfSet(\"q\", query);");
		text.Should().Contain("builder.AddQueryString(\"page\", page);");
		text.Should().Contain("public async Task<int> Get(int id, System.DateTime created, string query, int page, string token, CancellationToken cancellationToken)");

		text.Should().Contain("builder.CreateStringRequest(body);");
		text.Should().Contain("builder.CreateJsonRequest<DemoDto>(body);");
	}

	[Fact]
	public async Task Convert_ShouldOmitConstructor_WhenConfigured() {
		var compilation = await (AspNetCoreStubs + ControllerCode).CreateNet8CompilationAsync();
		var controllerSymbol = compilation.GetRequiredSymbol("DemoController");
		var model = new ControllerInfo(compilation, controllerSymbol);
		var settings = new CSharpWebClientSettings {
			Namespace = "Demo.Client",
			ConstructorSettings = new Dictionary<string, WebClientConstructorSettings> {
				["DemoController"] = new WebClientConstructorSettings {
					Omit = true
				}
			}
		};

		var converter = new ConvertWebApiToCSharpFile(
			new CompilationFactory(compilation),
			new StaticSettingsFactory(settings),
			new DefaultTypeConverter());
		var file = converter.Convert(model);
		var text = file.Render();

		text.Should().NotContain("DemoClient(HttpClient client)");
	}
}
