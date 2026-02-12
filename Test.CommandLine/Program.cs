using Albatross.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using Test.Proxy;

namespace Test.CommandLine {
	internal class Program {
		static async Task<int> Main(string[] args) {
			await using var host = new CommandHost("test")
				.RegisterServices(RegisterServices)
				.AddCommands()
				.Parse(args)
				.Build();
			return await host.InvokeAsync();
		}

		static void RegisterServices(ParseResult result, IServiceCollection services){
			services.RegisterCommands();
			services.AddTestProxy();
		}
	}
}