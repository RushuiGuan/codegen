using Albatross.CodeGen.Database;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class GetCSharpType : IGetCSharpType {
		public Type Get(SqlType sqlType) {
			switch (sqlType.Name.ToLower()) {
				case "bit":
					return typeof(Boolean);

				case "binary":
				case "varbinary":
				case "image":
				case "timestamp":
					return typeof(byte[]);
				
				case "varchar":
				case "nvarchar":
				case "char":
				case "nchar":
				case "ntext":
				case "text":
				case "xml":
					return typeof(string);

				case "tinyint":
					return typeof(byte);
				case "smallint":
					return typeof(Int16);
				case "int":
					return typeof(int);
				case "bigint":
					return typeof(long);

				case "uniqueidentifier":
					return typeof(Guid);

				case "date":
				case "datetime":
				case "datetime2":
				case "smalldatetime":
					return typeof(DateTime);
				case "datetimeoffset":
					return typeof(DateTimeOffset);

				case "decimal":
				case "numeric":
				case "money":
				case "smallmoney":
					return typeof(decimal);

				case "float":
					return typeof(double);
				case "real":
					return typeof(Single);

				case "sql_variant":
					return typeof(object);

				default:
					throw new NotSupportedException();
			}
		}
	}
}
