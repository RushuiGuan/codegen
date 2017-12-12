using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableSelectByID : CompositeTableQuery{
		public TableSelectByID() : base("table_select_by_id", "table_select", "table_where_by_id") {
			Description = "Composite: Select statement with the where clause on the identity column";
		}
	}
}
