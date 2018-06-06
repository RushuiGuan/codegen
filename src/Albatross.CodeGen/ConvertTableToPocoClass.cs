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
            var properties = new List<DotNetProperty>();
			var result = new PocoClass {
				Name = table.Name.Proper(),
                Properties = properties,
			};

			foreach (var column in table.Columns) {
                propertyTypeOverrides.TryGetValue(column.Name, out string typeOverride);
                DotNetProperty property = new DotNetProperty { 
                    Name = column.Name,
                    Type = GetDotNetType(column.Type),
                    TypeOverride = typeOverride
                };
                properties.Add(property);
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
