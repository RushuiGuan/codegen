using Albatross.CodeGen.CSharp;
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
			services.AddCSharpCodeGen();
			services.AddScoped<CreateHttpClientRegistrations>();
			return services;
		}
	}
}