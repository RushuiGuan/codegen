using Albatross.CodeGen.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen {
	public static class ServiceExtension {
		public static IServiceCollection AddCodeGen(this IServiceCollection services, Assembly assembly) {
			Type genericInterfaceType;
			foreach (var type in assembly.GetTypes()) {
				if (type.TryGetGenericInterfaceType(typeof(IConvertObject<>), out genericInterfaceType)){
					services.AddTransient(type);
					//register any close implementation of IConvertObject<>
					services.AddTransient(genericInterfaceType, type);
					if (type.TryGetGenericInterfaceType(typeof(IConvertObject<,>), out genericInterfaceType)) {
						services.AddTransient(genericInterfaceType, type);
					}
				}else if (type.TryGetGenericInterfaceType(typeof(ICodeGenerator<>), out genericInterfaceType)) {
					// register any ICodeGenerator
					services.AddTransient(type);
					services.AddTransient(genericInterfaceType, type);
				}
			}
			return services;
		}

		public static IServiceCollection AddDefaultCodeGen(this IServiceCollection services) {
			services.AddCodeGen(typeof(ServiceExtension).Assembly);
			return services;
		}
	}
}
