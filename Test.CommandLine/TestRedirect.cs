using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using System.CommandLine;
using Test.Proxy;

namespace Test.CommandLine {
	[Verb<TestRedirectCommandHandler>("test-redirect")]
	public class TestRedirectOptions {
		public bool AbsUrl { get; set; }
		public int ActionId { get; set; }
	}

	public class TestRedirectCommandHandler : BaseHandler<TestRedirectOptions> {
		private readonly RedirectTestProxyService client;
		private readonly AbsUrlRedirectTestProxyService absClient;

		public TestRedirectCommandHandler(ParseResult result, TestRedirectOptions options, RedirectTestProxyService client, AbsUrlRedirectTestProxyService absClient) :base(result, options){
			this.client = client;
			this.absClient = absClient;
			client.UseTextWriter(System.Console.Out);
		}

		public async override Task<int> InvokeAsync(CancellationToken _) {
			if (parameters.AbsUrl) {
				await absClient.Get(parameters.ActionId);
			} else {
				await client.Get(parameters.ActionId);
			}
			return 0;
		}
	}
}