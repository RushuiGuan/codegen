using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public static class Extensions {
		#region columns
		public static bool IsString(this Column c) {
			string dataType = c.DataType.ToLower();
			return dataType == "nvarchar" || dataType == "varchar" || dataType == "char" || dataType == "nchar";
		}
		public static bool IsDateTime(this Column c) {
			string dataType = c.DataType.ToLower();
			return dataType == "datetime" || dataType == "datetime2" || dataType == "smalldatetime";
		}
		public static bool IsBoolean(this Column c) {
			return c.DataType.ToLower() == "bit";
		}
		public static bool IsNumeric(this Column c) {
			string dataType = c.DataType.ToLower();
			return
				dataType == "tinyint"
				|| dataType == "smallint"
				|| dataType == "int"
				|| dataType == "bigint"

				|| dataType == "float"
				|| dataType == "real"
				|| dataType == "decimal"
				|| dataType == "numeric"

				|| dataType == "money"
				|| dataType == "smallmoney";
		}
		#endregion

		public static bool Match(this IBuiltInColumnFactory builtInColumnFactory, Column column) {
			return builtInColumnFactory.Get(column.Name).Match(column);
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
		public static StringBuilder EscapeName(this StringBuilder sb, string name) {
			return sb.Append('[').Append(name).Append(']');
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

		public static string GetTypeEmbeddedResource(this Type type, string extension) {
			using (Stream stream = type.Assembly.GetManifestResourceStream(type.FullName + extension)) {
				return new StreamReader(stream).ReadToEnd();
			}
		}
	}
}
