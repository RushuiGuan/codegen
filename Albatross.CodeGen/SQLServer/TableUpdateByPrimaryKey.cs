using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableUpdateByPrimaryKey : CompositeTableQuery {
		public TableUpdateByPrimaryKey() : base("table_update_by_primarykey", new string[] { "table_update_exclude_primarykey", "table_where_by_primarykey" }) {
			Description = "Update statement with where clause on the primary key columns";
		}
	}
}
