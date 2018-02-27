using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	[CodeGenerator("table_to_class", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a sql server table")]
	public class CSharpClassFromTable : ClassGenerator<DatabaseObject> {
		IGetTableColumn _getColumns;
		IGetCSharpType _getCSharpType;

		public override string GetClassName(DatabaseObject t, ClassOption option) {
			return t.Name.Proper();
		}

		public CSharpClassFromTable(IGetTableColumn getColumns, IGetCSharpType getCSharpType) {
			_getColumns = getColumns;
			_getCSharpType = getCSharpType;
		}

		public override void RenderBody(StringBuilder sb, int tabLevel, DatabaseObject t, ClassOption options) {
			foreach (var item in _getColumns.Get(t.Server, t.Schema, t.Name)) {
				sb.Tab(tabLevel).Append("public ").Append(t.Name.Proper());
			}
		}
	}
}
