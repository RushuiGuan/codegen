using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Albatross.DbScripting.SqlServer {
	public class ColumnSqlTypeBuilder : IColumnSqlTypeBuilder {
		public StringBuilder Build(StringBuilder sb, Column c) {
			string dataType = c.DataType?.ToLower();
			switch (dataType) {
				case "ntext":
					sb.Append("nvarchar(max)");
					break;
				case "text":
					sb.Append("varchar(max)");
					break;
				case "image":
					sb.Append("varbinary(max)");
					break;

				case "binary":
				case "char":
				case "nchar":
					sb.Append(dataType).OpenParenthesis().Append(c.MaxLength).CloseParenthesis();
					break;

				case "nvarchar":
				case "varbinary":
				case "varchar":
					sb.Append(dataType).OpenParenthesis();
					if (c.MaxLength == -1) {
						sb.Append("max");
					} else {
						sb.Append(c.MaxLength);
					}
					sb.CloseParenthesis();
					break;

				case "datetime2":
				case "datetimeoffset":
				case "float":
					sb.Append(dataType).OpenParenthesis().Append(c.Precision).CloseParenthesis();
					break;
				case "decimal":
				case "numeric":
					sb.Append(dataType).OpenParenthesis().Append(c.Precision).Comma().Space().Append(c.Scale).CloseParenthesis();
					break;
				default:
					sb.Append(dataType);
					break;
			}
			return sb;
		}
	}
}
