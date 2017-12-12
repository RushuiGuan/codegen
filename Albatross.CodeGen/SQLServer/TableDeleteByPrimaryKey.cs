using Albatross.DbScripting.Core;
using Albatross.DbScripting.Core.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableDeleteByPrimaryKey : CompositeTableQuery{
		public TableDeleteByPrimaryKey() : base("table_delete_default_by_primarykey", "table_delete", "table_where_by_primarykey") {
		}
	}
}
