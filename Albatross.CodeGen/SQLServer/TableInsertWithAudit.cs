using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableInsertWithAudit : ITableBasedQuery {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;
		IBuiltInColumnFactory _builtInColumnFactory;

		public TableInsertWithAudit(IGetTableColumns getTableColumns, IGetVariableName getVariableName, IBuiltInColumnFactory builtInColumnFactory) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
			_builtInColumnFactory = builtInColumnFactory;
		}

		public string Scenario => "table_insert_w_audit";
		public string Description => "Insert statement with database populated audit fields such as createdby, modifiedby, created and modified";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			Column[] columns = (from c in _getTableColumns.Get(schema, table).ToArray() where !c.IdentityColumn && !c.ComputedColumn select c).ToArray();
			if (columns.Length == 0) {
				throw new ColumnNotFoundException(schema, table);
			}
			sb.Append($"insert into [{schema}].[{table}] ").OpenParenthesis().AppendLine();
			foreach (Column c in columns) {
				sb.Tab().EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" values ").OpenParenthesis().AppendLine();
			foreach (Column c in columns) {
				string variable, name = c.Name.ToLower();
				if ((name == "created" || name == "modified") && _builtInColumnFactory.Get(name).Match(c)) {
					sb.Tab().Append("getdate()");
				} else if ((name == "createdby" || name == "modifiedby") && _builtInColumnFactory.Get(name).Match(c)) {
					variable = "@user";
					@params[variable] = c;
					sb.Tab().Append(variable);
				} else {
					variable = _getVariableName.Get(c.Name);
					@params[variable] = c;
					sb.Tab().Append(variable);
				}
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.CloseParenthesis().Semicolon();
			return sb;
		}
	}
}
