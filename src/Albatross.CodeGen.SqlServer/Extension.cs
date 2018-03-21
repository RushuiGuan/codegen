using System.Linq;
using Albatross.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public static class Extension
    {
		public static StringBuilder EscapeName(this StringBuilder sb, string name) {
			return sb.Append('[').Append(name).Append(']');
		}

		#region table
		public static void Get(this IGetTable handle, ref Table table) {
			table = handle.Get(table.Database, table.Schema, table.Name);
		}
		public static IEnumerable<Column> GetPrimaryKeyColumns(this Table table) {
			if (table.PrimaryKeys != null) {
				return from c in table.Columns join i in table.PrimaryKeys on c.Name equals i.Name select c;
			} else {
				return new Column[0];
			}
		}
		#endregion


		#region columns
		public static bool IsString(this SqlType type) {
			string dataType = type.Name.ToLower();
			return dataType == "nvarchar" || dataType == "varchar" || dataType == "char" || dataType == "nchar";
		}
		public static bool IsDateTime(this SqlType type) {
			string dataType = type.Name.ToLower();
			return dataType == "datetime" || dataType == "datetime2" || dataType == "smalldatetime";
		}
		public static bool IsBoolean(this SqlType type) {
			return type.Name.ToLower() == "bit";
		}
		public static bool IsNumeric(this SqlType type) {
			string dataType = type.Name.ToLower();
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

		#region variable
		public static Variable GetVariable(this Column column) {
			return new Variable {
				Name = column.Name,
				Type = column.Type,
				Direction = System.Data.ParameterDirection.Input,
			};
		}
		#endregion
	}
}
