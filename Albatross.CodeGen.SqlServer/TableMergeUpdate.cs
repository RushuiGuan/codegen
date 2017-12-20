using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeUpdate : TableCodeGenerator {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;

		public TableMergeUpdate(IGetTableColumns getColumns, IGetVariableName getVariableName) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
		}

		public override string Name => "table_merge_update";
		public override string Description => "Table merge update clause";

		public override StringBuilder Build(StringBuilder sb, Table table, ICodeGeneratorFactory factory) {
			Column[] columns = (from c in _getColumns.Get(table)
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.ComputedColumn && !c.IdentityColumn
								select c).ToArray();
			if (columns.Length == 0) {
				//merge without update is OK
				return sb;
			}
			sb.Append("when matched then update set").AppendLine();
			foreach (var column in columns) {
				sb.Tab().EscapeName(column.Name).Append(" = src.").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
					sb.AppendLine();
				}
			}
			return sb;
		}
	}
}
