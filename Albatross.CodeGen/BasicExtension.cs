using System;
using System.IO;
using System.Text;

namespace Albatross.CodeGen {
	public static class BasicExtension {
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
	}
}
