using Albatross.CodeGen.Core;
using Albatross.CodeGen.Faults;
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

		public override IEnumerable<object> Generate(StringBuilder sb, IDictionary<string, string> customCode, Table t, SqlCodeGenOption options) {
			IEnumerable<Column> columns = t.Columns ?? new Column[0];
			if (columns.Count() == 0) {
				throw new CodeGeneratorException($"Table doesn't have any columns");
			}
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
