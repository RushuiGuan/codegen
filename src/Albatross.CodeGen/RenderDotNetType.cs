using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
	public class RenderDotNetType : IRenderDotNetType {
		Dictionary<Type, string> mapping = new Dictionary<Type, string>();

		void Init() {
			mapping.Add(typeof(string), "string");

			mapping.Add(typeof(short), "short");
			mapping.Add(typeof(int), "int");
			mapping.Add(typeof(long), "long");

			mapping.Add(typeof(ushort), "ushort");
			mapping.Add(typeof(uint), "uint");
			mapping.Add(typeof(ulong), "ulong");

			mapping.Add(typeof(float), "float");
			mapping.Add(typeof(decimal), "decimal");
			mapping.Add(typeof(char), "char");
			mapping.Add(typeof(double), "double");
			mapping.Add(typeof(bool), "bool");

			mapping.Add(typeof(byte), "byte");
			mapping.Add(typeof(sbyte), "sbyte");
		}

		public RenderDotNetType() {
			Init();
		}

		StringBuilder GetTypeName(StringBuilder sb, Type type) {
			string name;
			if (type.Namespace == "System") {
				name = type.Name;
			} else {
				name = type.FullName;
			}

			int index = name.LastIndexOf('`');
			if (index != -1) {
				name = name.Substring(0, index);
			}
			name = name.Replace('+', '.');
			return sb.Append(name);
		}
		public StringBuilder Render(StringBuilder sb, Type type, bool nullable) {
			if (type.IsGenericType) {
				Type[] arguments = type.GetGenericArguments();
				Type genericType = type.GetGenericTypeDefinition();
				if (genericType == typeof(Nullable<>)) {
					Render(sb, arguments[0], true);
				} else {
					GetTypeName(sb, genericType);
					for (int i = 0; i < arguments.Length; i++) {
						if (i == 0) {
							sb.Append("<");
						} else {
							sb.Comma().Space();
						}
						Render(sb, arguments[i], false);
					}
					sb.Append(">");
				}
			} else {
				string text;
				if (mapping.TryGetValue(type, out text)) {
					sb.Append(text);
				} else {
					GetTypeName(sb, type);
				}
				if (nullable && type.IsValueType) {
					sb.Append("?");
				}
			}
			return sb;
		}
	}
}
