using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Models;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.UnitTest {
	public class TestParameterInfo {
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
	public class FromQueryAttribute : System.Attribute { public string? Name { get; set; } }
	public class FromRouteAttribute : System.Attribute {}
	public class FromBodyAttribute : System.Attribute {}
	public class FromHeaderAttribute : System.Attribute { public string? Name { get; set; } }
}
""";

		[Fact]
		public async Task ParameterInfo_ShouldResolveBindingSourcesAndDefaults() {
			var code = AspNetCoreStubs + """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class DemoController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet("item/{id:int}/{slug}")]
	public void Get(
		[Microsoft.AspNetCore.Mvc.FromRoute] int id,
		[Microsoft.AspNetCore.Mvc.FromQuery(Name = "q")] string query,
		[Microsoft.AspNetCore.Mvc.FromBody] string body,
		[Microsoft.AspNetCore.Mvc.FromHeader(Name = "X-Token")] string token,
		string slug,
		string other) { }
}
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var controller = compilation.GetRequiredSymbol("DemoController");
			var method = controller.GetMembers().OfType<IMethodSymbol>().Single(x => x.Name == "Get");
			var routeSegments = method.GetRouteText().GetRouteSegments().ToArray();

			var infos = method.Parameters
				.Select(x => new ParameterInfo(compilation, x, routeSegments))
				.ToDictionary(x => x.Name, x => x);

			infos["id"].WebType.Should().Be(ParameterType.FromRoute);
			infos["query"].WebType.Should().Be(ParameterType.FromQuery);
			infos["query"].QueryKey.Should().Be("q");
			infos["body"].WebType.Should().Be(ParameterType.FromBody);
			infos["token"].WebType.Should().Be(ParameterType.FromHeader);
			infos["token"].HeaderKey.Should().Be("X-Token");

			infos["slug"].WebType.Should().Be(ParameterType.FromRoute);
			infos["other"].WebType.Should().Be(ParameterType.FromQuery);
			infos["other"].QueryKey.Should().Be("other");

			var idSegment = routeSegments.OfType<RouteParameterSegment>().Single(x => x.Text == "id");
			idSegment.RequiredParameterInfo.Should().BeSameAs(infos["id"]);

			var slugSegment = routeSegments.OfType<RouteParameterSegment>().Single(x => x.Text == "slug");
			slugSegment.RequiredParameterInfo.Should().BeSameAs(infos["slug"]);
		}
	}
}
