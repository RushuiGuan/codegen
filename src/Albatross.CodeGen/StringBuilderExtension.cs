using System.Reflection;
using System;
using System.IO;
using System.Text;
using System.Linq;

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
		public static StringBuilder OpenAngleBracket(this StringBuilder sb, int count = 1) {
			return sb.AppendChar('<', count);
		}
		public static StringBuilder CloseAngleBracket(this StringBuilder sb, int count = 1) {
			return sb.AppendChar('>', count);
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
		/// <summary>
		/// this function will always TrimEnd
		/// </summary>
		public static StringBuilder Tabify(this StringBuilder sb, string content, int count, bool removeBlankLines) {
			int begin = sb.Length;
			bool isBlank = true;
			sb.AppendChar('\t', count);
			for (int i = 0; i<content.Length; i++) {
				char c = content[i];
				sb.Append(c);
				if (!char.IsWhiteSpace(c)) {
					isBlank = false;
				}

				if (c == '\n') {
					if (!(isBlank && removeBlankLines)) {
						begin = sb.Length;
					} else {
						sb.Length = begin;
					}
					isBlank = true;
					if (i < content.Length-1) {
						sb.AppendChar('\t', count);
					}
				}
			}
			if (isBlank && removeBlankLines) {
				sb.Length = begin;
			}
			
			return sb;
		}
		public static StringBuilder TrimEnd(this StringBuilder sb, int offset = 0, params char[] additional) {
			int i = sb.Length - 1;
			while(i >= offset && (char.IsWhiteSpace(sb[i]) || additional.Contains(sb[i]))) {
				i--;
			}
			sb.Length = i + 1;
			return sb;
		}
		public static StringBuilder TrimTrailingComma(this StringBuilder sb) {
			return sb.TrimEnd(0, ',');
		}
		public static StringBuilder Write<T>(this StringBuilder sb, IWriteObject<T> writer, T t, int tabify = 0) {
			int position = sb.Length;
			string text = writer.Write(t);
			if (tabify > 0) {
				sb.Tabify(text, tabify, true);
			} else {
				sb.Append(text);
			}
			return sb;
		}
		#endregion

		#region C# code generation
		public static StringBuilder This(this StringBuilder sb, string name) {
			return sb.Append("this.").Append(name);
		}

		public static StringBuilder Static(this StringBuilder sb) {
			sb.Append("static ");
			return sb;
		}

		public static StringBuilder ReadOnly(this StringBuilder sb) {
			sb.Append("readonly ");
			return sb;
		}

		public static StringBuilder Assignment(this StringBuilder sb) {
			sb.Append(" = ");
			return sb;
		}

		public static StringBuilder Region(this StringBuilder sb, string name) {
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
		#endregion
	}
}
