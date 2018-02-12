using System.Text;
using Albatross.CodeGen.Database;

namespace Albatross.CodeGen.SqlServer {
	public class ColumnSqlTypeBuilder : IColumnSqlTypeBuilder {
		public string Build(Column c) {
			string dataType = c.DataType?.ToLower();
			switch (dataType) {
				case "ntext":
					return "nvarchar(max)";
				case "text":
					return "varchar(max)";

				case "image":
					return "varbinary(max)";

				case "binary":
				case "char":
				case "nchar":
					return $"{dataType}({c.MaxLength})";

				case "nvarchar":
				case "varbinary":
				case "varchar":
					if (c.MaxLength == -1) {
						return $"{dataType}(max)";
					} else {
						return $"{dataType}({c.MaxLength})";
					}

				case "datetime2":
				case "datetimeoffset":
				case "float":
					return $"{dataType}({c.Precision})";

				case "decimal":
				case "numeric":
					return $"{dataType}({c.Precision}, {c.Scale})";
				default:
					return dataType;
			}
		}
	}
}
