using Albatross.CodeGen.Python;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Albatross.Config;
using Albatross.Logging;
using Albatross.Serialization.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.CommandLine.Invocation;
using System.Text.Json;

namespace Albatross.CodeGen.CommandLine {
	public class MySetup : Setup {
		protected override string RootCommandDescription => "Albatross Code Generator that creates HttpClient proxy for CSharp, TypeScript and Python projects";
		public override void RegisterServices(InvocationContext context, IConfiguration configuration, EnvironmentSetting envSetting, IServiceCollection services) {
			base.RegisterServices(context, configuration, envSetting, services);
			services.RegisterCommands();
			services.AddWebClientCodeGen();
			if(context.ParseResult.CommandResult.Parent?.Symbol.Name == "python") {
				services.AddPythonCodeGen();
				services.AddPythonWebClientCodeGen();
			} else if(context.ParseResult.CommandResult.Parent?.Symbol.Name == "typescript") {
				services.AddTypeScriptCodeGen();
				services.AddTypeScriptWebClientCodeGen();
			} else if (context.ParseResult.CommandResult.Parent?.Symbol.Name == "csharp") {
				services.AddCSharpWebClientCodeGen();
			}else if (context.ParseResult.CommandResult.Parent?.Symbol.Name == "csharp2") {
				services.AddCSharpWebClientCodeGen();
			}
			services.AddScoped<Compilation>(provider => {
				var options = provider.GetRequiredService<IOptions<CodeGenCommandOptions>>().Value;
				if (options.ProjectFile.Exists) {
					var workspace = MSBuildWorkspace.Create();
					var project = workspace.OpenProjectAsync(options.ProjectFile.FullName).Result;
					var compilation = project.GetCompilationAsync().Result;
					return compilation;
				} else {
					throw new InvalidOperationException($"File {options.ProjectFile.Name} doesn't exist");
				}
			});
			services.AddShortenLoggerName(false, "Albatross");
			services.AddSingleton(provider => {
				var options = provider.GetRequiredService<IOptions<CodeGenCommandOptions>>().Value;
				if (options.SettingsFile == null) {
					return new CodeGenSettings();
				} else if (options.SettingsFile.Exists) {
					using var stream = options.SettingsFile.OpenRead();
					var settings = JsonSerializer.Deserialize<CodeGenSettings>(stream, DefaultJsonSettings.Instance.Value) ?? throw new ArgumentException("Unable to deserialize codegen settings");
					return settings;
				} else {
					throw new InvalidOperationException($"File {options.SettingsFile.Name} doesn't exist");
				}
			});
		}
	}
}