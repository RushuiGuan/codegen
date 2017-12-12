using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableDeleteByID : CompositeTableQuery{
		public TableDeleteByID() : base("table_delete_default_by_id","table_delete", "table_where_by_id") {
		}
	}
}
