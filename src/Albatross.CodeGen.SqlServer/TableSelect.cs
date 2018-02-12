
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("table_select", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Select statement sorted by ordinal position")]
	public class TableSelect : TableQueryGenerator {
		IGetTableColumns getTableColumns;

		public TableSelect(IGetTableColumns getTableColumns) {
			this.getTableColumns = getTableColumns;
		}

		public override IEnumerable<object> Build(StringBuilder sb, DatabaseObject t, SqlQueryOption options) {
			IEnumerable<Column> columns = getTableColumns.Get(t);
			sb.Append("select").AppendLine();
			foreach (Column c in columns) {
				sb.Tab().EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.Append("from ").EscapeName(t.Schema).Dot().EscapeName(t.Name);
			return new[] { this };
		}
	}
}
