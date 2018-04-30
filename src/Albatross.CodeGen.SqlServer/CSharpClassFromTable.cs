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
	[CodeGenerator("table_to_class", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a sql server table")]
	public class CSharpClassFromTable : ClassGenerator<Table> {
		IGetTable getTable;
		IConvertDataType getCSharpType;

		public override string GetClassName(Table t, ClassOption option) {
			return t.Name.Proper();
		}

		public CSharpClassFromTable(IGetTable getTable, IConvertDataType getCSharpType) {
			this.getTable = getTable;
			this.getCSharpType = getCSharpType;
		}

		public override void RenderBody(StringBuilder sb, int tabLevel, Table t, ClassOption options) {
			t = getTable.Get(t.Database, t.Schema, t.Name);
			foreach (var item in t.Columns) {
				sb.Tab(tabLevel).Append("public ").Append(t.Name.Proper());
			}
		}
	}
}
