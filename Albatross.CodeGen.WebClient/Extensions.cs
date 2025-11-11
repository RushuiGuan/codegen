using Albatross.CodeGen.Python;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Albatross.CodeGen.WebClient {
	public static class Extensions {
		public static IServiceCollection AddWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			return services;
		}

		public static IServiceCollection AddCSharpWebClientCodeGen(this IServiceCollection services) {
			services.AddSingleton<CreateHttpClientRegistrations>();
			return services;
		}

		public static IServiceCollection AddTypeScriptWebClientCodeGen(this IServiceCollection services) {
			services.AddSingleton<ITypeConverter, TypeScript.MappedTypeConverter>();
			services.AddSingleton<ISourceLookup>(provider => {
				var settings = provider.GetRequiredService<CodeGenSettings>();
				return new DefaultTypeScriptSourceLookup(settings.TypeScriptWebClientSettings.NameSpaceModuleMapping);
			});
			return services;
		}

		public static IServiceCollection AddPythonWebClientCodeGen(this IServiceCollection services) {
			services.AddSingleton<ITypeConverter, Python.MappedTypeConverter>();
			services.AddSingleton<ISourceLookup>(provider => {
				var settings = provider.GetRequiredService<CodeGenSettings>();
				return new DefaultPythonSourceLookup(settings.PythonWebClientSettings.NameSpaceModuleMapping);
			});
			return services;
		}
	}
}