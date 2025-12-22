using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.TypeScript;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Albatross.CodeGen.WebClient.CSharp {
	/// <summary>
	/// Extension methods for C# web client code generation functionality and dependency injection setup
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Adds C# web client code generation services to the dependency injection container
		/// </summary>
		/// <param name="services">The service collection to add services to</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddCSharpWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
#pragma warning disable CS0612 // Type or member is obsolete
			services.AddSingleton<CreateHttpClientRegistrations>();
#pragma warning restore CS0612 // Type or member is obsolete
			services.AddCSharpCodeGen();
			services.AddSingleton<CreateHttpClientRegistrations>();
			return services;
		}
	}
}