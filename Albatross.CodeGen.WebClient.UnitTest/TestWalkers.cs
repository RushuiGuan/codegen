using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.Settings;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.UnitTest {
	public class TestWalkers {
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
	public class FromHeaderAttribute : System.Attribute {}
	public class ActionResult {}
	public class ActionResult<T> {}
	public interface IActionResult {}
}
namespace Microsoft.AspNetCore.Authorization {
	public class AuthorizeAttribute : System.Attribute {}
}
""";

		[Fact]
		public async Task ApiControllerClassWalker_ShouldFindControllerBaseSubclasses() {
			var code = AspNetCoreStubs + """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class GoodController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet]
	public int Ping() => 1;
}
public class NotAController { }
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var syntaxTree = compilation.SyntaxTrees.First();
			var semanticModel = compilation.GetSemanticModel(syntaxTree);
			var filters = new[] {
				new Albatross.CodeGen.WebClient.Settings.SymbolFilter(new SymbolFilterPatterns { Include = "GoodController$" })
			};

			var walker = new ApiControllerClassWalker(semanticModel, filters);
			walker.Visit(syntaxTree.GetRoot());

			walker.Result.Should().ContainSingle();
			walker.Result[0].Name.Should().Be("GoodController");
		}

		[Fact]
		public async Task DtoClassEnumWalker_ShouldCollectPublicDtosAndEnums() {
			var code = AspNetCoreStubs + """
public class PublicDto { public int Id { get; set; } }
internal class InternalDto { public int Id { get; set; } }
public abstract class AbstractDto { public int Id { get; set; } }
public class GenericDto<T> { public T? Value { get; set; } }
public interface IContract {}
public record PublicRecord(int Id);
public enum Status { New = 1, Done = 2 }
""";
			var compilation = await code.CreateNet8CompilationAsync();
			var syntaxTree = compilation.SyntaxTrees.First();
			var semanticModel = compilation.GetSemanticModel(syntaxTree);
			var filters = new[] {
				new Albatross.CodeGen.WebClient.Settings.SymbolFilter(new SymbolFilterPatterns {
					Exclude = ".*",
					Include = "^(PublicDto|PublicRecord|Status)$"
				})
			};

			var walker = new DtoClassEnumWalker(semanticModel, filters);
			walker.Visit(syntaxTree.GetRoot());

			walker.DtoClasses.Select(x => x.Name).Should().BeEquivalentTo(["PublicDto", "PublicRecord"]);
			walker.EnumTypes.Select(x => x.Name).Should().BeEquivalentTo(["Status"]);
		}
	}
}
