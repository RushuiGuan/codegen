using Albatross.CodeGen;
using Albatross.CodeGen.Database;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.SqlServer {
	[CodeGenerator]
	public class TableInsert : TableCodeGenerator {
		IGetTableColumns _getTableColumns;
		IGetVariableName _getVariableName;

		public TableInsert(IGetTableColumns getTableColumns, IGetVariableName getVariableName) {
			_getTableColumns = getTableColumns;
			_getVariableName = getVariableName;
		}

		public override string Name => "table_insert";
		public override string Description => "Insert statement that excludes the computed columns";

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
				string name = _getVariableName.Get(c.Name);
				sb.Tab().Append(name);
				if (c != columns.Last()) { sb.Comma(); }
				sb.AppendLine();
			}
			sb.CloseParenthesis().Semicolon();
			return sb;
		}
	}
}
