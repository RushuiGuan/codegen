
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableUpdateExcludePrimaryKey : TableQueryGenerator {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;
		IGetTablePrimaryKey _getPrimary;

		public override string Name => "table_update_exclude_primarykey";
		public override string Description => "Update statement that exclude the primary keys as well as the computed columns";

		public TableUpdateExcludePrimaryKey(IGetTableColumns getTableColumns, IGetTablePrimaryKey getPrimary, IGetVariableName getVariableName) {
			_getTableColumns = getTableColumns;
			_getPrimary = getPrimary;
			_getVariableName = getVariableName;
		}

		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			IEnumerable<string> primaryKey = from c in _getPrimary.Get(t) select c.Name;

			Column[] columns = (from c in _getTableColumns.Get(t).ToArray()
								where !c.IdentityColumn && !c.ComputedColumn && !primaryKey.Contains(c.Name)
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
