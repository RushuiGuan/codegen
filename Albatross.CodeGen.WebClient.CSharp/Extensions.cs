using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Albatross.CodeGen.WebClient.CSharp {
	public static class Extensions {
		public static IServiceCollection AddCSharpWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
#pragma warning disable CS0612 // Type or member is obsolete
			services.AddSingleton<CreateHttpClientRegistrations>();
#pragma warning restore CS0612 // Type or member is obsolete
			services.AddCSharpCodeGen();
			services.AddSingleton<CreateHttpClientRegistrations2>();
			return services;
		}
	}
}