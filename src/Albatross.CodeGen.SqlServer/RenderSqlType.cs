using System;
using System.Text;
using Albatross.CodeGen.Database;
using Albatross.Database;

namespace Albatross.CodeGen.SqlServer {
	public class RenderSqlType : IRenderSqlType  {
		const int default_datetime2_Precision = 7;
		const int default_datetimeoffset_Precision = 7;

		const int default_float_Precision = 53;
		const int default_decimal_Precision = 18;
		const int default_decimal_Scale = 0;

		public string Render(SqlType type) {
			if (type.IsTableType) {
				return $"[{type.Schema}].[{type.Name}]";
			} else {
				string dataType = type.Name.ToLower();
				switch (dataType) {
					case "ntext":
					case "text":
					case "image":
						return dataType;

					case "binary":
					case "char":
					case "nchar":
						if (type.MaxLength.HasValue) {
							return $"{dataType}({type.MaxLength})";
						} else {
							return dataType;
						}

					case "nvarchar":
					case "varbinary":
					case "varchar":
						if (type.MaxLength == -1) {
							return $"{dataType}(max)";
						} else if (type.MaxLength.HasValue) {
							return $"{dataType}({type.MaxLength})";
						} else {
							return dataType;
						}

					case "datetime2":
					case "datetimeoffset":
						if (type.Precision == default_datetime2_Precision || type.Precision == null) {
							return dataType;
						} else {
							return $"{dataType}({type.Precision})";
						}
					case "float":
						if (type.Precision == default_float_Precision || type.Precision == null) {
							return dataType;
						} else {
							return $"{dataType}({type.Precision})";
						}

					case "decimal":
					case "numeric":
						if (type.Precision == default_decimal_Precision || type.Precision == null) {
							if (type.Scale == default_decimal_Scale || type.Scale == null) {
								return dataType;
							} else {
								return $"{dataType}({type.Precision})";
							}
						} else {
							return $"{dataType}({type.Precision}, {type.Scale})";
						}
					default:
						return dataType;
				}
			}
		}
	}
}
