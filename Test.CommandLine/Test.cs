using Albatross.CommandLine;
using Test.Dto.Classes;
using Test.Proxy;

namespace Test.CommandLine {
	[Verb<TestCommandHandler>("test")]
	public class TestOptions {
	}
	public class TestCommandHandler : CommandAction<TestOptions> {
		private readonly FromBodyParamTestProxyService proxy;

		public TestCommandHandler(FromBodyParamTestProxyService proxy, TestOptions options):base(options) {
			this.proxy = proxy;
		}

		public async override Task<int> Invoke(CancellationToken _) {
			await proxy.RequiredObject(new MyDto());
			return 0;
		}
	}
}