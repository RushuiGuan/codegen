using Albatross.CodeGen.CSharp.TypeConversions;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Albatross.CodeGen.CSharp {
	public static class Extensions {
		public static IServiceCollection AddCSharpCodeGen(this IServiceCollection services) {
			services.AddSingleton<IConvertObject<ITypeSymbol, ITypeExpression>, DefaultTypeConverter>();
			return services;
		}
	}
}