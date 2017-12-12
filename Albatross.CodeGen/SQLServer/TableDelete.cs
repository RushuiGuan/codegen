using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableDelete : ITableBasedQuery {

		public TableDelete() {
		}

		public string Scenario => "table_delete";
		public string Description => "Default delete statement";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			sb.Append($"delete from [{schema}].[{table}]");
			return sb;
		}
	}
}
