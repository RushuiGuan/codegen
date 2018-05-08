using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.merge.update", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Table merge update clause")]
	public class MergeUpdate : TableQueryGenerator {
		IGetTable getTable;
		ICreateSqlVariableName createVariableName;
		IRenderSqlType buildSqlType;

		public MergeUpdate(IGetTable getTable, ICreateSqlVariableName createVariableName, IRenderSqlType buildSqlType) {
			this.getTable = getTable;
			this.createVariableName = createVariableName;
			this.buildSqlType = buildSqlType;
		}

		public override IEnumerable<object>  Generate(StringBuilder sb, IDictionary<string, string> customCode, Table table, SqlCodeGenOption options) {
			HashSet<string> keys = new HashSet<string>();
			keys.AddRange(from item in table.PrimaryKeys select item.Name);

			Column[] columns = (from c in table.Columns ?? new Column[0]
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
