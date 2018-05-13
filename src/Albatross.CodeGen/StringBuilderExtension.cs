using System.Reflection;
using System;
using System.IO;
using System.Text;
using System.Linq;
using Albatross.CodeGen.Core;

namespace Albatross.CodeGen {
	public static class StringBuilderExtension {
		#region standard
		public static StringBuilder Proper(this StringBuilder sb, string text) {
			if (!string.IsNullOrEmpty(text)) {
				sb.Append(text.Substring(0, 1).ToUpper());
				if (text.Length > 1) {
					sb.Append(text.Substring(1));
				}
			}
			return sb;
		}
		public static StringBuilder ProperVariable(this StringBuilder sb, string text) {
			if (!string.IsNullOrEmpty(text)) {
				sb.Append(text.Substring(0, 1).ToLower());
				if (text.Length > 1) {
					sb.Append(text.Substring(1));
				}
			}
			return sb;
		}

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
		public static StringBuilder Public(this StringBuilder sb) {
			return sb.Append("public ");
		}
		public static StringBuilder PublicClass(this StringBuilder sb) {
			sb.Append("public class ");
			return sb;
		}
		public static StringBuilder OpenScope(this StringBuilder sb) {
			return sb.Append(" {").AppendLine();
		}
		public static StringBuilder CloseScope(this StringBuilder sb) {
			return sb.AppendLine("}");
		}
		public static StringBuilder EmptyScope(this StringBuilder sb) {
			sb.AppendLine("{ }");
			return sb;
		}
		public static StringBuilder EmptyParenthesis(this StringBuilder sb) {
			sb.AppendLine("()");
			return sb;
		}
		public static StringBuilder Terminate(this StringBuilder sb) {
			return sb.AppendLine(";");
		}
		public static StringBuilder Override(this StringBuilder sb, string methodName) {
			return sb.Append("override ").Append(methodName);
		}
		public static StringBuilder Return(this StringBuilder sb) {
			return sb.Append("return ");
		}
		public static StringBuilder Await(this StringBuilder sb) {
			return sb.Append("await ");
		}
		public static StringBuilder Variable(this StringBuilder sb, string name) {
			return sb.Append("name");
		}
		public static StringBuilder Generic(this StringBuilder sb, IRenderDotNetType renderDotNetType, string name, params Type[] paramTypes) {
			sb.Append(name);
			for (int i = 0; i < paramTypes.Length; i++) {
				if (i == 0) {
					sb.Append("<");
				} else {
					sb.Comma().Space();
				}
				renderDotNetType.Render(sb, paramTypes[i], false);
			}
			return sb.Append("<");
		}
		public static StringBuilder GenericMethod(this StringBuilder sb, IRenderDotNetType renderDotNetType, string name, params Type[] paramTypes) {
			sb.Dot();
			GenericMethod(sb, renderDotNetType, name, paramTypes);
			return sb;
		}
		public static StringBuilder Method(this StringBuilder sb, string name) {
			return sb.Dot().Append(name);
		}
		public static StringBuilder PublicGetSet (this StringBuilder sb, IRenderDotNetType renderDotNetType, Type propertyType, string name) {
			sb.Public();
			renderDotNetType.Render(sb, propertyType, false).Space().Append(name).AppendLine(" { get; set; }");
			return sb;
		}
		public static StringBuilder BeginRegion(this StringBuilder sb, string name) {
			return sb.Append("#region ").AppendLine(name);
		}
		public static StringBuilder EndRegion(this StringBuilder sb) {
			return sb.AppendLine("#endregion ");
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
		public static StringBuilder Void(this StringBuilder sb) {
			sb.Append("void ");
			return sb;
		}
		#endregion
	}
}
