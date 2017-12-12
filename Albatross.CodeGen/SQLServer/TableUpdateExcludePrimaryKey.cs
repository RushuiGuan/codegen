using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableUpdateExcludePrimaryKey : ITableBasedQuery {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;
		IGetTablePrimaryKey _getPrimary;

		public string Scenario => "table_update_exclude_primarykey";
		public string Description => "Update statement that exclude the primary keys as well as the computed columns";

		public TableUpdateExcludePrimaryKey(IGetTableColumns getTableColumns, IGetTablePrimaryKey getPrimary, IGetVariableName getVariableName) {
			_getTableColumns = getTableColumns;
			_getPrimary = getPrimary;
			_getVariableName = getVariableName;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			sb.Append($"update [{schema}].[{table}] set").AppendLine();
			IEnumerable<string> primaryKey = from c in _getPrimary.Get(schema, table) select c.Name;

			Column[] columns = (from c in _getTableColumns.Get(schema, table).ToArray()
					where !c.IdentityColumn && !c.ComputedColumn && !primaryKey.Contains(c.Name)
					select c).ToArray();

			string name;
			foreach (Column c in columns) {
				name = _getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ").Append(name);
				@params[name] = c;
				if (c != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return sb;
		}
	}
}
