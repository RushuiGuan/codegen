using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen {
	public static class Extension {
		public static string GetAssemblyResource(this Type type, string name) {
			using (Stream stream = type.Assembly.GetManifestResourceStream(name)) {
				using (StreamReader reader = new StreamReader(stream)) {
					return reader.ReadToEnd();
				}
			}
		}

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

		public static string GetGeneratorKey(this Type type, string name) {
				return $"{type.FullName}.{name}";
		}

		public static StringBuilder Tabify(this StringBuilder sb, string content, int count) {
			sb.AppendChar('\t', count);
			foreach (char c in content) {
				sb.Append(c);
				if (c == '\n') { sb.AppendChar('\t', count); }
			}
			return sb;
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
			Type type = typeof(CompositeCodeGenerator<,>);
			var meta = new CodeGenerator {
				Name = item.Name,
				Target = item.Target,
				Category = item.Category,
				Description = item.Description,
				GeneratorType = type.MakeGenericType(item.SourceType, item.OptionType),
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
		public static bool TryRegister<T>(this IConfigurableCodeGenFactory factory, string name, string target, string category, string description, object data) {
			return TryRegister(factory, typeof(T), name, target, category, description, data);
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

		public static void Register(this Assembly asm, IConfigurableCodeGenFactory codeGenFactory, IFactory<SourceType> srcTypeFactory, IFactory<OptionType> optionTypeFactory) {
			foreach (Type type in asm.GetTypes()) {
				var codeGenAttrib = type.GetCustomAttribute<CodeGeneratorAttribute>();
				if (codeGenAttrib != null) {
					codeGenFactory.TryRegister(type, codeGenAttrib.Name, codeGenAttrib.Target, codeGenAttrib.Category, codeGenAttrib.Description, null);
				}

				if (srcTypeFactory != null) {
					var srcTypeAttrib = type.GetCustomAttribute<SourceTypeAttribute>();
					if (srcTypeAttrib != null) {
						srcTypeFactory.Register(new SourceType { Description = srcTypeAttrib.Description, ObjectType = type, });
					}
				}

				if (optionTypeFactory != null) {
					var optionTypeAttrib = type.GetCustomAttribute<OptionTypeAttribute>();
					if (optionTypeAttrib != null) {
						optionTypeFactory.Register(new OptionType { Description = optionTypeAttrib.Description, ObjectType = type, });
					}
				}
			}
		}
		#endregion
	}
}
