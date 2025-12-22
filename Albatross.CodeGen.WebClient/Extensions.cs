using Albatross.CodeGen.WebClient.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Albatross.CodeGen.WebClient {
	/// <summary>
	/// Extension methods for web client code generation functionality and dependency injection setup
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Adds web client code generation services to the dependency injection container
		/// </summary>
		/// <param name="services">The service collection to add services to</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddWebClientCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			services.AddScoped<IJsonDerivedTypeIndex, JsonDerivedTypeIndex>();
			return services;
		}
	}
}