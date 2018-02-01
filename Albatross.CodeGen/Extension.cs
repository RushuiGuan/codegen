using Albatross.CodeGen.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
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
		
		public static Type LoadType(this ObjectType objType) {
			Assembly asm = Assembly.ReflectionOnlyLoadFrom(objType.AssemblyLocation);
			return asm.GetType(objType.ClassName);
		}


		public static List<Type> GetSourceType(this List<Type> list, Assembly asm) {
			foreach (var type in asm.GetTypes()) {
				if (type.GetCustomAttribute<SourceTypeAttribute>() != null) {
					list.Add(type);
				}
			}
			return list;
		}

		public static void TypeCheck<T>(this object t, string name) {
			if (t != null && !(t is T)) {
				throw new ArgumentException($"Invalid {name}: {t.GetType().Name}; expected: {typeof(T).Name}");
			}
		}

		public static T ContextCheck<T>(this IDictionary<string, object> dict, string key) {
			if (!dict.TryGetValue(key, out object value)) {
				throw new ContextException(key);
			} else if (!(value is T)) {
				throw new ContextException(key, typeof(T));
			} else {
				return (T)value;
			}
		}
	}
}
