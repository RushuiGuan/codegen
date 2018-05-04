using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.merge.join", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Merge statement source join clause")]
	public class MergeJoin : TableQueryGenerator {
		IGetTable getTable;

		public MergeJoin(IGetTable getTable) {
			this.getTable = getTable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table table, SqlCodeGenOption options) {
			table = getTable.Get(table.Database, table.Schema, table.Name);
			if ((options.Filter & FilterOption.ByIdentityColumn) > 0) {
				if (table.IdentityColumn == null) {
					throw new CodeGenException("Identity column doesn't exist");
				}
				sb.Append("on src.").EscapeName(table.IdentityColumn.Name).Append(" = dst.").EscapeName(table.IdentityColumn.Name);
			} else if ((options.Filter & FilterOption.ByPrimaryKey) > 0) {
				if (table.PrimaryKeys?.Count() > 0) {
					foreach (var item in table.PrimaryKeys) {
						if (item == table.PrimaryKeys.First()) {
							sb.Append("on ");
						} else {
							sb.Append("and ");
						}
						sb.Append("src.").EscapeName(item.Name).Append(" = dst.").EscapeName(item.Name);
					}
				} else {
					throw new CodeGenException("Primary key doesn't exist");
				}
			}
			
			return new[] { this };
		}
	}
}
