using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Core;
using Albatross.Database;

namespace Albatross.CodeGen.SqlServer {
	/// <summary>
	/// Create a CSharp class from a SQL Stored procedure
	/// </summary>
	public class SqlTable2CSharpClass : CodeGeneratorBase<Table, Class> {
		IWriteObject<Class> writeClass;
		IGetDotNetType getDotNetType;

		public SqlTable2CSharpClass(IWriteObject<Class> writeClass, IGetDotNetType getDotNetType) {
			this.writeClass = writeClass;
			this.getDotNetType = getDotNetType;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table source, Class classOption) {
			if (string.IsNullOrEmpty(classOption.Name)) {
				classOption.Name = source.Name;
			}
			var columns = from item in source.Columns select new Property(item.Name.Proper()) {
				Type = getDotNetType.Get(item.Type),
				Modifier = AccessModifier.Public,
			};
			if (classOption.Properties?.Count() > 0) {
				classOption.Properties = columns.Except(from c in columns join p in classOption.Properties on c.Name equals p.Name select c).Union(classOption.Properties).OrderBy(p => p.Name);
			} else {
				classOption.Properties = columns;
			}
			sb.Write(writeClass, classOption);
			return new object[] { this };
		}
	}
}