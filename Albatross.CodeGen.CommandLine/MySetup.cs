using Albatross.CodeGen.Python;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Albatross.Config;
using Albatross.Logging;
using Albatross.Serialization.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.CommandLine;
using System.Text.Json;

namespace Albatross.CodeGen.CommandLine {
	public class MySetup : Setup {
		public MySetup() : base("Albatross Code Generator that creates HttpClient proxy for CSharp, TypeScript and Python projects") { }
		protected override void RegisterServices(ParseResult result, IConfiguration configuration, EnvironmentSetting envSetting, IServiceCollection services) {
			base.RegisterServices(result, configuration, envSetting, services);
			services.RegisterCommands();
			var key = result.CommandResult.Command.GetCommandKey();
			services.AddWebClientCodeGen();
			if (key.StartsWith("python")) {
				services.AddPythonCodeGen();
				services.AddPythonWebClientCodeGen();
			} else if (key.StartsWith("typescript")) {
				services.AddTypeScriptCodeGen();
				services.AddTypeScriptWebClientCodeGen();
			} else if (key.StartsWith("csharp ")) {
				services.AddCSharpWebClientCodeGen();
			} else if (key.StartsWith("csharp2")) {
				services.AddCSharpWebClientCodeGen();
			}
			services.AddShortenLoggerName(false, "Albatross");
			services.AddSingleton<IProjectCompilationFactory, ProjectCompilationFactory>();
			services.AddSingleton<Compilation>(provider => provider.GetRequiredService<IProjectCompilationFactory>().Create().Result);
			RegisterCodeGenSettings<CSharpWebClientSettings>(services);
			RegisterCodeGenSettings<TypeScriptWebClientSettings>(services);
			RegisterCodeGenSettings<PythonWebClientSettings>(services);
			RegisterCodeGenSettings<CodeGenSettings>(services);
		}

		static IServiceCollection RegisterCodeGenSettings<T>(IServiceCollection services) where T : CodeGenSettings, new() {
			services.AddSingleton(provider => {
				var options = provider.GetRequiredService<CodeGenCommandOptions>();
				if (options.SettingsFile == null) {
					return new T();
				} else if (options.SettingsFile.Exists) {
					using var stream = options.SettingsFile.OpenRead();
					var settings = JsonSerializer.Deserialize<T>(stream, DefaultJsonSettings.Instance.Value) ?? throw new ArgumentException("Unable to deserialize codegen settings");
					return settings;
				} else {
					throw new InvalidOperationException($"File {options.SettingsFile.Name} doesn't exist");
				}
			});

			return services;
		}
	}
}