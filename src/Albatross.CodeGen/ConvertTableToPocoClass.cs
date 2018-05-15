using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;

namespace Albatross.CodeGen {
	public class ConvertTableToPocoClass : IConvertTableToPocoClass {
		IConvertSqlDataType convertSqlDataType;
		public ConvertTableToPocoClass(IConvertSqlDataType convertSqlDataType) {
			this.convertSqlDataType = convertSqlDataType;
		}
		public PocoClass Convert(Table table, IDictionary<string, string> propertyTypeOverrides) {
			var result = new PocoClass {
				Name = table.Name.Proper(),
				TypeOverrides = propertyTypeOverrides ?? new Dictionary<string, string>(),
				Properties = new Dictionary<string, Type>(),
			};

			foreach (var column in table.Columns) {
				result.Properties.Add(column.Name.Proper(), GetDotNetType(column.Type));
			}
			return result;
		}

		Type GetDotNetType(SqlType sqlType) {
			Type type = convertSqlDataType.GetDotNetType(sqlType);
			if (type.IsValueType && sqlType.IsNullable) {
				type = typeof(Nullable<>).MakeGenericType(type);
			}
			return type;
		}
	}
}
