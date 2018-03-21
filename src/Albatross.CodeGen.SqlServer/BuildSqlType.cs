using System.Text;
using Albatross.CodeGen.Database;
using Albatross.Database;

namespace Albatross.CodeGen.SqlServer {
	public class BuildSqlType : IBuildSqlType  {
		public string Build(SqlType type) {
			string dataType = type.Name.ToLower();
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
					return $"{dataType}({type.MaxLength})";

				case "nvarchar":
				case "varbinary":
				case "varchar":
					if (type.MaxLength == -1) {
						return $"{dataType}(max)";
					} else {
						return $"{dataType}({type.MaxLength})";
					}

				case "datetime2":
				case "datetimeoffset":
				case "float":
					return $"{dataType}({type.Precision})";

				case "decimal":
				case "numeric":
					return $"{dataType}({type.Precision}, {type.Scale})";
				default:
					return dataType;
			}
		}
	}
}
