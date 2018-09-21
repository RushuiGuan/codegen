using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen {
	public static class IConfigurableCodeGenFactoryExt {
		public static void RegisterConstant(this IConfigurableCodeGenFactory factory) {
			factory.RegisterConstant("newline", GeneratorTarget.Any, "\r\n", GeneratorCategory.Constant, null);
			factory.RegisterConstant("tab", GeneratorTarget.Any, "\t", GeneratorCategory.Constant, null);
		}


		public static IConfigurableCodeGenFactory RegisterComposite(this IConfigurableCodeGenFactory factory, Composite item) {
			factory.Register(item.GetMeta());
			return factory;
		}

		public static bool TryRegister(this IConfigurableCodeGenFactory factory, Type type, string name, string target, string category, string description, object data) {
			Type interfaceType = type.FindInterfaces(new TypeFilter((t, obj) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICodeGenerator<,>)), null).FirstOrDefault();
			if (interfaceType != null) {
				Type[] arguments = interfaceType.GetGenericArguments();
				CodeGenerator gen = new CodeGenerator {
					Name = name,
					Target = target,
					Category = category,
					Description = description,
					GeneratorType = type,
					SourceType = arguments[0],
					OptionType = arguments[1],
				};
				factory.Register(gen);
				return true;
			} else {
				return false;
			}
		}

		public static void RegisterAssembly(this IConfigurableCodeGenFactory codeGenFactory, Assembly asm) {
			foreach (Type type in asm.GetTypes()) {
				var codeGenAttrib = type.GetCustomAttribute<CodeGeneratorAttribute>();
				if (codeGenAttrib != null) {
					codeGenFactory.TryRegister(type, codeGenAttrib.Name, codeGenAttrib.Target, codeGenAttrib.Category, codeGenAttrib.Description, null);
				}
			}
		}
		public static IConfigurableCodeGenFactory RegisterConstant(this IConfigurableCodeGenFactory factory, string name, string target, string content, string category, string description) {
			factory.Register(new CodeGenerator {
				Category = category,
				Data = content,
				Description = description,
				GeneratorType = typeof(ConstantCodeGenerator),
				SourceType = typeof(object),
				OptionType = typeof(object),
				Name = name,
				Target = target,
			});
			return factory;
		}
	}
}
