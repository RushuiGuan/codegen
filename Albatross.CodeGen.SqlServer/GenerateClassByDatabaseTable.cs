using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	public class GenerateClassByDatabaseTable : ClassGenerator<Table> {
		public override string Category => "Sql Server";
		public override string Description => "Generate a C# class from a sql server table";
		public override string Name => "table_to_class";
		IGetTableColumns _getColumns;
		IGetCSharpType _getCSharpType;

		public override string GetClassName(Table t) {
			return t.Name.Proper();
		}

		public GenerateClassByDatabaseTable(IGetTableColumns getColumns, IGetCSharpType getCSharpType) {
			_getColumns = getColumns;
			_getCSharpType = getCSharpType;
		}

		public override void RenderBody(StringBuilder sb, int tabLevel, Table t, ClassOptions options, ICodeGeneratorFactory factory) {
			foreach (var item in _getColumns.Get(t)) {
				sb.Tab(tabLevel).Append("public ").Append(t.Name.Proper())
			}
		}
	}
}
