using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableSelectScopedIdentity : ITableBasedQuery {
		IGetTableIdentityColumn _getIDColumn;

		public string Scenario => "table_select_scope_identity";
		public string Description => "Select the Scope_Identity() of the previous insert operation.  Typically used after the insert operation.";

		public TableSelectScopedIdentity(IGetTableIdentityColumn getIDColumn) {
			_getIDColumn = getIDColumn;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			Column identityColumn = _getIDColumn.Get(schema, table);
			if (identityColumn == null) {
				throw new IdentityColumnNotFoundException(schema, table);
			}
			return sb.Append("select scope_identity() as ").EscapeName(identityColumn.Name);
		}
	}
}
