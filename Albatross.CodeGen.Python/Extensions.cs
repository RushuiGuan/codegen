using Albatross.CodeGen.Python.TypeConversions;
using Albatross.Reflection;
using Albatross.Text;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen.Python {
	/// <summary>
	/// Extension methods for Python code generation functionality and dependency injection setup
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Creates a Python-specific code generator scope with colon syntax for block structures
		/// </summary>
		/// <param name="writer">The TextWriter to create the scope for</param>
		/// <returns>A disposable CodeGeneratorScope configured for Python block syntax</returns>
		public static CodeGeneratorScope BeginPythonScope(this TextWriter writer) {
			return new CodeGeneratorScope(writer, args => args.AppendLine(":"), args => { });
		}

		/// <summary>
		/// Creates a Python-specific code generator scope with custom opening and closing braces
		/// </summary>
		/// <param name="writer">The TextWriter to create the scope for</param>
		/// <param name="openBrace">The opening brace or delimiter</param>
		/// <param name="closeBrace">The closing brace or delimiter</param>
		/// <returns>A disposable CodeGeneratorScope with custom delimiters</returns>
		public static CodeGeneratorScope BeginPythonLineBreak(this TextWriter writer, string openBrace, string closeBrace) {
			return new CodeGeneratorScope(writer, args => args.AppendLine(openBrace), args => args.Append(closeBrace));
		}

		/// <summary>
		/// Adds Python code generation services to the dependency injection container
		/// </summary>
		/// <param name="services">The service collection to add services to</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddPythonCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			services.AddScoped<IConvertObject<ITypeSymbol, ITypeExpression>, ConvertType>();
			services.AddTypeConverters(typeof(ConvertType).Assembly);
			return services;
		}
		
		/// <summary>
		/// Adds type converter services from the specified assembly to the service collection
		/// </summary>
		/// <param name="services">The service collection to add converters to</param>
		/// <param name="assembly">The assembly to scan for type converter implementations</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddTypeConverters(this IServiceCollection services, Assembly assembly) {
			foreach (var type in assembly.GetTypes()) {
				if (type.IsConcreteType() && type.IsDerived<ITypeConverter>()) {
					services.TryAddEnumerable(ServiceDescriptor.Scoped(typeof(ITypeConverter), type));
				}
			}
			return services;
		}
	}
}