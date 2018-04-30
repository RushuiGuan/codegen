using System.Reflection;
using System;
using System.IO;
using System.Text;
using System.Linq;
using Albatross.CodeGen.Core;

namespace Albatross.CodeGen {
	public static class StringBuilderExtension {
		#region standard
		public static StringBuilder AppendChar(this StringBuilder sb, char c, int count = 1) {
			for (int i = 0; i < count; i++) {
				sb.Append(c);
			}
			return sb;
		}
		public static StringBuilder Tab(this StringBuilder sb, int count = 1) {
			return sb.AppendChar('\t', count);
		}
		public static StringBuilder Dot(this StringBuilder sb, int count = 1) {
			return sb.AppendChar('.', count);
		}
		public static StringBuilder Comma(this StringBuilder sb, int count = 1) {
			return sb.AppendChar(',', count);
		}
		public static StringBuilder OpenSquareBracket(this StringBuilder sb, int count = 1) {
			return sb.AppendChar('[', count);
		}
		public static StringBuilder CloseSquareBracket(this StringBuilder sb, int count = 1) {
			return sb.AppendChar(']', count);
		}
		public static StringBuilder OpenParenthesis(this StringBuilder sb, int count = 1) {
			return sb.AppendChar('(', count);
		}
		public static StringBuilder CloseParenthesis(this StringBuilder sb, int count = 1) {
			return sb.AppendChar(')', count);
		}
		public static StringBuilder Space(this StringBuilder sb, int count = 1) {
			return sb.AppendChar(' ', count);
		}
		public static StringBuilder Semicolon(this StringBuilder sb, int count = 1) {
			return sb.AppendChar(';', count);
		}
		public static StringBuilder Tabify(this StringBuilder sb, string content, int count) {
			sb.AppendChar('\t', count);
			foreach (char c in content) {
				sb.Append(c);
				if (c == '\n') { sb.AppendChar('\t', count); }
			}
			return sb;
		}
		#endregion

		#region C# code generation
		public static StringBuilder PublicClass(this StringBuilder sb) {
			sb.Append("public class ");
			return sb;
		}

		public static StringBuilder BaseClass(this StringBuilder sb, Type type) {
			sb.Append(" : ").GetTypeName(type).OpenScope();
			return sb;
		}

		public static StringBuilder ClassName(this StringBuilder sb, Type type, string postFix) {
			sb.Append(type.Name).Append(postFix);
			return sb;
		}

		public static StringBuilder Join(this StringBuilder sb, params string[] segments) {
			foreach (var item in segments) {
				sb.Append(item);
			}
			return sb;
		}

		public static StringBuilder OpenScope(this StringBuilder sb) {
			return sb.Append(" {").AppendLine();
		}
		public static StringBuilder CloseScope(this StringBuilder sb) {
			return sb.AppendLine("}");
		}
		public static StringBuilder Terminate(this StringBuilder sb) {
			return sb.AppendLine(";");
		}
		public static StringBuilder Pad(this StringBuilder sb, char c, int count = 1) {
			for (int i = 0; i < count; i++) {
				sb.Append(c);
			}
			return sb;
		}

		public static StringBuilder Public(this StringBuilder sb) {
			return sb.Append("public ");
		}
		public static StringBuilder Override(this StringBuilder sb, string methodName) {
			return sb.Append("override ").Append(methodName);
		}
		public static StringBuilder MethodParam<T>(this StringBuilder sb, string paramName) {
			return sb.GetTypeName(typeof(T)).Space().Append(paramName);
		}
		public static StringBuilder MethodParam(this StringBuilder sb, Type type, string paramName) {
			return sb.GetTypeName(type).Space().Append(paramName);
		}

		public static StringBuilder Method(this StringBuilder sb, Type returnType, string name, Action<StringBuilder> setParams) {
			sb.Space().GetTypeName(returnType).Append(name).OpenParenthesis();
			setParams(sb);
			sb.CloseParenthesis().OpenScope();
			return sb;
		}

		public static StringBuilder Constructor(this StringBuilder sb, string name, Action<StringBuilder> setParams, params string[] baseClassParams) {
			sb.Space().Append(name).OpenParenthesis();
			setParams(sb);
			sb.CloseParenthesis();
			if (baseClassParams.Length > 0) {
				sb.Append(" : base").OpenParenthesis();
				for (int i = 0; i < baseClassParams.Length; i++) {
					sb.Append(baseClassParams[i]);
					if (i < baseClassParams.Length - 1) {
						sb.Comma().Space();
					}
				}
				sb.CloseParenthesis();
			}
			sb.Space();
			return sb;
		}
		public static StringBuilder EmptyScope(this StringBuilder sb) {
			sb.AppendLine("{ }");
			return sb;
		}
		public static StringBuilder PublicGetSet (this StringBuilder sb, IRenderDotNetType renderDotNetType, Type propertyType, string name) {
			sb.Public();
			renderDotNetType.Render(sb, propertyType, false).Space().Append(name).AppendLine(" { get; set; }");
			return sb;
		}

		public static StringBuilder PublicProperty(this StringBuilder sb, IRenderDotNetType renderDotNetType, Type propertyType, string name) {
			sb.Public();
			renderDotNetType.Render(sb, propertyType, false).Space().Append(name).OpenScope();
			return sb;
		}

		public static StringBuilder Region(this StringBuilder sb, string name) {
			return sb.Append("#region ").AppendLine(name);
		}
		public static StringBuilder EndRegion(this StringBuilder sb) {
			return sb.AppendLine("#endregion ");
		}

		public static StringBuilder PropertyAssignment(this StringBuilder sb, string propertyName, object value) {
			sb.Append(propertyName).Append(" = ").Literal(value).Comma();
			return sb;
		}
		public static StringBuilder Literal(this StringBuilder sb, object value) {
			if (value == null) {
				sb.Append("null");
			} else if (value.GetType() == typeof(string)) {
				sb.Append("\"");
				sb.Append(value);
				sb.Append("\"");
			} else {
				sb.Append(value);
			}
			return sb;
		}

		public static StringBuilder AsString(this StringBuilder sb) {
			return sb.Append(".ToString()");
		}

		public static string ToGenericTypeString(this Type t) {
			if (!t.IsGenericType)
				return t.Name;
			string genericTypeName = t.GetGenericTypeDefinition().Name;
			genericTypeName = genericTypeName.Substring(0,
				genericTypeName.IndexOf('`'));
			string genericArgs = string.Join(",", t.GetGenericArguments().Select(ta => ToGenericTypeString(ta)).ToArray());
			return genericTypeName + "<" + genericArgs + ">";
		}

		public static StringBuilder GetTypeName(this StringBuilder sb, Type type) {
			if (type.IsGenericType) {
				sb.Append(type.ToGenericTypeString());
			} else if (type == typeof(int)) {
				sb.Append("int");
			} else if (type == typeof(long)) {
				sb.Append("long");
			} else if (type == typeof(DateTime)) {
				sb.Append("DateTime");
			} else if (type == typeof(string)) {
				sb.Append("string");
			} else if (type == typeof(bool)) {
				sb.Append("bool");
			} else if (type == typeof(decimal)) {
				sb.Append("decimal");
			} else if (type == typeof(double)) {
				sb.Append("double");
			} else if (type == typeof(float)) {
				sb.Append("float");
			} else {
				sb.Append(type.Name);
			}
			return sb;
		}
		#endregion
	}
}
