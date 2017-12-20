using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableInsertWithAudit : TableCodeGenerator {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;
		IBuiltInColumnFactory _builtInColumnFactory;

		public TableInsertWithAudit(IGetTableColumns getTableColumns, IGetVariableName getVariableName, IBuiltInColumnFactory builtInColumnFactory) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
			_builtInColumnFactory = builtInColumnFactory;
		}

		public override string Name => "table_insert_w_audit";
		public override string Description => "Insert statement with database populated audit fields such as createdby, modifiedby, created and modified";


		public override StringBuilder Build(StringBuilder sb, Table table, ICodeGeneratorFactory factory) {

			Column[] columns = (from c in _getTableColumns.Get(table) where !c.IdentityColumn && !c.ComputedColumn select c).ToArray();
			if (columns.Length == 0) {
				throw new ColumnNotFoundException(table.Schema, table.Name);
			}
			sb.Append($"insert into [{table.Schema}].[{table.Name}] ").OpenParenthesis().AppendLine();
			foreach (Column c in columns) {
				sb.Tab().EscapeName(c.Name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.CloseParenthesis().Append(" values ").OpenParenthesis().AppendLine();
			foreach (Column c in columns) {
				string variable, name = c.Name.ToLower();
				if ((name == "created" || name == "modified") && _builtInColumnFactory.Get(name).Match(c)) {
					sb.Tab().Append("getdate()");
				} else if ((name == "createdby" || name == "modifiedby") && _builtInColumnFactory.Get(name).Match(c)) {
					variable = "@user";
					sb.Tab().Append(variable);
				} else {
					variable = _getVariableName.Get(c.Name);
					sb.Tab().Append(variable);
				}
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.CloseParenthesis().Semicolon();
			return sb;
		}

		
	}
}
