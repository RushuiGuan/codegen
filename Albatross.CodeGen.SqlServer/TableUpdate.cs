
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	public class TableUpdate : TableCodeGenerator {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;

		public override string Name => "table_update";
		public override string Description => "Update statement that excludes the computed columns";

		public TableUpdate(IGetTableColumns getTableColumns, IGetVariableName getVariableName) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
		}

		public override StringBuilder Build(StringBuilder sb, Table t, ICodeGeneratorFactory factory) {
			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			Column[] columns = (from c in _getTableColumns.Get(t).ToArray()
								where !c.IdentityColumn && !c.ComputedColumn
								select c).ToArray();

			string name;
			foreach (Column c in columns) {
				name = _getVariableName.Get(c.Name);
				sb.Tab().EscapeName(c.Name).Append(" = ").Append(name);
				if (c != columns.Last()) {
					sb.Comma().AppendLine();
				}
			}
			return sb;
		}

	}
}
