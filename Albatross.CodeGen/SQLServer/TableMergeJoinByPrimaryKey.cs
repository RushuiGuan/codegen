using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeJoinByPrimaryKey : ITableBasedQuery {
		IGetTablePrimaryKey _getPrimaryKey;

		public string Scenario => "table_merge_join_by_primarykey";
		public string Description => "Merge statement source join clause by the primary key columns";

		public TableMergeJoinByPrimaryKey(IGetTablePrimaryKey getPrimaryKey) {
			_getPrimaryKey = getPrimaryKey;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			IEnumerable<Column> keys = _getPrimaryKey.Get(schema, table);
			if (keys.Count() == 0) {
				throw new PrimaryKeyNotFoundException(schema, table);
			}
			sb.Append("on ");
			foreach (Column key in keys) {
				if (key != keys.First()) { sb.AppendLine().Tab().Append("and "); }
				sb.Append("src.").EscapeName(key.Name).Append(" = dst.").EscapeName(key.Name);
			}
			return sb;
		}
	}
}
