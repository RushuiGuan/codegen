using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen {
	public static class Extension {
		public static string Proper(this string text) {
			if (!string.IsNullOrEmpty(text)) {
				string result = text.Substring(0, 1).ToUpper();
				if (text.Length > 1) {
					result = result + text.Substring(1);
				}
				return result;
			} else {
				return text;
			}
		}

		public static void AddRange<T>(this HashSet<T> list, IEnumerable<T> items) {
			if (items != null) {
				foreach (var item in items) {
					list.Add(item);
				}
			}
		}

		#region registration helpers
		public static IConfigurableCodeGenFactory RegisterStatic(this IConfigurableCodeGenFactory factory, string name, string target, string content, string category, string description) {
			factory.Register(new CodeGenerator {
				Category = category,
				Data = content,
				Description = description,
				GeneratorType = typeof(StaticCodeGenerator),
				SourceType = typeof(object),
				OptionType = typeof(object),
				Name = name,
				Target = target,
			});
			return factory;
		}

		public static void RegisterStatic(this IConfigurableCodeGenFactory factory) {
			factory.RegisterStatic("Newline", GeneratorTarget.Any, "\r\n", "Static", null);
			factory.RegisterStatic("Tab", GeneratorTarget.Any, "\r\n", "Tab", null);
		}

		public static CodeGenerator GetMeta(this Composite item) {
			Type type;
			if (item.SourceType == typeof(object) && item.OptionType == typeof(object)) {
				type = typeof(MultiSourceCompositeCodeGenerator);
			} else {
				type = typeof(MonoSourceCompositeCodeGenerator<,>);
				type = type.MakeGenericType(item.SourceType, item.OptionType);
			}
			var meta = new CodeGenerator {
				Name = item.Name,
				Target = item.Target,
				Category = item.Category,
				Description = item.Description,
				GeneratorType = type,
				SourceType = item.SourceType,
				OptionType = item.OptionType,
				Data = item.Branch,
			};
			return meta;
		}
		public static IConfigurableCodeGenFactory Register(this IConfigurableCodeGenFactory factory, Composite item) {
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

		public static void Register(this Assembly asm, IConfigurableCodeGenFactory codeGenFactory) {
			foreach (Type type in asm.GetTypes()) {
				var codeGenAttrib = type.GetCustomAttribute<CodeGeneratorAttribute>();
				if (codeGenAttrib != null) {
					codeGenFactory.TryRegister(type, codeGenAttrib.Name, codeGenAttrib.Target, codeGenAttrib.Category, codeGenAttrib.Description, null);
				}
			}
		}
		#endregion

		#region source option type validation
		public static void Validate(this CodeGenerator codeGenerator, Type sourceType, Type optionType) {
			if (!codeGenerator.SourceType.IsAssignableFrom(sourceType)) {
				throw new Faults.InvalidSourceTypeException(codeGenerator.SourceType, sourceType);
			}
			if (!codeGenerator.OptionType.IsAssignableFrom(optionType)) {
				throw new Faults.InvalidOptionTypeException(codeGenerator.SourceType, optionType);
			}
		}
		public static IEnumerable<object> ValidateNBuild<T, O>(this ICodeGenerator<T, O> codeGenerator, StringBuilder sb, object source, object option) where T : class where O : class {
			if (source != null && !typeof(T).IsAssignableFrom(source.GetType())) {
				throw new Faults.InvalidSourceTypeException(typeof(T), source.GetType());
			}
			if (option != null && !typeof(O).IsAssignableFrom(option.GetType())) {
				throw new Faults.InvalidOptionTypeException(typeof(O), option.GetType());
			}
			return codeGenerator.Build(sb, (T)source, (O)option);
			#endregion
		}
	}
}
