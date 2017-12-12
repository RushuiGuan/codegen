using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableSelect : ITableBasedQuery {
		IGetTableColumns _getTableColumns;

		public TableSelect(IGetTableColumns getTableColumns) {
			_getTableColumns = getTableColumns;
		}

		public string Scenario => "table_select";
		public string Description => "Select statement sorted by ordinal position";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			IEnumerable<Column> columns =  _getTableColumns.Get(schema, table);
			sb.Append("select").AppendLine();
			foreach (Column c in columns) {
				sb.Tab().EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.Append("from ").EscapeName(schema).Dot().EscapeName(table);
			return sb;
		}
	}
}
