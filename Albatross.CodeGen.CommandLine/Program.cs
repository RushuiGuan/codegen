using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Albatross.CommandLine.Defaults;
using Albatross.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Threading.Tasks;
using Albatross.CodeGen.CommandLine.Parameters;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.CommandLine {
	internal class Program {
		static async Task<int> Main(string[] args) {
			await using var host = new CommandHost("Albatross Code Generator that creates HttpClient proxy for CSharp, TypeScript and Python projects")
				.RegisterServices(RegisterServices)
				.AddCommands()
				.Parse(args)
				.WithDefaults()
				.Build();
			await host.InvokeAsync();
			return 0;
		}

		static void RegisterServices(ParseResult result, IConfiguration configuration, IServiceCollection services) {
			services.RegisterCommands();
			services.AddWebClientCodeGen();
			var key = result.CommandResult.Command.GetCommandKey();
			if (key.StartsWith("python ")) {
				services.AddPythonCodeGen();
				services.AddPythonWebClientCodeGen();
			} else if (key.StartsWith("typescript ")) {
				services.AddTypeScriptCodeGen();
				services.AddTypeScriptWebClientCodeGen();
			} else if (key.StartsWith("csharp ")) {
				services.AddCSharpWebClientCodeGen();
			}
			services.AddShortenLoggerName(false, "Albatross");
			services.AddScoped<LoadCodeGenSettings>();
			services.AddScoped<LoadCSharpProject>();
			services.AddScoped<ICompilationFactory, ProjectCompilationFactory>();
			services.AddScoped<ICodeGenSettingsFactory, CodeGenSettingsFactory>();
		}
	}
}