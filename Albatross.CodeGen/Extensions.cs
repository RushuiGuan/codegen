using Albatross.Reflection;
using Albatross.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Albatross.CodeGen {
	public static class Extensions {
		public static IServiceCollection AddCodeGen(this IServiceCollection services, Assembly assembly) {
			foreach (var type in assembly.GetTypes()) {
				TryAddConverter(services, type);
			}
			return services;
		}

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
		public static CodeGeneratorScope BeginScope(this TextWriter writer, string? text = null) {
			return new CodeGeneratorScope(writer, args => args.AppendLine($"{text} {{"), args => args.Append("}"));
		}
		public static CodeGeneratorScope BeginIndentScope(this TextWriter writer, string? text = null) {
			return new CodeGeneratorScope(writer, args => args.AppendLine($"{text}"), _ => { });
		}

		public static IExpression Chain(this IExpression expression, bool lineBreak, params IEnumerable<IExpression> members)
			=> new MemberAccessExpression(expression, lineBreak, members);
	}
}