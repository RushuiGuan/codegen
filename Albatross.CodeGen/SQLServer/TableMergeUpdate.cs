using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeUpdate : ITableBasedQuery {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;

		public TableMergeUpdate(IGetTableColumns getColumns, IGetVariableName getVariableName) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
		}

		public string Scenario => "table_merge_update";
		public string Description => "Table merge update clause";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			Column[] columns = (from c in _getColumns.Get(schema, table)
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
