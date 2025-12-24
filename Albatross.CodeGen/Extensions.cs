using Albatross.Reflection;
using Albatross.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen {
	/// <summary>
	/// Provides extension methods for code generation functionality and dependency injection setup
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Adds code generation services from the specified assembly to the service collection
		/// </summary>
		/// <param name="services">The service collection to add services to</param>
		/// <param name="assembly">The assembly containing converter types</param>
		/// <returns>The service collection for method chaining</returns>
		public static IServiceCollection AddCodeGen(this IServiceCollection services, Assembly assembly) {
			foreach (var type in assembly.GetTypes()) {
				TryAddConverter(services, type);
			}
			return services;
		}

		/// <summary>
		/// Attempts to register a type as a converter service if it implements IConvertObject interfaces
		/// </summary>
		/// <param name="services">The service collection to add the converter to</param>
		/// <param name="converterType">The type to register as a converter</param>
		/// <returns>True if the type was successfully registered as a converter; otherwise, false</returns>
		public static bool TryAddConverter(this IServiceCollection services, Type converterType) {
			if (converterType.TryGetClosedGenericType(typeof(IConvertObject<>), out Type genericInterfaceType)) {
				services.AddTransient(converterType);
				services.AddTransient(genericInterfaceType, converterType);
				if (converterType.TryGetClosedGenericType(typeof(IConvertObject<,>), out genericInterfaceType)) {
					services.AddTransient(genericInterfaceType, converterType);
				}
				return true;
			} else {
				return false;
			}
		}
		/// <summary>
		/// Creates a new code generator scope with automatic brace handling for block structures
		/// </summary>
		/// <param name="writer">The TextWriter to create the scope for</param>
		/// <param name="text">Optional text to write before the opening brace</param>
		/// <returns>A disposable CodeGeneratorScope that automatically handles indentation and braces</returns>
		public static CodeGeneratorScope BeginScope(this TextWriter writer) => writer.BeginScope("{", "}");

		public static CodeGeneratorScope BeginScope(this TextWriter writer, string open, string close) {
			writer.Space();
			return new CodeGeneratorScope(writer, args => args.AppendLine(open), args => args.Append(close));
		}

		/// <summary>
		/// Creates a new code generator scope with automatic indentation but no braces
		/// </summary>
		/// <param name="writer">The TextWriter to create the scope for</param>
		/// <param name="text">Optional text to write at the beginning of the scope</param>
		/// <returns>A disposable CodeGeneratorScope that handles indentation</returns>
		public static CodeGeneratorScope BeginIndentScope(this TextWriter writer, string? text = null) {
			return new CodeGeneratorScope(writer, args => args.AppendLine($"{text}"), _ => { });
		}

		/// <summary>
		/// Chains member access expressions to create method or property access chains (e.g., obj.method1().property.method2())
		/// </summary>
		/// <param name="expression">The base expression to chain from</param>
		/// <param name="lineBreak">Whether to include line breaks between chained members</param>
		/// <param name="members">The member expressions to chain</param>
		/// <returns>A MemberAccessExpression representing the chained access</returns>
		public static IExpression Chain(this IExpression expression, bool lineBreak, params IEnumerable<IExpression> members)
			=> new MemberAccessExpression(expression, lineBreak, members);
	}
}