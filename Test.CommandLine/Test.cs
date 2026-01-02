using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using System.CommandLine;
using Test.Dto.Classes;
using Test.Proxy;

namespace Test.CommandLine {
	[Verb<TestCommandHandler>("test")]
	public class TestOptions {
	}
	public class TestCommandHandler : BaseHandler<TestOptions> {
		private readonly FromBodyParamTestProxyService proxy;

		public TestCommandHandler(ParseResult result, FromBodyParamTestProxyService proxy, TestOptions options) : base(result, options) {
			this.proxy = proxy;
		}

		public async override Task<int> InvokeAsync(CancellationToken _) {
			await proxy.RequiredObject(new MyDto());
			return 0;
		}
	}
}