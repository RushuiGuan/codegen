using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Models;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.UnitTest {
	public class TestMethodInfo {
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
	public class HttpPutAttribute : Routing.HttpMethodAttribute { public HttpPutAttribute(string template = "") : base(template) {} }
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
		public async Task MethodInfo_ShouldMapHttpMethodParametersAndFlags() {
			var code = AspNetCoreStubs + """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class SampleController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpPut("item/{id}")]
	[Microsoft.AspNetCore.Authorization.Authorize]
	[System.Obsolete]
	public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<int>> Update(
		[Microsoft.AspNetCore.Mvc.FromRoute] int id,
		[Microsoft.AspNetCore.Mvc.FromQuery(Name = "q")] string query,
		[Microsoft.AspNetCore.Mvc.FromHeader(Name = "X-Token")] string token,
		System.Threading.CancellationToken cancellationToken) => throw null!;
}
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var controller = compilation.GetRequiredSymbol("SampleController");
			var symbol = controller.GetMembers().OfType<IMethodSymbol>().Single(x => x.Name == "Update");
			var model = new MethodInfo(compilation, symbol);

			model.HttpMethod.Should().Be("Put");
			model.ReturnTypeText.Should().Be("System.Int32");
			model.CanCancel.Should().BeTrue();
			model.RequiresAuthentication.Should().BeTrue();
			model.IsObsolete.Should().BeTrue();
			model.HasQueryStringParameter.Should().BeTrue();
			model.Parameters.Select(x => x.Name).Should().BeEquivalentTo(["id", "query", "token"]);
			model.Parameters.Single(x => x.Name == "id").WebType.Should().Be(ParameterType.FromRoute);
			model.Parameters.Single(x => x.Name == "query").WebType.Should().Be(ParameterType.FromQuery);
			model.Parameters.Single(x => x.Name == "token").WebType.Should().Be(ParameterType.FromHeader);
			model.Parameters.Single(x => x.Name == "token").HeaderKey.Should().Be("X-Token");
		}

		[Theory]
		[InlineData("NoContent", "System.Void")]
		[InlineData("PlainActionResult", "System.Void")]
		[InlineData("PlainInterfaceResult", "System.Void")]
		[InlineData("Stream", "System.Collections.Generic.IEnumerable<System.String>")]
		[InlineData("TaskOfDto", "Dto")]
		public async Task MethodInfo_ShouldResolveReturnTypes(string methodName, string expectedReturnType) {
			var code = AspNetCoreStubs + """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class SampleController : Microsoft.AspNetCore.Mvc.ControllerBase {
	public class Dto {}

	[Microsoft.AspNetCore.Mvc.HttpGet]
	public System.Threading.Tasks.Task NoContent() => throw null!;
	[Microsoft.AspNetCore.Mvc.HttpGet]
	public Microsoft.AspNetCore.Mvc.ActionResult PlainActionResult() => throw null!;
	[Microsoft.AspNetCore.Mvc.HttpGet]
	public Microsoft.AspNetCore.Mvc.IActionResult PlainInterfaceResult() => throw null!;
	[Microsoft.AspNetCore.Mvc.HttpGet]
	public System.Collections.Generic.IAsyncEnumerable<string> Stream() => throw null!;
	[Microsoft.AspNetCore.Mvc.HttpGet]
	public System.Threading.Tasks.Task<Dto> TaskOfDto() => throw null!;
}
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var controller = compilation.GetRequiredSymbol("SampleController");
			var symbol = controller.GetMembers().OfType<IMethodSymbol>().Single(x => x.Name == methodName);
			var model = new MethodInfo(compilation, symbol);

			model.ReturnTypeText.Should().Be(expectedReturnType);
		}
	}
}
