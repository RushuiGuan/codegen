using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableWhereByID : ITableBasedQuery {
		IGetTableIdentityColumn _getIDColumn;
		IGetVariableName _getVariableName;

		public string Scenario => "table_where_by_id";
		public string Description => "Where clause by the identity column";

		public TableWhereByID(IGetTableIdentityColumn getIDColumn, IGetVariableName getVariableName) {
			_getIDColumn = getIDColumn;
			_getVariableName = getVariableName;
		}

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			Column identityColumn = _getIDColumn.Get(schema, table);
			if (identityColumn == null) {
				throw new IdentityColumnNotFoundException(schema, table);
			}
			string name = _getVariableName.Get(identityColumn.Name);
			@params[name] = identityColumn;
			sb.AppendLine("where");
			sb.Tab().EscapeName(identityColumn.Name).Append(" = ").Append(name);
			return sb;
		}
	}
}
