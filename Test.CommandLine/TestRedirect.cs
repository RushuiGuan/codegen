using Albatross.CommandLine;
using Test.Proxy;

namespace Test.CommandLine {
	[Verb<TestRedirectCommandHandler>("test-redirect")]
	public class TestRedirectOptions {
		public bool AbsUrl { get; set; }
		public int ActionId { get; set; }
	}

	public class TestRedirectCommandHandler : CommandAction<TestRedirectOptions> {
		private readonly RedirectTestProxyService client;
		private readonly AbsUrlRedirectTestProxyService absClient;

		public TestRedirectCommandHandler(TestRedirectOptions options, RedirectTestProxyService client, AbsUrlRedirectTestProxyService absClient) :base(options){
			this.client = client;
			this.absClient = absClient;
			client.UseTextWriter(System.Console.Out);
		}

		public async override Task<int> Invoke(CancellationToken _) {
			if (options.AbsUrl) {
				await absClient.Get(options.ActionId);
			} else {
				await client.Get(options.ActionId);
			}
			return 0;
		}
	}
}