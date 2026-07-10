using Albatross.Hosting;
using System;
using System.Threading.Tasks;

namespace Test.WebApi {
	internal class Program {
		public static Task Main(string[] args) {
			Albatross.Logging.Extensions.RemoveLegacySlackSinkOptions();
			return new Setup(args, AppContext.BaseDirectory)
				.ConfigureWebHost<MyStartup>()
				.RunAsync();
		}
	}
}