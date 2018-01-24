
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableSelect : TableQueryGenerator{
		IGetTableColumns _getTableColumns;

		public TableSelect(IGetTableColumns getTableColumns) {
			_getTableColumns = getTableColumns;
		}

		public override string Name => "table_select";
		public override string Description => "Select statement sorted by ordinal position";

		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			IEnumerable<Column> columns = _getTableColumns.Get(t);
			sb.Append("select").AppendLine();
			foreach (Column c in columns) {
				sb.Tab().EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.Append("from ").EscapeName(t.Schema).Dot().EscapeName(t.Name);
			return sb;
		}
	}
}
