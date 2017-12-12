using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableUpdateWithAudit : ITableBasedQuery {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;
		IBuiltInColumnFactory _builtInColumnFactory;

		public string Scenario => "table_update_w_audit";
		public string Description => "Update statement with database populated audit fields such as createdby, modifiedby, created and modified";

		public TableUpdateWithAudit(IGetTableColumns getTableColumns, IGetVariableName getVariableName, IBuiltInColumnFactory builtInColumnFactory) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
			_builtInColumnFactory = builtInColumnFactory;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			sb.Append($"update [{schema}].[{table}] set").AppendLine();
			Column[] columns = (from c in _getTableColumns.Get(schema, table).ToArray()
					where !c.IdentityColumn && !c.ComputedColumn
					select c).ToArray();

			string name;
			foreach (Column c in columns) {
				string columnName = c.Name.ToLower();
				if ((columnName == "created" || columnName == "createdby") && _builtInColumnFactory.Get(c.Name).Match(c)) {
					continue;
				} else if (columnName == "modified" && _builtInColumnFactory.Get(c.Name).Match(c)) {
					sb.Tab().EscapeName(c.Name).Append(" = getdate()");
				} else if (columnName == "modifiedby" && _builtInColumnFactory.Get(c.Name).Match(c)) {
					sb.Tab().EscapeName(c.Name).Append(" = @user");
					@params["@user"] = c;
				} else {
					name = _getVariableName.Get(c.Name);
					sb.Tab().EscapeName(c.Name).Append(" = ").Append(name);
					@params[name] = c;
				}
				if (c != columns.Last()) { sb.Comma().AppendLine(); }
			}
			return sb;
		}
	}
}
