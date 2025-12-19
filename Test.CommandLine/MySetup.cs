using Albatross.CommandLine;
using Albatross.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using Test.Proxy;

namespace Test.CommandLine {
	public class MySetup : Setup {
		public MySetup(): base("test commandline") {
		}

		protected override void RegisterServices(ParseResult result, IConfiguration configuration, EnvironmentSetting envSetting, IServiceCollection services) {
			base.RegisterServices(result, configuration, envSetting, services);
			services.AddTestProxy();
			services.RegisterCommands();
		}
	}
}