using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeSelectWithAudit : ITableBasedQuery {
		IGetTableColumns _getColumns;
		IGetVariableName _getVariableName;
		IBuiltInColumnFactory _builtInColumnFactory;

		public TableMergeSelectWithAudit(IGetTableColumns getColumns, IGetVariableName getVariableName, IBuiltInColumnFactory builtInColumnFactory) {
			_getColumns = getColumns;
			_getVariableName = getVariableName;
			_builtInColumnFactory = builtInColumnFactory;
		}

		public string Scenario => "table_merge_select_w_audit";
		public string Description => "Merge statement select clause with audit fields";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			IEnumerable<Column> columns = _getColumns.Get(schema, table);
			if (columns.Count() == 0) {
				throw new ColumnNotFoundException(schema, table);
			}
			sb.Append("using ").OpenParenthesis().AppendLine();
			sb.Tab().AppendLine("select");
			foreach (var column in columns) {
				if ((column.Name == "created" || column.Name == "modified") && _builtInColumnFactory.Match(column)) {
					sb.Tab(2).Append("getdate()");
				} else if ((column.Name == "createdby" || column.Name == "modifiedby") && _builtInColumnFactory.Match(column)) {
					sb.Tab(2).Append("@user");
					@params["@user"] = column;
				} else {
					string name = _getVariableName.Get(column.Name);
					@params[name] = column;
					sb.Tab(2).Append(name);
				}
				sb.Append(" as ").EscapeName(column.Name);
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
