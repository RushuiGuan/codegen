using Albatross.CodeGen.Python;
using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Albatross.CodeGen.WebClient {
	public static class Extensions {
		public static IServiceCollection AddWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			services.AddScoped<IJsonDerivedTypeIndex, JsonDerivedTypeIndex>();
			return services;
		}
	}
}