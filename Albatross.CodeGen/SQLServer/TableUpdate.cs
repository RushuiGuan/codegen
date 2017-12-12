using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableUpdate : ITableBasedQuery {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;

		public string Scenario => "table_update";
		public string Description => "Update statement that excludes the computed columns";

		public TableUpdate(IGetTableColumns getTableColumns, IGetVariableName getVariableName) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			sb.Append($"update [{schema}].[{table}] set").AppendLine();
			Column[] columns = (from c in _getTableColumns.Get(schema, table).ToArray()
					where !c.IdentityColumn && !c.ComputedColumn
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
