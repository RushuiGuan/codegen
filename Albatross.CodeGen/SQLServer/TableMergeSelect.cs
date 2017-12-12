using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeSelect : ITableBasedQuery {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;

		public TableMergeSelect(IGetTableColumns getColumns, IGetVariableName getVariableName) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
		}

		public string Scenario => "table_merge_select";
		public string Description => "Merge statement select clause";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			Column[] columns = _getColumns.Get(schema, table).ToArray();
			if (columns.Length == 0) {
				throw new ColumnNotFoundException(schema, table);
			}
			sb.Append("using ").OpenParenthesis().AppendLine();
			sb.Tab().AppendLine("select");
			foreach (var column in columns) {
				string name = _getVariableName.Get(column.Name);
				@params[name] = column;
				sb.Tab(2).Append(name).Append(" as ").EscapeName(column.Name);
				if (column != columns.Last()) {
					sb.Comma();
				}
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" as src");
			return sb;
		}
	}
}
