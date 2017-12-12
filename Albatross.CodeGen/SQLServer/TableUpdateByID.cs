using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableUpdateByID : CompositeTableQuery {
		public TableUpdateByID() : base("table_update_by_id", new string[] { "table_update", "table_where_by_id" }) {
			Description = "Update statement with where clause on the identity column";
		}
	}
}
