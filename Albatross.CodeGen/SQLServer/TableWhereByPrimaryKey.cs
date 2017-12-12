using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableWhereByPrimaryKey : ITableBasedQuery {
		IGetTablePrimaryKey _getPrimaryKey;
		IGetVariableName _getVariableName;

		public string Scenario => "table_where_by_primarykey";
		public string Description => "Where clause by the primary key";

		public TableWhereByPrimaryKey(IGetTablePrimaryKey getPrimaryKey, IGetVariableName getVariableName) {
			_getPrimaryKey = getPrimaryKey;
			_getVariableName = getVariableName;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			sb.AppendLine("where");
			IEnumerable<Column> keys = _getPrimaryKey.Get(schema, table);
			if (keys.Count() == 0) {
				throw new PrimaryKeyNotFoundException(schema, table);
			}
			foreach (Column key in keys) {
				string name = _getVariableName.Get(key.Name);
				@params[name] = key;
				sb.Tab();
				if (key != keys.First()) { sb.Append("and "); }
				sb.EscapeName(key.Name).Append(" = ").Append(name);
				if (key != keys.Last()) {
					sb.AppendLine();
				}
			}
			return sb;
		}
	}
}
