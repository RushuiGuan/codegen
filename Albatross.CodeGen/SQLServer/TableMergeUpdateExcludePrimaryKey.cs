using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeUpdateExcludePrimaryKey : ITableBasedQuery {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;
		IGetTablePrimaryKey _getPrimaryKey;

		public TableMergeUpdateExcludePrimaryKey(IGetTableColumns getColumns, IGetVariableName getVariableName, IGetTablePrimaryKey getPrimaryKey) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
			_getPrimaryKey = getPrimaryKey;
		}

		public string Scenario => "table_merge_update_exclude_primarykey";
		public string Description => "Table merge update clause exclude the primary key columns";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			IEnumerable<string> primaryKeys = from c in _getPrimaryKey.Get(schema, table) select c.Name;
			Column[] columns = (from c in _getColumns.Get(schema, table)
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
