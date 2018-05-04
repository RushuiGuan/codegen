using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator("sql.select.table", GeneratorTarget.Sql, Category = GeneratorCategory.SQLServer, Description = "Select statement sorted by ordinal position")]
	public class SelectStatement : TableQueryGenerator {
		IGetTable getTable;

		public SelectStatement(IGetTable getTable) {
			this.getTable = getTable;
		}

		public override IEnumerable<object> Build(StringBuilder sb, Table t, SqlCodeGenOption options) {
			t = getTable.Get(t.Database, t.Schema, t.Name);
			IEnumerable<Column> columns = t.Columns;
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
