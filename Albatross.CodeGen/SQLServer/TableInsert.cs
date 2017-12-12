using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableInsert : ITableBasedQuery {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;

		public TableInsert(IGetTableColumns getTableColumns, IGetVariableName getVariableName) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
		}

		public string Scenario => "table_insert";
		public string Description => "Insert statement that excludes the computed columns";

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
				string name = _getVariableName.Get(c.Name);
				@params[name] = c;
				sb.Tab().Append(name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.CloseParenthesis().Semicolon();
			return sb;
		}
	}
}
