using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_merge_join_by_id", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement source join clause by the identity column")]
	public class TableMergeJoinByID : TableQueryGenerator {
		IGetTable getTable;

		public TableMergeJoinByID(IGetTable getTable) {
			this.getTable = getTable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table table, SqlCodeGenOption options) {
			table = getTable.Get(table.Database, table.Schema, table.Name);
			if (table.IdentityColumn == null) {
				throw new CodeGenException("Identity column doesn't exist");
			}
			sb.Append("on src.").EscapeName(table.IdentityColumn.Name).Append(" = dst.").EscapeName(table.IdentityColumn.Name);
			return new[] { this };
		}
	}
}
