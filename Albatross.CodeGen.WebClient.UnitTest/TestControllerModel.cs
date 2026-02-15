using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.UnitTest {
	public class TestControllerModel {
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
namespace Microsoft.AspNetCore.Authorization {
	public class AuthorizeAttribute : System.Attribute {}
}
""";

		[Fact]
		public async Task ControllerInfo_ShouldMapRouteMethodsAndParameters() {
			var code = AspNetCoreStubs + """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[System.Obsolete]
[Microsoft.AspNetCore.Authorization.Authorize]
public class SampleController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet("item/{id}")]
	[Microsoft.AspNetCore.Authorization.Authorize]
	public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<int>> Get(
		[Microsoft.AspNetCore.Mvc.FromRoute] int id,
		[Microsoft.AspNetCore.Mvc.FromQuery(Name = "q")] string query,
		[Microsoft.AspNetCore.Mvc.FromHeader(Name = "X-Token")] string token,
		System.Threading.CancellationToken cancellationToken) => throw null!;

	[Microsoft.AspNetCore.Mvc.HttpPost("save")]
	public Microsoft.AspNetCore.Mvc.IActionResult Save(
		[Microsoft.AspNetCore.Mvc.FromBody] string body) => throw null!;
}
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var symbol = compilation.GetRequiredSymbol("SampleController");
			var model = new ControllerInfo(compilation, symbol);

			model.ControllerName.Should().Be("Sample");
			model.Route.Should().Be("api/sample");
			model.IsObsolete.Should().BeTrue();
			model.RequiresAuthentication.Should().BeTrue();
			model.Methods.Should().HaveCount(2);

			var getMethod = model.Methods.Single(x => x.Name == "Get");
			getMethod.HttpMethod.Should().Be("Get");
			getMethod.ReturnTypeText.Should().Be("System.Int32");
			getMethod.CanCancel.Should().BeTrue();
			getMethod.RequiresAuthentication.Should().BeTrue();
			getMethod.Parameters.Should().HaveCount(3);
			getMethod.HasQueryStringParameter.Should().BeTrue();
			getMethod.Parameters.Single(x => x.Name == "id").WebType.Should().Be(ParameterType.FromRoute);
			getMethod.Parameters.Single(x => x.Name == "query").QueryKey.Should().Be("q");
			getMethod.Parameters.Single(x => x.Name == "token").WebType.Should().Be(ParameterType.FromHeader);
			getMethod.Parameters.Single(x => x.Name == "token").HeaderKey.Should().Be("X-Token");

			var saveMethod = model.Methods.Single(x => x.Name == "Save");
			saveMethod.ReturnTypeText.Should().Be("System.Void");
			saveMethod.Parameters.Should().ContainSingle(x => x.WebType == ParameterType.FromBody);
		}

		[Fact]
		public async Task ControllerInfo_ApplyMethodFilters_ShouldRemoveFilteredMethods() {
			var code = AspNetCoreStubs + """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class SampleController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet("one")]
	public int One() => 1;
	[Microsoft.AspNetCore.Mvc.HttpGet("two")]
	public int Two() => 2;
}
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var symbol = compilation.GetRequiredSymbol("SampleController");
			var model = new ControllerInfo(compilation, symbol);
			var filters = new[] {
				new SymbolFilter(new SymbolFilterPatterns { Exclude = "SampleController\\.Two$" })
			};

			model.ApplyMethodFilters(filters);

			model.Methods.Select(x => x.Name).Should().BeEquivalentTo(["One"]);
		}
	}
}
