using Albatross.CodeGen.Python.TypeConversions;
using Albatross.CodeGen.Syntax;
using Albatross.Reflection;
using Albatross.Text;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen.Python {
	public static class Extensions {
		public static CodeGeneratorScope BeginPythonScope(this TextWriter writer) {
			return new CodeGeneratorScope(writer, args => args.AppendLine(":"), args => { });
		}
		
		public static CodeGeneratorScope BeginPythonLineBreak(this TextWriter writer, string openBrace, string closeBrace) {
			return new CodeGeneratorScope(writer, args => args.AppendLine(openBrace), args => args.Append(closeBrace));
		}

		public static IServiceCollection AddPythonCodeGen(this IServiceCollection services) {
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
	}
}