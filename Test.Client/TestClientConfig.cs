using Albatross.Config;
using Microsoft.Extensions.Configuration;

namespace Test.Client {
	public class TestClientConfig : ConfigBase {
		public TestClientConfig(IConfiguration configuration) : base(configuration, null) {
			this.EndPoint = configuration.GetRequiredEndPoint("test");
		}
		public string EndPoint { get; }
	}
}