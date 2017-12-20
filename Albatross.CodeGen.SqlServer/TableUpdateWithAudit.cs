
using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableUpdateWithAudit : TableCodeGenerator {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;
		IBuiltInColumnFactory _builtInColumnFactory;

		public override string Name => "table_update_w_audit";
		public override string Description => "Update statement with database populated audit fields such as createdby, modifiedby, created and modified";

		public TableUpdateWithAudit(IGetTableColumns getTableColumns, IGetVariableName getVariableName, IBuiltInColumnFactory builtInColumnFactory) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
			_builtInColumnFactory = builtInColumnFactory;
		}


		public override StringBuilder Build(StringBuilder sb, Table t, object options, ICodeGeneratorFactory factory) {
			sb.Append($"update [{t.Schema}].[{t.Name}] set").AppendLine();
			Column[] columns = (from c in _getTableColumns.Get(t).ToArray()
								where !c.IdentityColumn && !c.ComputedColumn
								select c).ToArray();

			string name;
			foreach (Column c in columns) {
				string columnName = c.Name.ToLower();
				if ((columnName == "created" || columnName == "createdby") && _builtInColumnFactory.Get(c.Name).Match(c)) {
					continue;
				} else if (columnName == "modified" && _builtInColumnFactory.Get(c.Name).Match(c)) {
					sb.Tab().EscapeName(c.Name).Append(" = getdate()");
				} else if (columnName == "modifiedby" && _builtInColumnFactory.Get(c.Name).Match(c)) {
					sb.Tab().EscapeName(c.Name).Append(" = @user");
				} else {
					name = _getVariableName.Get(c.Name);
					sb.Tab().EscapeName(c.Name).Append(" = ").Append(name);
				}
				if (c != columns.Last()) { sb.Comma().AppendLine(); }
			}
			return sb;
		}
	}
}
