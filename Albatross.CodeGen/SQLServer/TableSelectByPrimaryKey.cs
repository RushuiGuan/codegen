using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public class TableSelectByPrimaryKey : CompositeTableQuery{
		public TableSelectByPrimaryKey() : base("table_select_by_primarykey", "table_select", "table_where_by_primarykey") {
			Description = "Composite: Select statement with the where clause on the primary key";
		}
	}
}
