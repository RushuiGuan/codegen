
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableMergeUpdateExcludePrimaryKey : TableCodeGenerator {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;
		IGetTablePrimaryKey _getPrimaryKey;

		public TableMergeUpdateExcludePrimaryKey(IGetTableColumns getColumns, IGetVariableName getVariableName, IGetTablePrimaryKey getPrimaryKey) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
			_getPrimaryKey = getPrimaryKey;
		}

		public override string Name => "table_merge_update_exclude_primarykey";
		public override string Description => "Table merge update clause exclude the primary key columns";

		public override StringBuilder Build(StringBuilder sb, Table table, object options, ICodeGeneratorFactory factory) {
			IEnumerable<string> primaryKeys = from c in _getPrimaryKey.Get(table) select c.Name;
			Column[] columns = (from c in _getColumns.Get(table)
								orderby c.OrdinalPosition ascending, c.Name ascending
								where !c.ComputedColumn && !c.IdentityColumn && !primaryKeys.Contains(c.Name)
								select c).ToArray();
			if (columns.Length == 0) {
				//it is possible for a merge without an update statement
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
