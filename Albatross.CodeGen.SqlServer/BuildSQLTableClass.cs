using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Albatross.CodeGen;

namespace Albatross.CodeGen.SqlServer
{
	public class BuildSQLTableClass {
		public class Column {
			public string Column_Name { get; set; }
			public string Data_Type { get; set; }
			public int? Character_Maximum_Length { get; set; }
			public string Is_Nullable { get; set; }
			public bool IsNullable => Is_Nullable == "YES";
		}

		public string Build(string server, string database, string table, string className) {
			StringBuilder sb = new StringBuilder();

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = server;
			builder.InitialCatalog = database;
			builder.IntegratedSecurity = true;

			sb.PublicClass().Append(className).OpenScope();

			using (SqlConnection conn = new SqlConnection(builder.ToString())) {
				var items = conn.Query<Column>("select * from INFORMATION_SCHEMA.COLUMNS where table_name = @name order by column_name", new { name = table });
				foreach (var item in items) {
					bool requireTrim, valueType;
					Type type = GetType(item.Data_Type, item.Character_Maximum_Length, out requireTrim, out valueType);

					if (requireTrim) {
						sb.Tab().GetTypeName(type).Space().Append("_").Append(item.Column_Name).Terminate();
						sb.Tab().Public().GetTypeName(type).Space().Append(item.Column_Name).OpenScope();
						sb.Tab(2).Append("get { return _").Append(item.Column_Name).CloseScope(true);
						sb.Tab(2).Append("set { _").Append(item.Column_Name).Append(" = value?.TrimEnd(); }").AppendLine();
						sb.Tab().CloseScope();
					} else {
						sb.Tab().Public().GetTypeName(type);
						if (valueType && item.IsNullable) { sb.Append("?"); }
						sb.Space().Append(item.Column_Name).Append(" { get; set; }").AppendLine();
					}
				}
			}

			sb.CloseScope();
			return sb.ToString();
		}

		Type GetType(string sqlType, int? maxLength, out bool requireTrim, out bool valueType) {
			valueType = true;
			requireTrim = false;
			switch (sqlType) {
				case "bigint": return typeof(long);
				case "bit": return typeof(bool);
				case "int": return typeof(int);
				case "smallint": return typeof(short);
				case "tinyint": return typeof(byte);

				case "char":
				case "nchar":
					valueType = false;
					if (maxLength == 1) {
						return typeof(char);
					} else {
						requireTrim = true;
						return typeof(string);
					}
				case "nvarchar":
				case "varchar":
				case "xml":
				case "ntext":
				case "text":
					valueType = false;
					return typeof(string);

				case "date":
				case "datetime":
				case "datetime2":
				case "smalldatetime":
					return typeof(DateTime);

				case "decimal":
				case "numeric":
					return typeof(decimal);

				case "float": return typeof(float);
				case "money": return typeof(decimal);

				case "real": return typeof(double);

				case "uniqueidentifier": return typeof(Guid);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
