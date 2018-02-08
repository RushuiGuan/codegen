using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	public static class Extension
    {
		public static string GetConnectionString(this DatabaseObject table) {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
				DataSource = table.Server.DataSource,
				InitialCatalog = table.Server.InitialCatalog,
				IntegratedSecurity = true
			};
			return builder.ToString();
		}

		public static Composite<DatabaseObject, SqlQueryOption> NewSqlTableComposite(string name, string description, Branch branch) {
			return new Composite<DatabaseObject, SqlQueryOption>{
				Name = name,
				Description = description,
				Category = "Sql Server",
				Branch = branch,
				Target = "sql",
			};
		}

		public static StringBuilder EscapeName(this StringBuilder sb, string name) {
			return sb.Append('[').Append(name).Append(']');
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
