using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.TypeScript.TypeConversions;
using Albatross.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Reflection;

namespace Albatross.CodeGen.TypeScript {
	/// <summary>
	/// Extension methods for TypeScript code generation functionality and dependency injection setup
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Adds TypeScript code generation services to the dependency injection container
		/// </summary>
		/// <param name="services">The service collection to add services to</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddTypeScriptCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(Extensions).Assembly);
			services.AddScoped<IConvertObject<ITypeSymbol, ITypeExpression>, ConvertType>();
			services.AddTypeConverters(typeof(ConvertType).Assembly);
			return services;
		}


		public static IServiceCollection AddTypeConverters(this IServiceCollection services, Assembly assembly) {
			foreach (var type in assembly.GetTypes()) {
				if (type.IsConcreteType() && type.IsDerived<ITypeConverter>()) {
					services.TryAddEnumerable(ServiceDescriptor.Scoped(typeof(ITypeConverter), type));
				}
			}
			return services;
		}

		public static bool IsPromise(this ITypeExpression type) {
			return type is GenericTypeExpression generic && object.Equals(generic.Identifier, Defined.Identifiers.Promise);
		}

		public static bool IsObservable(this ITypeExpression type) {
			return type is GenericTypeExpression generic && object.Equals(generic.Identifier, Defined.Identifiers.Observable);
		}

		public static ITypeExpression ToPromise(this ITypeExpression type) {
			if (!type.IsPromise()) {
				type = new GenericTypeExpression(Defined.Identifiers.Promise) {
					Arguments = new ListOfNodes<ITypeExpression> {
						type
					}
				};
			}
			return type;
		}

		public static ITypeExpression ToObservable(this ITypeExpression type) {
			if (!type.IsObservable()) {
				type = new GenericTypeExpression(Defined.Identifiers.Observable) {
					Arguments = new ListOfNodes<ITypeExpression> {
						type
					}
				};
			}
			return type;
		}

		/// <summary>
		/// Parse a string into an IdentifierNameExpression, a fully qualified name can be constructed by using the format name,soure
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static IIdentifierNameExpression ParseIdentifierName(this string name) {
			name = name.Trim();
			var index = name.IndexOf(',');
			var source = string.Empty;
			if (index > 0) {
				source = name.Substring(index + 1).Trim();
				name = name.Substring(0, index).Trim();
			}
			if (string.IsNullOrEmpty(name)) {
				throw new ArgumentException($"{name} is not valid identifier name");
			}
			if (string.IsNullOrEmpty(source)) {
				return new IdentifierNameExpression(name);
			} else {
				return new QualifiedIdentifierNameExpression(name, new GenericSourceExpression(source));
			}
		}
	}
}
