using Albatross.CodeAnalysis;
using Albatross.CodeAnalysis.Testing;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Models;
using Albatross.Testing;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.CodeGen.WebClient.CSharp.UnitTest;

public class TestCreateHttpClientRegistrations {
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
}
""";

	const string Controllers = """
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class FirstController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet("ping")]
	public int Ping() => 1;
}

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class SecondController : Microsoft.AspNetCore.Mvc.ControllerBase {
	[Microsoft.AspNetCore.Mvc.HttpGet("ping")]
	public int Ping() => 1;
}
""";

	[Fact]
	public async Task Generate_ShouldUseInterfaceRegistrations_WhenEnabled() {
		var compilation = await (AspNetCoreStubs + Controllers).CreateNet8CompilationAsync();
		var models = new List<ControllerInfo> {
			new ControllerInfo(compilation, compilation.GetRequiredSymbol("FirstController")),
			new ControllerInfo(compilation, compilation.GetRequiredSymbol("SecondController")),
		};
		var settings = new CSharpWebClientSettings {
			Namespace = "Demo.Client",
			UseInterface = true,
		};

		var generator = new CreateHttpClientRegistrations(new StaticSettingsFactory(settings));
		using var writer = new StringWriter();
		generator.Generate(models).Generate(writer);
		var text = writer.ToString().NormalizeLineEnding()!;

		text.Should().Contain("namespace Demo.Client");
		text.Should().Contain("AddTypedClient<IFirstClient, FirstClient>()");
		text.Should().Contain("AddTypedClient<ISecondClient, SecondClient>()");
	}

	[Fact]
	public async Task Generate_ShouldUseClassOnlyRegistrations_WhenInterfaceDisabled() {
		var compilation = await (AspNetCoreStubs + Controllers).CreateNet8CompilationAsync();
		var models = new List<ControllerInfo> {
			new ControllerInfo(compilation, compilation.GetRequiredSymbol("FirstController")),
		};
		var settings = new CSharpWebClientSettings {
			Namespace = "Demo.Client",
			UseInterface = false,
		};

		var generator = new CreateHttpClientRegistrations(new StaticSettingsFactory(settings));
		using var writer = new StringWriter();
		generator.Generate(models).Generate(writer);
		var text = writer.ToString().NormalizeLineEnding()!;

		text.Should().Contain("AddTypedClient<FirstClient>()");
		text.Should().NotContain("AddTypedClient<IFirstClient, FirstClient>()");
	}
}
