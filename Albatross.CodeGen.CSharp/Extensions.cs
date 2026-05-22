using Albatross.CodeGen.CSharp.TypeConversions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Albatross.CodeGen.CSharp {
	/// <summary>
	/// Extension methods for configuring C# code generation services
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Adds C# code generation services to the dependency injection container
		/// </summary>
		/// <param name="services">The service collection to add services to</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddCSharpCodeGen(this IServiceCollection services, Dictionary<string, string> customTypeMapping) {
			services.AddSingleton<IConvertObject<ITypeSymbol, ITypeExpression>>(new DefaultTypeConverter(customTypeMapping));
			return services;
		}
	}
}