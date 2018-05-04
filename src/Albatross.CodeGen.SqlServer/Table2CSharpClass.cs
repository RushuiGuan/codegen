using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	[CodeGenerator("sql.table.class", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a sql server table")]
	public class Table2CSharpClass : CSharpClassGenerator<Table> {
		IGetTable getTable;
		IConvertSqlDataType getCSharpType;

		public override string GetClassName(Table t, CSharpClassOption option) {
			return t.Name.Proper();
		}

		public Table2CSharpClass(IGetTable getTable, IConvertSqlDataType getCSharpType) {
			this.getTable = getTable;
			this.getCSharpType = getCSharpType;
		}

		public override void RenderBody(StringBuilder sb, Table t, CSharpClassOption options) {
			t = getTable.Get(t.Database, t.Schema, t.Name);
			foreach (var item in t.Columns) {
				sb.Tab(options.TabLevel).Append("public ").Append(t.Name.Proper());
			}
		}
	}
}
