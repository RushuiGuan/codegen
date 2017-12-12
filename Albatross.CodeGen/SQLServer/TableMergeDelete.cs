using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableMergeDelete : ITableBasedQuery {

		public string Scenario => "table_merge_delete";
		public string Description => "Merage statement delete clause";

		public StringBuilder Build(string schema, string table, ITableQueryFactory factory, StringBuilder sb, IDictionary<string, Column> @params) {
			return sb.Append("when not matched by source then delete");
		}
	}
}
