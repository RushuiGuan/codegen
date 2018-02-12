using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	[CodeGenerator("table_to_class", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a sql server table")]
	public class GenerateClassByDatabaseTable : ClassGenerator<DatabaseObject> {
		IGetTableColumns _getColumns;
		IGetCSharpType _getCSharpType;

		public override string GetClassName(DatabaseObject t) {
			return t.Name.Proper();
		}

		public GenerateClassByDatabaseTable(IGetTableColumns getColumns, IGetCSharpType getCSharpType) {
			_getColumns = getColumns;
			_getCSharpType = getCSharpType;
		}

		public override void RenderBody(StringBuilder sb, int tabLevel, DatabaseObject t, ClassOption options) {
			foreach (var item in _getColumns.Get(t)) {
				sb.Tab(tabLevel).Append("public ").Append(t.Name.Proper());
			}
		}
	}
}
