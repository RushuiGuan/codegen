using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public static class SQLExtension {
		public static StringBuilder EscapeName(this StringBuilder sb, string name) {
			return sb.Append('[').Append(name).Append(']');
		}

		public static bool Match(this IBuiltInColumnFactory builtInColumnFactory, Column column) {
			return builtInColumnFactory.Get(column.Name).Match(column);
		}

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

	}
}
