using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table-merge-update", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Table merge update clause")]
	public class TableMergeUpdate : CodeGeneratorBase<Table, SqlCodeGenOption> {
		IGetTable getTable;
		ICreateVariableName createVariableName;
		IBuildSqlType buildSqlType;

		public TableMergeUpdate(IGetTable getTable, ICreateVariableName createVariableName, IBuildSqlType buildSqlType) {
			this.getTable = getTable;
			this.createVariableName = createVariableName;
			this.buildSqlType = buildSqlType;
		}

		public override IEnumerable<object>  Build(StringBuilder sb, Table table, SqlCodeGenOption options) {
			table = getTable.Get(table.Database, table.Schema, table.Name);

			HashSet<string> keys = new HashSet<string>();
			if (options.ExcludePrimaryKey) {
				keys.AddRange(from item in table.PrimaryKeys select item.Name);
			}

			Column[] columns = (from c in table.Columns
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.IsComputed && !c.IsIdentity && !keys.Contains(c.Name)
								select c).ToArray();
			if (columns.Length == 0) {
				//merge without update is OK
				return new[] { this, };
			}
			sb.Append("when matched then update set").AppendLine();
			foreach (var column in columns) {
				sb.Tab().EscapeName(column.Name).Append(" = src.").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return new[] { this, };
		}
	}
}
