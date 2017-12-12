using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeJoinByID : ITableBasedQuery {
		IGetTableIdentityColumn _getIDColumn;

		public string Scenario => "table_merge_join_by_id";
		public string Description => "Merge statement source join clause by the identity column";

		public TableMergeJoinByID(IGetTableIdentityColumn getIDColumn) {
			_getIDColumn = getIDColumn;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			Column idColumn = _getIDColumn.Get(schema, table);
			if (idColumn == null) {
				throw new IdentityColumnNotFoundException(schema, table);
			}
			sb.Append("on src.").EscapeName(idColumn.Name).Append(" = dst.").EscapeName(idColumn.Name);
			return sb;
		}
	}
}
